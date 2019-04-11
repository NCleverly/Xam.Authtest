#if __IOS__
using System;
using System.Threading.Tasks;
using BigTed;
using Xamarin.Forms.CommonCore;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DialogPrompt))]
namespace Xamarin.Forms.CommonCore
{
    public class DialogPrompt : IDialogPrompt
    {
        public void ShowMessage(Prompt prompt)
        {
            if (prompt.ButtonTitles == null || prompt.ButtonTitles.Length == 0)
                return;

            var controller = GetUIController();
            var alert = UIAlertController.Create(prompt.Title, prompt.Message, UIAlertControllerStyle.Alert);
            foreach (var txt in prompt.ButtonTitles)
            {
                alert.AddAction(UIAlertAction.Create(txt, UIAlertActionStyle.Default, action =>
                {
                    prompt.Callback?.Invoke(prompt.ButtonTitles.IndexOf(txt));
                }));
            }

            controller.PresentViewController(alert, true, null);
        }

        public void ShowActionSheet(string title, string subTitle, string[] list, Action<int> callBack)
        {
            var controller = GetUIController();
            var alert = UIAlertController.Create(title, subTitle, UIAlertControllerStyle.ActionSheet);
            alert.View.TintColor = UIColor.Black;
            foreach (var obj in list)
            {
                alert.AddAction(UIAlertAction.Create(obj, UIAlertActionStyle.Default, (action) =>
                {
                    var index = list.ToList().IndexOf(action.Title);
                    callBack.Invoke(index);
                }));
            }
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, (action) =>
            {
                callBack.Invoke(-1);
            }));

            var presentationPopover = alert.PopoverPresentationController;
            if (presentationPopover != null)
            {
                presentationPopover.SourceView = controller.View;
                presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
                presentationPopover.SourceRect = controller.View.Frame;
            }
            controller.PresentViewController(alert, true, null);
        }

        private UIViewController GetUIController()
        {
            var win = UIApplication.SharedApplication.KeyWindow;
            var vc = win.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;
            return vc;
        }


        public void ShowToast(string message)
        {
            BTProgressHUD.ShowToast(message, false, 4000);
        }
    }
}
#endif

