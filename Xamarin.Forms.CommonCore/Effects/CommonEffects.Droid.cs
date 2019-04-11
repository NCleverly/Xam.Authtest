#if __ANDROID__
using System;
using Android.Graphics.Drawables;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Graphics = Android.Graphics;
using Android.Views;
using OS = Android.OS;
using Android.Graphics;
using Android.Support.V7.Widget;

[assembly: ExportEffect(typeof(ViewShadow), "ViewShadow")]
[assembly: ExportEffect(typeof(HideTableSeparator), "HideTableSeparator")]
namespace Xamarin.Forms.CommonCore
{

	public class HideTableSeparator : PlatformEffect
	{
		protected override void OnAttached()
		{
			if (Control != null)
			{
				var listView = Control as global::Android.Widget.ListView;
				//listView.Divider = null;
				listView.Divider = new ColorDrawable(Graphics.Color.Transparent);
				listView.DividerHeight = 0;
			}
		}

		protected override void OnDetached()
		{

		}
	}

	public class ViewShadow : PlatformEffect
	{


		protected override void OnAttached()
		{
			//Control.Elevation = 10;
			//Control.TranslationZ = 20;
			//Control.SetBackgroundColor(Color.Red.ToAndroid());

			//var v = this.Element as View;
			//var renderer = RendererFactory.GetRenderer(v);
			//renderer.ViewGroup.SetClipToPadding(false);

		}

		protected override void OnDetached()
		{

		}
	}

}
#endif

