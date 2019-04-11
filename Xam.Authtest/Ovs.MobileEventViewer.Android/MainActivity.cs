using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Microsoft.AppCenter.Push;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using Android.Content;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Xam.Authtest.Droid
{
    [Activity(Label = "Xam.Authtest", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            InitGlobalLibraries();
            
            LoadApplication(new App());
        }

        private void InitGlobalLibraries()
        {
            CachedImageRenderer.Init(false);

        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }

    }
}

