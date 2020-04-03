using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SinglePageUI
{
    public partial class App : Application
    {
        public App()
        {
            NavigationPage navigationPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.CadetBlue,
                BarTextColor = Color.White
            };

            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
