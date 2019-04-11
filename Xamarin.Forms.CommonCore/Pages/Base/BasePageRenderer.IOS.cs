#if __IOS__
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BasePages), typeof(BasePageRenderer))]
namespace Xamarin.Forms.CommonCore
{
    public class BasePageRenderer : PageRenderer
    {
        private string backgroundImage;
        private ContentPage page;
        private BasePages basePage;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
  
            page = Element as ContentPage;

            if (page != null && page is BasePages)
            {
                basePage = (BasePages)page;
                backgroundImage = page.BackgroundImage;
                basePage.SizeChanged += PageSizedChanged;
            }


            base.OnElementChanged(e);
        }

        private void PageSizedChanged(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(backgroundImage))
            {
                UIImage i = UIImage.FromFile(backgroundImage);
                var size = new CoreGraphics.CGSize(0, 0);
                if (CoreSettings.ScreenSize != null)
                {
                    size.Height = (nfloat)CoreSettings.ScreenSize.Height;
                    size.Width = (nfloat)CoreSettings.ScreenSize.Width;
                }

                UIGraphics.BeginImageContext(size);
                i = i.Scale(size);

                this.View.BackgroundColor = UIColor.FromPatternImage(i);
            }
        }

        protected override void Dispose(bool disposing)
        {
            basePage.SizeChanged -= PageSizedChanged;
            base.Dispose(disposing);
        }
        public override void ViewWillAppear(bool animated)
        {

            base.ViewWillAppear(animated);
            try
            {
                if (!string.IsNullOrEmpty(backgroundImage))
                {
                    UIImage i = UIImage.FromFile(backgroundImage);
                    var size = new CoreGraphics.CGSize(0, 0);
                    if(CoreSettings.ScreenSize!=null){
                        size.Height = (nfloat)CoreSettings.ScreenSize.Height;
                        size.Width = (nfloat)CoreSettings.ScreenSize.Width;
                    }

                    if (size.Height > this.View.Frame.Size.Height)
                    {
                        UIGraphics.BeginImageContext(size);
                        i = i.Scale(size);
                    }
                    else
                    {
                        UIGraphics.BeginImageContext(this.View.Frame.Size);
                        i = i.Scale(this.View.Frame.Size);
                    }

                    this.View.BackgroundColor = UIColor.FromPatternImage(i);
                }
            }
            catch (Exception ex)
            {
                ex.ConsoleWrite();
            }

            if (this.NavigationController != null)
            {
                var isVisible = !this.NavigationController.TopViewController.NavigationItem.HidesBackButton;
                if (isVisible && basePage != null && basePage.OverrideBackButton)
                {
                    this.NavigationController.TopViewController.NavigationItem.SetHidesBackButton(true, false);

                    // Change back icon.
                    this.NavigationController.TopViewController.NavigationItem.LeftBarButtonItem =
                        new UIBarButtonItem(
                            basePage.OverrideBackText,
                            UIBarButtonItemStyle.Plain,
                            (sender, args) =>
                            {
                                if (basePage.NeedOverrideSoftBackButton)
                                {
                                    basePage.OnSoftBackButtonPressed();
                                }
                                else
                                {
                                    NavigationController.PopViewController(true);
                                }

                            });
                }
            }

        }
    }
}
#endif
