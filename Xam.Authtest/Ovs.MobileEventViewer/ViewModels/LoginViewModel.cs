using Microsoft.AppCenter.Crashes;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.CommonCore;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.AppCenter;
using MonkeyCache.SQLite;
//using Xam.Authtest.Controls;
using Xam.Authtest.Views;

namespace Xam.Authtest.ViewModels
{
    public class LoginViewModel : CoreViewModel
    {
        //private varibles
        private string _userName = string.Empty;

        private string _password = string.Empty;
        private JObject user = null;

        //public properties
        public string UserName
        {
            get => _userName;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _userName = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _password = value;
            }
        }

        public override async void OnViewMessageReceived(string key, object obj)
        {
            switch (key)
            {
                case CoreSettings.LoadResources:
                    await LoadResourcesAsync().ConfigureAwait(false);
                    break;
            }
        }

        private async Task LoadResourcesAsync()
        {
            //this.LoadingMessageOverlay = "Verifying Credentials . . .";
        }

        public async Task SubmitCommandAsync()
        {
            try
            {
                //IsLoadingOverlay = true;
                //var data = await DependencyService.Get<AuthenticationService>().Authenticate(App.tenanturl, App.GraphResourceUri, App.ApplicationID, App.ReturnUri);
                //AuthenticationResult authenticationResult = data;
                //if (authenticationResult.UserInfo != null)
                //{
                //    CustomProperties properties = new CustomProperties();
                //    Application.Current.MainPage = new NavigationPage(new SignedInPage());
                //    IsLoadingOverlay = false;
                //}
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}