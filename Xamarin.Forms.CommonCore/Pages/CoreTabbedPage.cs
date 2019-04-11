using System;
using Xamarin.Forms;

namespace Xamarin.Forms.CommonCore
{
    public class CoreTabbedPage : TabbedPage
    {
		public static readonly BindableProperty SelectedForegroundColorProperty =
			BindableProperty.Create("SelectedForegroundColor",
							typeof(Color),
							typeof(CoreTabbedPage),
							Color.Black);

		public Color SelectedForegroundColor
		{
			get { return (Color)this.GetValue(SelectedForegroundColorProperty); }
			set { this.SetValue(SelectedForegroundColorProperty, value); }
		}

		public static readonly BindableProperty UnSelectedForegroundColorProperty =
			BindableProperty.Create("UnSelectedForegroundColor",
							typeof(Color),
							typeof(CoreTabbedPage),
							Color.Black);

		public Color UnSelectedForegroundColor
		{
			get { return (Color)this.GetValue(UnSelectedForegroundColorProperty); }
			set { this.SetValue(UnSelectedForegroundColorProperty, value); }
		}

		public static readonly BindableProperty TabBackgroundColorProperty =
			BindableProperty.Create("TabBackgroundColor",
							typeof(Color),
							typeof(CoreTabbedPage),
					        Color.Default);
        
		public Color TabBackgroundColor
		{
			get { return (Color)this.GetValue(TabBackgroundColorProperty); }
			set { this.SetValue(TabBackgroundColorProperty, value); }
		}
    }
}
