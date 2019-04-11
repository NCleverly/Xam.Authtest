using System;
using System.Collections.Generic;
using System.Net;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Newtonsoft.Json;
using System.Reflection;
using Xamarin.Forms;

#if __ANDROID__
using Android.Widget;
#endif
#if __IOS__
using Foundation;
#endif

namespace Xamarin.Forms.CommonCore
{

    public enum DeviceOS
    {
        IOS,
        ANDROID
    }
    public partial class CoreSettings
    {
        public const string MasterDetailIsPresented = "IsPresented";
        public const string LoadResources = "loadresources";
        public const string ReleaseResources = "releaseresource";

        public static string CurrentBuid { get; set; } = "dev";
        public static INavigation AppNav { get; set; }
        public static Size ScreenSize { get; set; }
        public static List<string> NotificationTags { get; set; } = new List<string>();

        public static T On<T>(params T[] parameters)
        {
            T obj = default(T);

            switch (Device.RuntimePlatform.ToUpper())
            {
                case "IOS":
                    if (parameters.Length > 0)
                        obj = parameters[0];
                    break;
                case "ANDROID":
                    if (parameters.Length > 1)
                        obj = parameters[1];
                    break;
                default:
                    if (parameters.Length > 2)
                        obj = parameters[2];
                    break;
            }

            return obj;
        }

        public static DeviceOS OS
        {
            get
            {
#if __ANDROID__
                return DeviceOS.ANDROID;
#else
                return DeviceOS.IOS;
#endif
            }
        }
        private static ISettings _appSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static CoreConfiguration Config
        {
            get { return AppData.Settings; }
        }

        /// <summary>
        /// Application Installation ID
        /// </summary>
        /// <value>The installation identifier.</value>
        public static string InstallationId
        {
            get
            {
                var id = _appSettings.GetValueOrDefault("InstallationId", null);

                if (string.IsNullOrEmpty(id))
                {
                    id = Guid.NewGuid().ToString();
                    _appSettings.AddOrUpdateValue("InstallationId", id);
                }

                return id;
            }
            set { _appSettings.AddOrUpdateValue("InstallationId", value); }
        }


        /// <summary>
        /// Application Data Sync Time for offline sync with sql
        /// </summary>
        /// <value>The installation identifier.</value>
        public static long SyncTimeStamp
        {
            get
            {
                var id = _appSettings.GetValueOrDefault("SyncTimeStamp", 0L);

                if (id == default(long))
                {
                    id = DateTime.UtcNow.AddDays(-30).Ticks;
                    _appSettings.AddOrUpdateValue("SyncTimeStamp", id);
                }

                return id;
            }
            set { _appSettings.AddOrUpdateValue("SyncTimeStamp", value); }
        }



#if __ANDROID__
        public static int AppIcon { get; set; }
        public static int SearchView { get; set; }
        public static string DeviceToken { get; set; }
#endif

#if __IOS__
        public static NSData DeviceToken { get; set; }
        //public static bool PrefersLargeTitles { get; set; }
#endif

        internal class AppData
        {
            //public static AppData Instance = new AppData();
            static AppData()
            {
                Load();
            }

            public static void Reload()
            {
                CoreDependencyService.DisposeServices();
                CoreDependencyService.ReleaseAllResources();
                Load();
                CoreDependencyService.ReloadAllResources();
            }

            public static CoreConfiguration Settings { get; private set; }


            private static void Load()
            {
                Settings = new CoreConfiguration();
                string fileName = null;
                fileName = $"config.{CoreSettings.CurrentBuid}.json";

                var response = ResourceLoader.GetEmbeddedResourceString(Assembly.GetAssembly(typeof(ResourceLoader)), fileName);
                if (response.Error == null)
                {
                    try
                    {
                        var root = JsonConvert.DeserializeObject<CoreConfiguration>(response.Response);
                        if (root != null)
                            Settings = root;
                    }
                    catch (Exception ex)
                    {
                        ex.ConsoleWrite();
                    }

                }
                else
                {
                    response.Error?.ConsoleWrite();
                }
            }
        }

    }

}
