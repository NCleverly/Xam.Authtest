using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Push;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms.CommonCore;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using Xamarin.Forms;
using MonkeyCache;
using MonkeyCache.SQLite;

namespace Xam.Authtest
{
    public class App : Application
    {
        public static string ApplicationID = "";
        public static string commonAuthority = "https://login.windows.net/common";
        public static string tenant = "";
        public static string tenanturl = String.Format("https://login.microsoftonline.com/{0}", tenant);
        public static string ReturnUri = "";
        public const string GraphResourceUri = "https://graph.windows.net";
        public AuthenticationResult AuthenticationResult = null;
        
        public App()
        {
            try
            {
                Barrel.ApplicationId = "Xam.Authtest";
            }
            catch (Exception ex)
            {
                throw;
            }
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
            MainPage.SizeChanged += AppScreenSizeChanged;
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
        }

        protected override void OnSleep()
        {
            MainPage.SizeChanged -= AppScreenSizeChanged;
            CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
        }

        protected override void OnResume()
        {
            MainPage.SizeChanged += AppScreenSizeChanged;
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
        }

        private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs args)
        {
            //CrossConnectivity.Current. = args.IsConnected;
        }

        private void AppScreenSizeChanged(object sender, EventArgs args)
        {
            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
        }
    }
}