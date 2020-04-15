using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Themes;
using XMart.Util;

namespace XMart.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        private bool darkModeIsToggled;   //Comment
        public bool DarkModeIsToggled
        {
            get { return darkModeIsToggled; }
            set { SetProperty(ref darkModeIsToggled, value); }
        }

        public Command BackCommand { get; set; }
        public Command ThemeCommand { get; set; }

        public SettingViewModel()
        {
            DarkModeIsToggled = GlobalVariables.DarkMode;
            
            ThemeCommand = new Command(() =>
            {
                GlobalVariables.DarkMode = DarkModeIsToggled;
                Theme theme = GlobalVariables.DarkMode ? Theme.Dark : Theme.Light;

                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                if (mergedDictionaries != null)
                {
                    mergedDictionaries.Clear();

                    switch (theme)
                    {
                        case Theme.Dark:
                            mergedDictionaries.Add(new DarkTheme());
                            break;
                        case Theme.Light:
                        default:
                            mergedDictionaries.Add(new LightTheme());
                            break;
                    }
                }
            }, () => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }
    }
}
