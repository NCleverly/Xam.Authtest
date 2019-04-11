using Xam.Authtest.ViewModels;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms;

namespace Xam.Authtest
{
    public class LoginPage : CorePage<LoginViewModel>
    {
        public LoginPage()
        {
            Grid grid = new Grid();
            grid.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, true);
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            Style entry = new Style(typeof(Entry));

            entry.Setters.Add(new Setter
            {
                Property = View.MarginProperty,
                Value = "40,10"
            });
            StackLayout imageRow = new StackLayout
            {
                VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true),
                Children = {
                    new Image{
                    Source = "https://cdn.freebiesupply.com/logos/large/2x/xamarin-logo-png-transparent.png",
                    VerticalOptions = new LayoutOptions(LayoutAlignment.Center,true),
                        Aspect = Aspect.AspectFit,
                        HeightRequest = 137,
                    }
                }
            };
            //var username = new Entry
            //{
            //    VerticalOptions = new LayoutOptions(LayoutAlignment.End, true),
            //    Placeholder = "Login Name",
            //    Text = VM.UserName,
            //    TextColor = Color.DarkGray,
            //    Margin = new Thickness(40,10)
            //};
            //username.SetBinding(Entry.TextProperty,"UserName");
            //var password = new Entry
            //{
            //    VerticalOptions = new LayoutOptions(LayoutAlignment.End, true),
            //    Placeholder = "password",
            //    Text = VM.Password,
            //    IsPassword = true,
            //    TextColor = Color.DarkGray,
            //    Margin = new Thickness(40, 10)
            //};
            //password.SetBinding(Entry.TextProperty, "Password");
            var submit = new Button
            {
                Text = "Sign-in",
                Command = new CoreCommand((obj) => { VM.SubmitCommandAsync().ConfigureAwait(false); }),
                HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                WidthRequest = 240,
                VerticalOptions = new LayoutOptions(LayoutAlignment.Start, true),
                BackgroundColor = Color.FromHex("#184B7A"),
                TextColor = Color.White
            };
            StackLayout entryFieldsLayout = new StackLayout
            {
                VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true),
                Padding = new Thickness(2, 2, 0, 0),
                Children = { submit }
            };

            grid.Children.Add(imageRow, 0, 0);
            grid.Children.Add(entryFieldsLayout, 0, 1);

            Content = new StackLayout
            {
                Children = {
                    grid
                }
            };
        }
    }
}