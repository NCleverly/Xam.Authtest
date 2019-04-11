#if __ANDROID__
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CoreTelephonyPage), typeof(CoreTelephonyPageRenderer))]
namespace Xamarin.Forms.CommonCore
{
    public class CoreTelephonyPageRenderer : PageRenderer
    {

        public CoreTelephonyPageRenderer(Context ctx):base(ctx)
        {

        }
    }
}
#endif
