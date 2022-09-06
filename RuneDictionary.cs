namespace Lethality
{
    internal static class RuneDictionary
    {
        // TODO: Use relative path
        private static readonly string RunesPath = "C:\\Users\\jacks\\Desktop\\Projects\\Lethality\\Resources\\League\\Runes\\";

        internal static readonly string RuneImage = RunesPath + "RunesIcon.png";

        internal static readonly RuneCategory Precision = new RuneCategory()
        {
            Id = 8000,
            Image = RunesPath + "7201_Precision.png",

            Keystones = new Dictionary<int, string>()
            {{8005, RunesPath + "Precision\\PressTheAttack\\PressTheAttack.png"},
            {8008, RunesPath + "Precision\\LethalTempo\\\\LethalTempoTemp.png"},
            {8021, RunesPath + "Precision\\FleetFootwork\\FleetFootwork.png"},
            {8010, RunesPath + "Precision\\Conqueror\\Conqueror.png"}},

            Slot1 = new Dictionary<int, string>()
            {{9101, RunesPath + "Precision\\Overheal.png"},
            {9111, RunesPath + "Precision\\Triumph.png"},
            {8009, RunesPath + "Precision\\PresenceOfMind\\PresenceOfMind.png"}},

            Slot2 = new Dictionary<int, string>()
            {{9104, RunesPath + "Precision\\LegendAlacrity\\LegendAlacrity.png"},
            {9105, RunesPath + "Precision\\LegendTenacity\\LegendTenacity.png"},
            {9103, RunesPath + "Precision\\LegendBloodline\\LegendBloodline.png"}},

            Slot3 = new Dictionary<int, string>()
            {{8014, RunesPath + "Precision\\CoupDeGrace\\CoupDeGrace.png"},
            {8017, RunesPath + "Precision\\CutDown\\CutDown.png"},
            {8299, RunesPath + "Sorcery\\LastStand\\LastStand.png"}}
        };

        internal static readonly RuneCategory Domination = new RuneCategory()
        {
            Id = 8100,
            Image = RunesPath + "7200_Domination.png",

            Keystones = new Dictionary<int, string>()
            {{8112, RunesPath + "Domination\\Electrocute\\Electrocute.png"},
            {8124, RunesPath + "Domination\\Predator\\Predator.png"},
            {8128, RunesPath + "Domination\\DarkHarvest\\DarkHarvest.png"},
            {9923, RunesPath + "Domination\\HailOfBlades\\HailOfBlades.png"} },

            Slot1 = new Dictionary<int, string>()
            {{8126, RunesPath + "Domination\\CheapShot\\CheapShot.png"},
            {8139, RunesPath + "Domination\\TasteOfBlood\\GreenTerror_TasteOfBlood.png"},
            {8143, RunesPath + "Domination\\SuddenImpact\\SuddenImpact.png"}},

            Slot2 = new Dictionary<int, string>()
            {{8136, RunesPath + "Domination\\ZombieWard\\ZombieWard.png"},
            {8120, RunesPath + "Domination\\GhostPoro\\GhostPoro.png"},
            {8138, RunesPath + "Domination\\EyeballCollection\\EyeballCollection.png"}},

            Slot3 = new Dictionary<int, string>()
            {{8135, RunesPath + "Domination\\TreasureHunter\\TreasureHunter.png"},
            {8134, RunesPath + "Domination\\IngeniousHunter\\IngeniousHunter.png"},
            {8105, RunesPath + "Domination\\RelentlessHunter\\RelentlessHunter.png"},
            {8106, RunesPath + "Domination\\UltimateHunter\\UltimateHunter.png"}}
        };

        internal static readonly RuneCategory Sorcery = new RuneCategory()
        {
            Id = 8200,
            Image = RunesPath + "7202_Sorcery.png",

            Keystones = new Dictionary<int, string>()
            {{8214, RunesPath + "Sorcery\\SummonAery\\SummonAery.png"},
            {8229, RunesPath + "Sorcery\\ArcaneComet\\ArcaneComet.png"},
            {8230, RunesPath + "Sorcery\\PhaseRush\\PhaseRush.png"}},

            Slot1 = new Dictionary<int, string>()
            {{8224, RunesPath + "Sorcery\\NullifyingOrb\\Pokeshield.png"},
            {8226, RunesPath + "Sorcery\\ManaflowBand\\ManaflowBand.png"},
            {8275, RunesPath + "Sorcery\\NimbusCloak\\6361.png"}},

            Slot2 = new Dictionary<int, string>()
            {{8210, RunesPath + "Sorcery\\Transcendence\\Transcendence.png"},
            {8234, RunesPath + "Sorcery\\Celerity\\CelerityTemp.png"},
            {8233, RunesPath + "Sorcery\\AbsoluteFocus\\AbsoluteFocus.png"}},

            Slot3 = new Dictionary<int, string>()
            {{8237, RunesPath + "Sorcery\\Scorch\\Scorch.png"},
            {8232, RunesPath + "Sorcery\\Waterwalking\\Waterwalking.png"},
            {8236, RunesPath + "Sorcery\\GatheringStorm\\GatheringStorm.png"}}
        };

        internal static readonly RuneCategory Resolve = new RuneCategory()
        {
            Id = 8400,
            Image = RunesPath + "7204_Resolve.png",

            Keystones = new Dictionary<int, string>()
            {{8437, RunesPath + "Resolve\\GraspOfTheUndying\\GraspOfTheUndying.png"},
            {8439, RunesPath + "Resolve\\VeteranAftershock\\VeteranAftershock.png"},
            {8465, RunesPath + "Resolve\\Guardian\\Guardian.png"}},

            Slot1 = new Dictionary<int, string>()
            {{8446, RunesPath + "Resolve\\Demolish\\Demolish.png"},
            {8463, RunesPath + "Resolve\\FontOfLife\\FontOfLife.png"},
            {8401, RunesPath + "Resolve\\MirrorShell\\MirrorShell.png"}},

            Slot2 = new Dictionary<int, string>()
            {{8429, RunesPath + "Resolve\\Conditioning\\Conditioning.png"},
            {8444, RunesPath + "Resolve\\SecondWind\\SecondWind.png"},
            {8473, RunesPath + "Resolve\\BonePlating\\BonePlating.png"}},

            Slot3 = new Dictionary<int, string>()
            {{8451, RunesPath + "Resolve\\Overgrowth\\Overgrowth.png"},
            {8453, RunesPath + "Resolve\\Revitalize\\Revitalize.png"},
            {8242, RunesPath + "Sorcery\\Unflinching\\Unflinching.png"}}
        };

        internal static readonly RuneCategory Inspiration = new RuneCategory()
        {
            Id = 8300,
            Image = RunesPath + "7203_Whimsy.png",

            Keystones = new Dictionary<int, string>()
            {{8351, RunesPath + "Inspiration\\GlacialAugment\\GlacialAugment.png"},
            {8360, RunesPath + "Inspiration\\UnsealedSpellbook\\UnsealedSpellbook.png"},
            {8369, RunesPath + "Inspiration\\FirstStrike\\FirstStrike.png"}},

            Slot1 = new Dictionary<int, string>()
            {{8306, RunesPath + "Inspiration\\HextechFlashtraption\\HextechFlashtraption.png"},
            {8304, RunesPath + "Inspiration\\MagicalFootwear\\MagicalFootwear.png"},
            {8313, RunesPath + "Inspiration\\PerfectTiming\\PerfectTiming.png"}},

            Slot2 = new Dictionary<int, string>()
            {{8321, RunesPath + "Inspiration\\FuturesMarket\\FuturesMarket.png"},
            {8316, RunesPath + "Inspiration\\MinionDematerializer\\MinionDematerializer.png"},
            {8345, RunesPath + "Inspiration\\BiscuitDelivery\\BiscuitDelivery.png"}},

            Slot3 = new Dictionary<int, string>()
            {{8347, RunesPath + "Inspiration\\CosmicInsight\\CosmicInsight.png"},
            {8410, RunesPath + "Resolve\\ApproachVelocity\\ApproachVelocity.png"},
            {8352, RunesPath + "Inspiration\\TimeWarpTonic\\TimeWarpTonic.png"}}
        };

        // Stats has no keystones - just 3 slots
        internal static readonly RuneCategory Stats = new RuneCategory()
        {
            Slot1 = new Dictionary<int, string>()
            {{5008, RunesPath + "Stats\\StatModsAdaptiveForceIcon.png"},
            {5005, RunesPath + "Stats\\StatModsAttackSpeedIcon.png"},
            {5007, RunesPath + "Stats\\StatModsCDRScalingIcon.png"}},

            Slot2 = new Dictionary<int, string>()
            {{5008, RunesPath + "Stats\\StatModsAdaptiveForceIcon.png"},
            {5002, RunesPath + "Stats\\StatModsArmorIcon.png"},
            {5003, RunesPath + "Stats\\StatModsMagicResIcon.png"}},

            Slot3 = new Dictionary<int, string>()
            {{5001, RunesPath + "Stats\\StatModsHealthScalingIcon.png"},
            {5002, RunesPath + "Stats\\StatModsArmorIcon.png"},
            {5003, RunesPath + "Stats\\StatModsMagicResIcon.png"}}
        };

        internal static RuneCategory GetRuneCategoryWithId(int id)
        {
            if (Precision.Id == id) return Precision;
            if (Domination.Id == id) return Domination;
            if (Sorcery.Id == id) return Sorcery;
            if (Resolve.Id == id) return Resolve;
            if (Inspiration.Id == id) return Inspiration;
            else return Stats;
        }

        internal static bool IsMatch(int id, string image, int statRow)
        {
            var category =
                Precision.Contains(id) ? Precision :
                Domination.Contains(id) ? Domination :
                Sorcery.Contains(id) ? Sorcery :
                Resolve.Contains(id) ? Resolve :
                Inspiration.Contains(id) ? Inspiration :
                Stats;

            // Handle case where no id is selected
            if (id == 0) return false;

            // Handle stats
            if (statRow != 0)
            {
                return statRow == 1 ? category.Slot1.GetValueOrDefault(id) == image :
                    statRow == 2 ? category.Slot2.GetValueOrDefault(id) == image :
                    category.Slot3.GetValueOrDefault(id) == image;
            }
            // Handle runes
            else
            {
                return category.Keystones.GetValueOrDefault(id) == image ||
                   category.Slot1.GetValueOrDefault(id) == image ||
                   category.Slot2.GetValueOrDefault(id) == image ||
                   category.Slot3.GetValueOrDefault(id) == image;
            }
        }

        internal struct RuneCategory
        {
            internal int Id;
            internal string Image;

            internal Dictionary<int, string> Keystones;
            internal Dictionary<int, string> Slot1;
            internal Dictionary<int, string> Slot2;
            internal Dictionary<int, string> Slot3;

            internal bool Contains(int id)
            {
                if ((Keystones != null && Keystones.ContainsKey(id)) ||
                    Slot1.ContainsKey(id) ||
                    Slot2.ContainsKey(id) ||
                    Slot3.ContainsKey(id)) return true;

                return false;
            }
        }
    }
}
