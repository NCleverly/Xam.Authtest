#if __IOS__
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using Xam.Authtest.Controls;
using Xam.Authtest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

[assembly: Dependency(typeof(AuthenticationService))]
namespace Xam.Authtest.Controls
{
    public class AuthenticationService : IAuthenticationService
    {
        HttpService httpService = (HttpService)CoreDependencyService.GetService<IHttpService, HttpService>(true);
        public async Task<AuthenticationResult> Authenticate(string tenantUrl, string graphResourceUri, string ApplicationID, string returnUri)
        {
            try
            {
                var authContext = new AuthenticationContext(tenantUrl);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault().Authority);
                var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
                var uri = new Uri(returnUri);
                var platformParams = new PlatformParameters(controller);
                var authResult = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, new Uri(returnUri), platformParams);
                
                return authResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}

#endif