using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xam.Authtest.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace Xam.Authtest.Views
{
    public class SignedInPage : CorePage<SignedInViewModel>
    {
        public SignedInPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms secured by Azure AD!" }
                }
            };
        }
    }
}