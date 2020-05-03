using DatabaseBuilderWPF;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinGOT
{
    public partial class App : Application
    {
        string _dbPath;

        public static GotDatabase Database {
            get; set;
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }



        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
