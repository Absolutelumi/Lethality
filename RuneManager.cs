using PoniLCU;
using static PoniLCU.LeagueClient;
using Newtonsoft.Json.Linq;
using Timer = System.Timers.Timer;

namespace Lethality
{
    internal class RuneManager
    {
        internal event Action<RunePage> RunePageUpdate;

        private LeagueClient Client;
        private Timer ClientConnectionTimer;

        private bool ReadyToUpdateView = true;
        private JObject CurrentPage;
        private string CurrentChampName = "";

        internal RuneManager(Action<RunePage> onRunePageUpdate)
        {
            // Initialize Event
            RunePageUpdate = new Action<RunePage>(onRunePageUpdate);

            // Initialize client connection timer
            /*
            ClientConnectionTimer = new Timer(1 * 1000);
            ClientConnectionTimer.Elapsed += (_, __) =>
            {
                // Failsafe b/c elapsing twice somehow ???
                if (Client.IsConnected)
                {
                    ClientConnectionTimer.Stop();
                    return;
                }

                Client = new(credentials.lockfile);
                if (Client.IsConnected)
                {
                    ClientConnectionTimer.Stop();
                    OnClientConnected();
                }
            };
            */

            // Send default page to view if client not connected on initial try
            Client = new(credentials.lockfile);
            if (!Client.IsConnected)
            {
                //ClientConnectionTimer.Start();

                RunePageUpdate.Invoke(
                    BuildRunePage(
                    8000,
                    8100,
                    new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 }));

                Client.OnConnected += () =>
                {
                    OnClientConnected();
                };
            }
            else OnClientConnected();
        }

        private void OnClientConnected()
        {
            Client.OnDisconnected += () =>
            {
                //ClientConnectionTimer.Start();
            };

            // Send new rune page to view when rune page updates 
            Client.Subscribe("/lol-perks/v1/perks/gameplay-updated", (OnWebsocketEventArgs) =>
            {
#pragma warning disable CS0612
#pragma warning disable CS0618 
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Bad solution
                    ReadyToUpdateView = !ReadyToUpdateView;

                    if (ReadyToUpdateView)
                    {
                        CurrentPage = JObject.Parse(Client.Request(requestMethod.GET, "/lol-perks/v1/currentpage").Result);

                        RunePageUpdate.Invoke(
                            BuildRunePage(
                            CurrentPage["primaryStyleId"].Value<int>(),
                            CurrentPage["subStyleId"].Value<int>(),
                            CurrentPage["selectedPerkIds"].Values<int>().ToList()));
                    }
                });
#pragma warning restore CS0618 
#pragma warning restore CS0612
            });

            // Update rune page to highest PR page upon hovering a champion
            Client.Subscribe("/lol-champ-select/v1/current-champion", (OnWebsocketEventArgs) =>
            {
                var sessionInfo = JObject.Parse(Client.Request(requestMethod.GET, "/lol-champ-select/v1/session").Result);

                // Event is called on game start with null session info - return for this case
                if (sessionInfo["errorCode"] != null) return;

                var playerInfo = sessionInfo["myTeam"][0];

                string champId = playerInfo["championId"].Value<int>().ToString();
                string roleName = playerInfo["assignedPosition"].Value<string>();
                roleName = roleName == "" ? "mid" : roleName; // If no role for current gamemode, default to mid

                JToken highestPRPage;
                using (HttpClient webClient = new HttpClient())
                {
                    var champDataStr = webClient.GetStringAsync($"https://na.op.gg/api/champions/{champId}/{roleName}/runes").Result;
                    var champData = JObject.Parse(champDataStr)["data"];

                    CurrentChampName = champData["summary"]["meta"]["name"].Value<string>();
                    highestPRPage = champData["runes"][0];
                }

                // Build highest PR rune ids list
                List<int> runeIds = new List<int>();
                runeIds.AddRange(highestPRPage["primary_rune_ids"].Values<int>().ToList());
                runeIds.AddRange(highestPRPage["secondary_rune_ids"].Values<int>());
                runeIds.AddRange(highestPRPage["stat_mod_ids"].Values<int>());

                // Send the rune page to the client (which, in turn, will send it to the view)
                SendRunePageToClient(
                    BuildRunePage(
                    highestPRPage["primary_page_id"].Value<int>(),
                    highestPRPage["secondary_page_id"].Value<int>(),
                    runeIds));
            });

            // Get current rune page 
            CurrentPage = JObject.Parse(Client.Request(requestMethod.GET, "/lol-perks/v1/currentpage").Result);

            // Send view current page's perk images
            RunePageUpdate.Invoke(
                BuildRunePage(
                CurrentPage["primaryStyleId"].Value<int>(),
                CurrentPage["subStyleId"].Value<int>(),
                CurrentPage["selectedPerkIds"].Values<int>().ToList()));
        }

        internal void SendRunePageToClient(RunePage runePage)
        {
            // Handle input while client still not open
            if (!Client.IsConnected) return;

            int[] runeIds = new int[]
            {
                // Primary
                runePage.Keystones.selected,
                runePage.Slot1Runes.selected,
                runePage.Slot2Runes.selected,
                runePage.Slot3Runes.selected,

                // Secondary
                (runePage.SecondarySlot1Runes.selected != 0 ? runePage.SecondarySlot1Runes.selected : runePage.SecondarySlot2Runes.selected),
                (runePage.SecondarySlot3Runes.selected != 0 ? runePage.SecondarySlot3Runes.selected : runePage.SecondarySlot2Runes.selected),

                // Stats
                runePage.Slot1Stats.selected,
                runePage.Slot2Stats.selected,
                runePage.Slot3Stats.selected
            };

            // Set name
            CurrentPage["name"] = $"Lethality{(CurrentChampName == "" ? "" : $": {CurrentChampName}")}";

            // Set runes
            CurrentPage["selectedPerkIds"] = JToken.FromObject(runeIds);

            // Set primary style
            CurrentPage["primaryStyleId"] = runePage.PrimaryCategoryId;

            // Set secondary style
            CurrentPage["subStyleId"] = runePage.SecondaryCategoryId;

            // Delete current
            Client.Request(requestMethod.DELETE, "/lol-perks/v1/pages", CurrentPage["order"]);

            // Send rune page to client
            Client.Request(requestMethod.POST, "/lol-perks/v1/pages", CurrentPage);
        }

        private RunePage BuildRunePage(int primaryId, int secondaryId, List<int> runeIds)
        {
            // 0 - 3: Primary rune tree
            var primary = RuneDictionary.GetRuneCategoryWithId(primaryId);

            // 4 - 5: Secondary rune tree
            var secondary = RuneDictionary.GetRuneCategoryWithId(secondaryId);

            // 6 - 8: Stats
            var stats = RuneDictionary.Stats;

            return new RunePage()
            {
                // Categories
                PrimaryCategoryId = primaryId,
                SecondaryCategoryId = secondaryId,

                // Primary
                Keystones = new RuneList(runeIds[0], primary.Keystones.Values.ToList()),
                Slot1Runes = new RuneList(runeIds[1], primary.Slot1.Values.ToList()),
                Slot2Runes = new RuneList(runeIds[2], primary.Slot2.Values.ToList()),
                Slot3Runes = new RuneList(runeIds[3], primary.Slot3.Values.ToList()),

                // Secondary 
                SecondarySlot1Runes = new RuneList(secondary.Slot1.Keys.Contains(runeIds[4]) ? runeIds[4]
                : secondary.Slot1.Keys.Contains(runeIds[5]) ? runeIds[5]
                : 0,
                secondary.Slot1.Values.ToList()),

                SecondarySlot2Runes = new RuneList(secondary.Slot2.Keys.Contains(runeIds[4]) ? runeIds[4]
                : secondary.Slot2.Keys.Contains(runeIds[5]) ? runeIds[5]
                : 0,
                secondary.Slot2.Values.ToList()),

                SecondarySlot3Runes = new RuneList(secondary.Slot3.Keys.Contains(runeIds[4]) ? runeIds[4]
                : secondary.Slot3.Keys.Contains(runeIds[5]) ? runeIds[5]
                : 0,
                secondary.Slot3.Values.ToList()),

                // Stats
                Slot1Stats = new RuneList(runeIds[6], stats.Slot1.Values.ToList()),
                Slot2Stats = new RuneList(runeIds[7], stats.Slot2.Values.ToList()),
                Slot3Stats = new RuneList(runeIds[8], stats.Slot3.Values.ToList())
            };
        }

        internal struct RunePage
        {
            internal int PrimaryCategoryId;
            internal int SecondaryCategoryId;

            internal RuneList Keystones;
            internal RuneList Slot1Runes;
            internal RuneList Slot2Runes;
            internal RuneList Slot3Runes;

            internal RuneList SecondarySlot1Runes;
            internal RuneList SecondarySlot2Runes;
            internal RuneList SecondarySlot3Runes;

            internal RuneList Slot1Stats;
            internal RuneList Slot2Stats;
            internal RuneList Slot3Stats;

            internal bool Equals(RunePage runePage)
            {
                if (PrimaryCategoryId.Equals(runePage.PrimaryCategoryId) &&
                    SecondaryCategoryId.Equals(runePage.SecondaryCategoryId) &&
                    Keystones.Equals(runePage.Keystones) &&
                    Slot1Runes.Equals(runePage.Slot1Runes) &&
                    Slot2Runes.Equals(runePage.Slot2Runes) &&
                    Slot3Runes.Equals(runePage.Slot3Runes) &&
                    SecondarySlot1Runes.Equals(runePage.SecondarySlot1Runes) &&
                    SecondarySlot2Runes.Equals(runePage.SecondarySlot2Runes) &&
                    SecondarySlot3Runes.Equals(runePage.SecondarySlot3Runes) &&
                    Slot1Stats.Equals(runePage.Slot1Stats) &&
                    Slot2Stats.Equals(runePage.Slot2Stats) &&
                    Slot3Stats.Equals(runePage.Slot3Stats)) return true;
                else return false;
            }
        }

        internal struct RuneList
        {
            internal int selected;
            internal List<string> images;

            internal RuneList(int selected, List<string> images)
            {
                this.selected = selected;
                this.images = images;
            }

            internal bool Equals(RuneList runeList)
            {
                if (selected.Equals(runeList.selected) &&
                    (images == null && runeList.images == null ||
                    images.SequenceEqual(runeList.images))) return true;
                else return false;
            }
        }
    }
}
