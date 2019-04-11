#if __ANDROID__
using System;
using System.ComponentModel;
using Android.Content.Res;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Xamarin.Forms;
using Attribute = Android.Resource.Attribute;
using Drawable = Android.Support.V4.Graphics.Drawable;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Android.Content;

[assembly: ExportRenderer(typeof(CoreTabbedPage), typeof(CoreTabbedPageRenderer))]
namespace Xamarin.Forms.CommonCore
{
    public class CoreTabbedPageRenderer :TabbedPageRenderer
    {
		private bool setup;
		private ViewPager pager;
		private TabLayout layout;
        private CoreTabbedPage tabbedPage;

        public CoreTabbedPageRenderer(Context ctx) : base(ctx)
        {

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (setup)
				return;

			if (e.PropertyName == "Renderer")
			{
                tabbedPage = (CoreTabbedPage)Element;
				pager = (ViewPager)ViewGroup.GetChildAt(0);
				layout = (TabLayout)ViewGroup.GetChildAt(1);
				setup = true;

				ColorStateList colors = CreateColorState();

                layout.SetTabTextColors(tabbedPage.UnSelectedForegroundColor.ToAndroid(), tabbedPage.SelectedForegroundColor.ToAndroid());

                if (tabbedPage.TabBackgroundColor != Color.Default)
                    layout.SetBackgroundColor(tabbedPage.TabBackgroundColor.ToAndroid());
           

				for (int i = 0; i < layout.TabCount; i++)
				{
					var tab = layout.GetTabAt(i);
					var icon = tab.Icon;
					if (icon != null)
					{
						icon = Drawable.DrawableCompat.Wrap(icon);
						Drawable.DrawableCompat.SetTintList(icon, colors);
					}
				}

			}
		}
		private ColorStateList CreateColorState()
		{
			int[][] states = new int[][] {
				new int[] { Attribute.StateSelected }, // enabled
                new int[] { -Attribute.StateSelected } // disabled
			};
			int[] colors = new int[] {
               
				tabbedPage.SelectedForegroundColor.ToAndroid(),
				tabbedPage.UnSelectedForegroundColor.ToAndroid()
			};
			return new ColorStateList(states, colors);
		}
    }
}
#endif
