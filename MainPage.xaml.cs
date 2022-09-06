using System.Drawing;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;
using Size = System.Drawing.Size;
using Color = System.Drawing.Color;
using System.Runtime.InteropServices;
using static Lethality.RuneManager; // TODO: Move to its own file

namespace Lethality;

public partial class MainPage : ContentPage
{
	private RuneManager RuneManager;

    private RunePage PreviousRunePage;
    private RunePage CurrentRunePage;
    
    private int LastSecondaryRowClicked = 0;

	public MainPage()
	{
        InitializeComponent();

        this.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(36, 41, 46);

        RuneManager = new RuneManager(OnRunePageUpdate);

        // Primary
        RuneImage.Source = RuneDictionary.RuneImage;
        PrecisionButton.Source = RuneDictionary.Precision.Image;
        DominationButton.Source = RuneDictionary.Domination.Image;
        SorceryButton.Source = RuneDictionary.Sorcery.Image;
        ResolveButton.Source = RuneDictionary.Resolve.Image;
		InspirationButton.Source = RuneDictionary.Inspiration.Image;

        // Secondary
        SecondaryRuneImage.Source = RuneDictionary.RuneImage;
        SecondaryPrecisionButton.Source = RuneDictionary.Precision.Image;
        SecondaryDominationButton.Source = RuneDictionary.Domination.Image;
        SecondarySorceryButton.Source = RuneDictionary.Sorcery.Image;
        SecondaryResolveButton.Source = RuneDictionary.Resolve.Image;
        SecondaryInspirationButton.Source = RuneDictionary.Inspiration.Image;
    }

    // TODO: Refactor
    private void OnRuneCategoryClick(object sender, EventArgs e)
	{
        var button = (ImageButton)sender;

        int categoryId;

        if (button == PrecisionButton) categoryId = RuneDictionary.Precision.Id;
        else if (button == DominationButton) categoryId = RuneDictionary.Domination.Id;
        else if (button == SorceryButton) categoryId = RuneDictionary.Sorcery.Id;
        else if (button == ResolveButton) categoryId = RuneDictionary.Resolve.Id;
        else categoryId = RuneDictionary.Inspiration.Id;

        PreviousRunePage = CurrentRunePage;

        CurrentRunePage.PrimaryCategoryId = categoryId;
        CurrentRunePage.Keystones.selected = 0;
        CurrentRunePage.Slot1Runes.selected = 0;
        CurrentRunePage.Slot2Runes.selected = 0;
        CurrentRunePage.Slot3Runes.selected = 0;

        RuneManager.SendRunePageToClient(CurrentRunePage);
	}

    private void OnSecondaryRuneCategoryClick(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        int categoryId;

        if (button == SecondaryPrecisionButton) categoryId = RuneDictionary.Precision.Id;
        else if (button == SecondaryDominationButton) categoryId = RuneDictionary.Domination.Id;
        else if (button == SecondarySorceryButton) categoryId = RuneDictionary.Sorcery.Id;
        else if (button == SecondaryResolveButton) categoryId = RuneDictionary.Resolve.Id;
        else categoryId = RuneDictionary.Inspiration.Id;

        PreviousRunePage = CurrentRunePage;

        CurrentRunePage.SecondaryCategoryId = categoryId;
        CurrentRunePage.SecondarySlot1Runes.selected = 0;
        CurrentRunePage.SecondarySlot2Runes.selected = 0;
        CurrentRunePage.SecondarySlot3Runes.selected = 0;

        RuneManager.SendRunePageToClient(CurrentRunePage);
    }

    private void OnKeystoneClick(object sender, EventArgs e) 
	{
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.PrimaryCategoryId).Keystones;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        // Edit current rune page and send to controller
        PreviousRunePage = CurrentRunePage;
        CurrentRunePage.Keystones.selected = runeId;
        RuneManager.SendRunePageToClient(CurrentRunePage);
    }

    private void OnSlot1Click(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.PrimaryCategoryId).Slot1;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        // Edit current rune page and send to controller
        PreviousRunePage = CurrentRunePage;
        CurrentRunePage.Slot1Runes.selected = runeId;
        RuneManager.SendRunePageToClient(CurrentRunePage);
    }

    private void OnSlot2Click(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.PrimaryCategoryId).Slot2;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        // Edit current rune page and send to controller
        PreviousRunePage = CurrentRunePage;
        CurrentRunePage.Slot2Runes.selected = runeId;
        RuneManager.SendRunePageToClient(CurrentRunePage);
    }

    private void OnSlot3Click(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.PrimaryCategoryId).Slot3;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        // Edit current rune page and send to controller
        PreviousRunePage = CurrentRunePage;
        CurrentRunePage.Slot3Runes.selected = runeId;
        RuneManager.SendRunePageToClient(CurrentRunePage);
    }

    private void OnSecondarySlot1Click(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.SecondaryCategoryId).Slot1;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        PreviousRunePage = CurrentRunePage;

        // Trivial case: row already has selected rune
        if (CurrentRunePage.SecondarySlot1Runes.selected != 0)
        {
            // Edit current rune page and send to controller
            CurrentRunePage.SecondarySlot1Runes.selected = runeId;
            RuneManager.SendRunePageToClient(CurrentRunePage);
        }
        // Complex case: rune does not currently have a selected rune
        else
        {
            // Previous row clicked not yet recorded
            if (LastSecondaryRowClicked == 0)
            {
                // Change highest row
                CurrentRunePage.SecondarySlot2Runes.selected = 0;

                // Edit current rune page and send to controller
                CurrentRunePage.SecondarySlot1Runes.selected = runeId;
                RuneManager.SendRunePageToClient(CurrentRunePage);
            }
            else
            {
                // Change row not last changed
                if (LastSecondaryRowClicked != 2)
                    CurrentRunePage.SecondarySlot2Runes.selected = 0;
                else
                    CurrentRunePage.SecondarySlot3Runes.selected = 0;

                // Edit current rune page and send to controller
                CurrentRunePage.SecondarySlot1Runes.selected = runeId;
                RuneManager.SendRunePageToClient(CurrentRunePage);
            }
        }

        // Update the last row clicked
        LastSecondaryRowClicked = 1;
    }

    private void OnSecondarySlot2Click(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.SecondaryCategoryId).Slot2;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        PreviousRunePage = CurrentRunePage;

        // Trivial case: row already has selected rune
        if (CurrentRunePage.SecondarySlot2Runes.selected != 0)
        {
            // Edit current rune page and send to controller
            CurrentRunePage.SecondarySlot2Runes.selected = runeId;
            RuneManager.SendRunePageToClient(CurrentRunePage);
        }
        // Complex case: rune does not currently have a selected rune
        else
        {
            // Previous row clicked not yet recorded
            if (LastSecondaryRowClicked == 0)
            {
                // Change highest row
                CurrentRunePage.SecondarySlot1Runes.selected = 0;

                // Edit current rune page and send to controller
                CurrentRunePage.SecondarySlot2Runes.selected = runeId;
                RuneManager.SendRunePageToClient(CurrentRunePage);
            }
            else
            {
                // Change row not last changed
                if (LastSecondaryRowClicked != 1)
                    CurrentRunePage.SecondarySlot1Runes.selected = 0;
                else
                    CurrentRunePage.SecondarySlot3Runes.selected = 0;

                // Edit current rune page and send to controller
                CurrentRunePage.SecondarySlot2Runes.selected = runeId;
                RuneManager.SendRunePageToClient(CurrentRunePage);
            }
        }

        // Update the last row clicked
        LastSecondaryRowClicked = 2;
    }

    private void OnSecondarySlot3Click(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.SecondaryCategoryId).Slot3;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        PreviousRunePage = CurrentRunePage;

        // Trivial case: row already has selected rune
        if (CurrentRunePage.SecondarySlot3Runes.selected != 0)
        {
            // Edit current rune page and send to controller
            CurrentRunePage.SecondarySlot3Runes.selected = runeId;
            RuneManager.SendRunePageToClient(CurrentRunePage);
        }
        // Complex case: rune does not currently have a selected rune
        else
        {
            // Previous row clicked not yet recorded
            if (LastSecondaryRowClicked == 0)
            {
                // Change highest row
                CurrentRunePage.SecondarySlot1Runes.selected = 0;

                // Edit current rune page and send to controller
                CurrentRunePage.SecondarySlot1Runes.selected = runeId;
                RuneManager.SendRunePageToClient(CurrentRunePage);
            }
            else
            {
                // Change row not last changed
                if (LastSecondaryRowClicked != 1)
                    CurrentRunePage.SecondarySlot1Runes.selected = 0;
                else
                    CurrentRunePage.SecondarySlot2Runes.selected = 0;

                // Edit current rune page and send to controller
                CurrentRunePage.SecondarySlot3Runes.selected = runeId;
                RuneManager.SendRunePageToClient(CurrentRunePage);
            }
        }

        // Update the last row clicked
        LastSecondaryRowClicked = 3;
    }

    private void OnSlot1StatClick(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.Slot1Stats.selected).Slot1;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        // Edit current rune page and send to controller
        PreviousRunePage = CurrentRunePage;
        CurrentRunePage.Slot1Stats.selected = runeId;
        RuneManager.SendRunePageToClient(CurrentRunePage);
    }

    private void OnSlot2StatClick(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.Slot2Stats.selected).Slot2;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        // Edit current rune page and send to controller
        PreviousRunePage = CurrentRunePage;
        CurrentRunePage.Slot2Stats.selected = runeId;
        RuneManager.SendRunePageToClient(CurrentRunePage);
    }

    private void OnSlot3StatClick(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;

        var runes = RuneDictionary.GetRuneCategoryWithId(CurrentRunePage.Slot3Stats.selected).Slot3;

        // Get id of rune pressed
        int runeId = runes.FirstOrDefault(
            rune => rune.Value.Split('\\').Last() == button.Source.ToString().Split('\\').Last().Split("Edit").First() + ".png").Key;

        // Edit current rune page and send to controller
        PreviousRunePage = CurrentRunePage;
        CurrentRunePage.Slot3Stats.selected = runeId;
        RuneManager.SendRunePageToClient(CurrentRunePage);
    }

    internal void OnRunePageUpdate(RunePage runePage)
    {
        // Initial update
        if (CurrentRunePage.Equals(default(RunePage)))
            PreviousRunePage = new RunePage();

        CurrentRunePage = runePage;
        UpdateRunePageView(PreviousRunePage);
    }

    private void UpdateRunePageView(RunePage previousRunePage)
	{
        #region Primary
        bool PrimaryCategorySwap = previousRunePage.PrimaryCategoryId != CurrentRunePage.PrimaryCategoryId;

        #region Keystones
        if (PrimaryCategorySwap || 
            !CurrentRunePage.Keystones.Equals(previousRunePage.Keystones))
        {
            var keystoneImages = SetupRuneImages(
                (int)Keystone1.WidthRequest, 
                CurrentRunePage.Keystones.selected, 
                CurrentRunePage.Keystones.images);

            Keystone1.Source = keystoneImages[0];
            Keystone2.Source = keystoneImages[1];
            Keystone3.Source = keystoneImages[2];
            if (keystoneImages.Count == 4)
            {
                Keystone4.IsEnabled = true;
                Keystone4.IsVisible = true;
                Keystone4.Source = keystoneImages[3];
            }
            else
            {
                Keystone4.IsEnabled = false;
                Keystone4.IsVisible = false;
            }
        }
        #endregion

        #region Slot 1
        if (PrimaryCategorySwap ||
            !CurrentRunePage.Slot1Runes.Equals(previousRunePage.Slot1Runes))
        {
            var slot1Images = SetupRuneImages(
                (int)Rune1Slot1.WidthRequest, 
                CurrentRunePage.Slot1Runes.selected, 
                CurrentRunePage.Slot1Runes.images);

            Rune1Slot1.Source = slot1Images[0];
            Rune2Slot1.Source = slot1Images[1];
            Rune3Slot1.Source = slot1Images[2];
        }
        #endregion

        #region Slot 2
        if (PrimaryCategorySwap ||
            !CurrentRunePage.Slot2Runes.Equals(previousRunePage.Slot2Runes))
        {
            var slot2Images = SetupRuneImages(
                (int)Rune1Slot2.WidthRequest, 
                CurrentRunePage.Slot2Runes.selected, 
                CurrentRunePage.Slot2Runes.images);

            Rune1Slot2.Source = slot2Images[0];
            Rune2Slot2.Source = slot2Images[1];
            Rune3Slot2.Source = slot2Images[2];
        }
        #endregion Slot 2

        #region Slot 3
        if (PrimaryCategorySwap ||
            !CurrentRunePage.Slot3Runes.Equals(previousRunePage.Slot3Runes))
        {
            var slot3Images = SetupRuneImages(
                (int)Rune1Slot3.WidthRequest, 
                CurrentRunePage.Slot3Runes.selected, 
                CurrentRunePage.Slot3Runes.images);

            Rune1Slot3.Source = slot3Images[0];
            Rune2Slot3.Source = slot3Images[1];
            Rune3Slot3.Source = slot3Images[2];
            if (slot3Images.Count == 4)
            {
                Rune4Slot3.IsEnabled = true;
                Rune4Slot3.IsVisible = true;
                Rune4Slot3.Source = slot3Images[3];
            }
            else
            {
                Rune4Slot3.IsEnabled = false;
                Rune4Slot3.IsVisible = false;
            }
        }
        #endregion
        #endregion

        #region Secondary
        bool SecondaryCategorySwap = previousRunePage.SecondaryCategoryId != CurrentRunePage.SecondaryCategoryId;

        #region Slot 1
        if (SecondaryCategorySwap ||
            !CurrentRunePage.SecondarySlot1Runes.Equals(previousRunePage.SecondarySlot1Runes))
        {
            var secondarySlot1Images = SetupRuneImages(
                (int)SecondaryRune1Slot1.WidthRequest, 
                CurrentRunePage.SecondarySlot1Runes.selected, 
                CurrentRunePage.SecondarySlot1Runes.images);

            SecondaryRune1Slot1.Source = secondarySlot1Images[0];
            SecondaryRune2Slot1.Source = secondarySlot1Images[1];
            SecondaryRune3Slot1.Source = secondarySlot1Images[2];
        }
        #endregion

        #region Slot 2
        if (SecondaryCategorySwap ||
            !CurrentRunePage.SecondarySlot2Runes.Equals(previousRunePage.SecondarySlot2Runes))
        {
            var secondarySlot2Images = SetupRuneImages(
                (int)SecondaryRune1Slot2.WidthRequest, 
                CurrentRunePage.SecondarySlot2Runes.selected, 
                CurrentRunePage.SecondarySlot2Runes.images);

            SecondaryRune1Slot2.Source = secondarySlot2Images[0];
            SecondaryRune2Slot2.Source = secondarySlot2Images[1];
            SecondaryRune3Slot2.Source = secondarySlot2Images[2];
        }
        #endregion

        #region Slot 3
        if (SecondaryCategorySwap ||
            !CurrentRunePage.SecondarySlot3Runes.Equals(previousRunePage.SecondarySlot3Runes))
        {
            var secondarySlot3Images = SetupRuneImages(
                (int)SecondaryRune1Slot3.WidthRequest, 
                CurrentRunePage.SecondarySlot3Runes.selected, 
                CurrentRunePage.SecondarySlot3Runes.images);

            SecondaryRune1Slot3.Source = secondarySlot3Images[0];
            SecondaryRune2Slot3.Source = secondarySlot3Images[1];
            SecondaryRune3Slot3.Source = secondarySlot3Images[2];
            if (secondarySlot3Images.Count == 4)
            {
                SecondaryRune4Slot3.IsEnabled = true;
                SecondaryRune4Slot3.IsVisible = true;
                SecondaryRune4Slot3.Source = secondarySlot3Images[3];
            }
            else
            {
                SecondaryRune4Slot3.IsEnabled = false;
                SecondaryRune4Slot3.IsVisible = false;
            }
        }
        #endregion
        #endregion

        #region Stats
        #region Slot 1
        if (SecondaryCategorySwap ||
            !CurrentRunePage.Slot1Stats.Equals(previousRunePage.Slot1Stats))
        {
            var slot1StatImages = SetupRuneImages(
                (int)Stat1Slot1.WidthRequest, 
                CurrentRunePage.Slot1Stats.selected, 
                CurrentRunePage.Slot1Stats.images, 1);

            Stat1Slot1.Source = slot1StatImages[0];
            Stat2Slot1.Source = slot1StatImages[1];
            Stat3Slot1.Source = slot1StatImages[2];
        }
        #endregion

        #region Slot 2
        if (SecondaryCategorySwap ||
            !CurrentRunePage.Slot2Stats.Equals(previousRunePage.Slot2Stats))
        {
            var slot2StatImages = SetupRuneImages(
                (int)Stat1Slot2.WidthRequest, 
                CurrentRunePage.Slot2Stats.selected, 
                CurrentRunePage.Slot2Stats.images, 2);

            Stat1Slot2.Source = slot2StatImages[0];
            Stat2Slot2.Source = slot2StatImages[1];
            Stat3Slot2.Source = slot2StatImages[2];
        }
        #endregion

        #region Slot 3
        if (SecondaryCategorySwap ||
            !CurrentRunePage.Slot3Stats.Equals(previousRunePage.Slot3Stats))
        {
            var slot3StatImages = SetupRuneImages(
                (int)Stat1Slot3.WidthRequest, 
                CurrentRunePage.Slot3Stats.selected, 
                CurrentRunePage.Slot3Stats.images, 3);

            Stat1Slot3.Source = slot3StatImages[0];
            Stat2Slot3.Source = slot3StatImages[1];
            Stat3Slot3.Source = slot3StatImages[2];
        }
        #endregion
        #endregion
    }

#pragma warning disable CA1416
    private List<string> SetupRuneImages(int size, int selected, List<string> sourceImages, int statRow = 0)
    {
        List<Bitmap> images = new List<Bitmap>();
        List<string> imagePaths = new List<string>();

        foreach (var image in sourceImages) try { images.Add((Bitmap)Image.FromFile(image)); } catch (Exception) { };

        for (int i = 0; i < images.Count; i++)
        {
            // Scale
            images[i] = new Bitmap(images[i], new Size(size, size));

            // Check if rune is not selected
            if (!RuneDictionary.IsMatch(selected, sourceImages[i], statRow))
            {
                // Greyscale if is not selected
                Rectangle rect = new Rectangle(0, 0, images[i].Width, images[i].Height);
                BitmapData imgData =
                    images[i].LockBits(rect, ImageLockMode.ReadWrite,
                    images[i].PixelFormat);

                IntPtr ptr = imgData.Scan0;

                int rgbValCount = Math.Abs(imgData.Stride) * images[i].Height / 4;
                int[] rgbVals = new int[rgbValCount];

                Marshal.Copy(ptr, rgbVals, 0, rgbValCount);

                for (int j = 0; j < rgbValCount; j++)
                {
                    Color c = Color.FromArgb(rgbVals[j]);

                    int r = c.R;
                    int g = c.G;
                    int b = c.B;

                    //int avg = (r + g + b) / 3;
                    int avg = (int)(0.3 * r + 0.59 * g + 0.11 * b);

                    rgbVals[j] = Color.FromArgb(c.A, avg, avg, avg).ToArgb();
                }

                Marshal.Copy(rgbVals, 0, ptr, rgbValCount);

                images[i].UnlockBits(imgData);
            }

            // Save
            string path = sourceImages[i].Split('.')[0] + $"Edit{(statRow == 0 ? "" : statRow)}.png";

            File.Delete(path);
            images[i].Save(path);
            imagePaths.Add(path);
        }

        return imagePaths;
    }
#pragma warning restore CA1416

}