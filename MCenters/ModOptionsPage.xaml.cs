using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCenters
{
    /// <summary>
    /// Interaction logic for ModOptionsPage.xaml
    /// </summary>
    public partial class ModOptionsPage : Page
    {
        (string Title, string Description, string LearnMoreLink) DllMethodOnlineResources = ("DLL Method (Online)", "This method uses cracked dlls that are provided by M Centers and third-parties (if enabled) using internet access. Third-party options can be enabled in settings and selected by you.", "https://mcenters.net/Methods/DLL-Method-Online/");
        (string Title, string Description, string LearnMoreLink) DllMethodAutoPatchResources = (  "DLL Method (Auto-Patch)",  "This method is the same as the previous method, DLL Method (Online), but instead of going online to get a cracked DLL, it creates a DLL from your system, which doesn't need internet, so it is safer than the previous method.", "https://mcenters.net/Methods/DLL-Method-Auto-Patch/");
        (string Title, string Description, string LearnMoreLink) DllMethodNonPermanentResources = ( "DLL Method (Auto-Patch, Non-Permanent)",  "This method is the same as DLL Method Auto-Patch except that it now doesn't modify system files and is non-permanent, since it creates a cracked version of DLLs like DLL Method Auto-Patch, but injects into the Minecraft process, instead of replacing system DLLs.", "https://mcenters.net/Methods/DLL-Method-Non-Permanent/");
        (string Title, string Description, string LearnMoreLink) MCenters5Resources = ( "Memory Injection", "This method uses the method that was used previously in M Centers 5.0, this method only works on newer minecraft versions starting from 1.19.20 and later, while being non-permanent, being one of the safest methods that exist. ", "https://mcenters.net/Methods/M-Centers-5/");
        (string Title, string Description, string LearnMoreLink) HookMethodResources = ( "DLL Hooking Method", "This method is non-permanent and makes use of debugging and hooking into the Minecraft process to unlock the game without modifying any system files.", "https://mcenters.net/Methods/DLL-Hook-Method/");
        SolidColorBrush DefaultBackground;
        SolidColorBrush ActiveBackground;
        HoverableButton ActiveButton;
        public ModOptionsPage()
        {
            InitializeComponent();
            DefaultBackground=dll_online_button.Background as SolidColorBrush;
            ActiveBackground = new SolidColorBrush(DefaultBackground.Color) { Opacity = 0.2 };
            var config = Config.Load();
            var button = ModOptionToButton(config.SelectedMod);
            if (button != null) {
                button.Background = ActiveBackground;
                ActiveButton = button;
                Screens.MainWindow.modOptionsButton.Content = button.Content;
            }
            SetCurrentResources(config.SelectedMod);
            SetMainPageButtonsText(config.SelectedMod);
            backButton.ConnectedImage = backIcon;
            dll_online_button.ConnectedImage = dll_online_image;
            dll_auto_patch_button.ConnectedImage = dll_auto_patch_image;
            dll_non_permanent_button.ConnectedImage = dll_non_permanent_image;
            mcenter_5_button.ConnectedImage = mcenter_5_image;
            hook_button.ConnectedImage = hook_image;
        }
        HoverableButton ModOptionToButton(ModOptions mod)
        {
            switch(mod)
            {
                case ModOptions.DllMethodOnline:
                    return dll_online_button;
                case ModOptions.DllMethodAutoPatch:
                    return dll_auto_patch_button;
                case ModOptions.DllMethodAutoPatchNonPermanent:
                    return dll_non_permanent_button;
                case ModOptions.MCenters5:
                    return mcenter_5_button;
                case ModOptions.HookMethod:
                    return hook_button;

            }
            return null;
        }
        (string Title, string Description, string LearnMoreLink)? ModOptionToResources(ModOptions mod)
        {
            switch (mod)
            {
                case ModOptions.DllMethodOnline:
                    return DllMethodOnlineResources;
                case ModOptions.DllMethodAutoPatch:
                    return DllMethodAutoPatchResources;
                case ModOptions.DllMethodAutoPatchNonPermanent:
                    return DllMethodNonPermanentResources;
                case ModOptions.MCenters5:
                    return MCenters5Resources;
                case ModOptions.HookMethod:
                    return HookMethodResources;

            }
            return null;
        }

        (string InstallButtonText, string UninstallButtonText)? ModOptionToMainPageButtonTexts(ModOptions mod)
        {
            switch (mod)
            {
                case ModOptions.DllMethodOnline:
                    return ("Install Cracked DLL","Uninstall Cracked DLL");
                case ModOptions.DllMethodAutoPatch:
                    return ("Install Cracked DLL", "Uninstall Cracked DLL");
                case ModOptions.DllMethodAutoPatchNonPermanent:
                    return ("Launch Minecraft", "N/A");
                case ModOptions.MCenters5:
                    return ("Launch Minecraft: Full", "Launch Minecraft: Trial");
                case ModOptions.HookMethod:
                    return ("Launch Minecraft", "N/A");

            }
            return null;
        }
        public void SetMainPageButtonsText(ModOptions option)
        {
            var texts = ModOptionToMainPageButtonTexts(option);
            if (texts == null) return;
            var win = Screens.MainWindow;
            win.installButton.Content=texts?.InstallButtonText;
            win.uninstallButton.Content=texts?.UninstallButtonText;
        }
        public ModOptions CurrentlyFocusedOption;

        Uri CurrentLearnMoreLink { get; set; }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {


            Screens.SetScreen(Screens.MainScreen);
        }
        void SetCurrentResources(ModOptions option)
        {
            var resource = ModOptionToResources(option);
            if (resource == null) return;
            TitleBox.Text= resource?.Title;
            DescriptionBox.Text= resource?.Description;
            CurrentLearnMoreLink= new Uri(resource?.LearnMoreLink);
            CurrentlyFocusedOption = option;
        }

        private void LearnMore_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentLearnMoreLink == null) return;
            Process.Start(CurrentLearnMoreLink.ToString());
        }

        private void DllOnlineButton_Clicked(object sender, RoutedEventArgs e)
        {
            SetCurrentResources(ModOptions.DllMethodOnline);
        }

        private void DllAutoPatchButton_Clicked(object sender, RoutedEventArgs e)
        {
            SetCurrentResources(ModOptions.DllMethodAutoPatch);

        }

        private void DllNonPermanentButton_Clicked(object sender, RoutedEventArgs e)
        {
            SetCurrentResources(ModOptions.DllMethodAutoPatchNonPermanent);

        }

        private void MCenters5Button_Clicked(object sender, RoutedEventArgs e)
        {
            SetCurrentResources(ModOptions.MCenters5);
        }

        private void HookButton_Clicked(object sender, RoutedEventArgs e)
        {
            SetCurrentResources(ModOptions.HookMethod);
        }

        private void ActivateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = ModOptionToButton(CurrentlyFocusedOption);
            if (button == null|| ReferenceEquals(button, ActiveButton)) return;

            ActiveButton.Background = DefaultBackground;

            button.Background = ActiveBackground;
            ActiveButton = button;
            Screens.MainWindow.modOptionsButton.Content = button.Content;

            SetMainPageButtonsText(CurrentlyFocusedOption);

            var config = Config.Load();
            config.SelectedMod=CurrentlyFocusedOption;
            config.Save();
        }
    }
}
