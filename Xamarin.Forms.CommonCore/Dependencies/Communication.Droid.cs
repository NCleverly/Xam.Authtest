﻿#if __ANDROID__
using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Telephony;
using Android.Widget;
using Droid = Android;
using Xamarin.Forms.CommonCore;
using Android.Content.PM;
using Android.Support.V4.App;
using Plugin.CurrentActivity;

[assembly: UsesPermission(Name = "android.permission.SEND_SMS")]
[assembly: UsesPermission(Name = "android.permission.CALL_PHONE")]
[assembly: UsesPermission(Name = "android.permission.READ_PHONE_STATE")]
[assembly: Xamarin.Forms.Dependency(typeof(Communication))]
namespace Xamarin.Forms.CommonCore
{
    public partial class Communication : ICommunication
    {
        private TelephonyManager telephonyManager;
        private PhoneCallListener phoneListener;
        private string callBackKey;
        private const string dialogMessage = "The application need access to the devices's phone";

        public Context Ctx
        {
            get => CrossCurrentActivity.Current.Activity;
        }


        public void PlaceCall(string phoneNumber)
        {
            try
            {
                var intent = new Intent(Intent.ActionCall);
                var uri = global::Android.Net.Uri.Parse("tel:" + CoreExtensions.ToNumericString(phoneNumber));
                intent.SetData(uri);
                Ctx.StartActivity(intent);
            }
            catch
            {
                var toast = Toast.MakeText(Ctx, "This activity is not supported", ToastLength.Long);
                toast.Show();
            }

        }

        public void PlaceCallWithCallBack(string phoneNumber, string callBackKey)
        {
            try
            {
                this.callBackKey = callBackKey;
                phoneListener = new PhoneCallListener();
                telephonyManager = (TelephonyManager)Ctx.GetSystemService(Context.TelephonyService);
                phoneListener.CallEndedEvent += PhoneCallEnded;

                telephonyManager.Listen(phoneListener, PhoneStateListenerFlags.CallState);
                var intent = new Intent(Intent.ActionCall);
                var uri = global::Android.Net.Uri.Parse("tel:" + CoreExtensions.ToNumericString(phoneNumber));
                intent.SetData(uri);
                Ctx.StartActivity(intent);
            }
            catch
            {
                var toast = Toast.MakeText(Ctx, "This activity is not supported", ToastLength.Long);
                toast.Show();
            }
        }

		public void SendEmail(EmailMessage message)
		{
			var emailIntent = new Intent(Intent.ActionSend);
			emailIntent.SetType("message/rfc822");
			emailIntent.PutExtra(Intent.ExtraEmail, new string[] { message.EmailAddress });
			emailIntent.PutExtra(Intent.ExtraSubject, message.Subject);
			emailIntent.PutExtra(Intent.ExtraText, message.Message);
            Ctx.StartActivity(Intent.CreateChooser(emailIntent, message.Title));
		}

		public void SendSMS(string phoneNumber, string message)
		{
            SmsManager.Default.SendTextMessage(phoneNumber, null, message, null, null);
		}

        private void PhoneCallEnded(DateTime start, DateTime end)
        {
            if (phoneListener != null)
                phoneListener.CallEndedEvent -= PhoneCallEnded;
            telephonyManager.Listen(phoneListener, PhoneStateListenerFlags.None);
            phoneListener = null;
            CoreDependencyService.SendViewModelMessage(callBackKey,true);
        }
    }

    public delegate void CallEndedEventHandle(DateTime startTime, DateTime endTime);
    public class PhoneCallListener : PhoneStateListener
    {
        private bool isPhoneCalling = false;
        public event CallEndedEventHandle CallEndedEvent;
        private DateTime startTime;

        public override void OnCallStateChanged(CallState state, string incomingNumber)
        {
            switch ((CallState)state)
            {
                case CallState.Ringing:
                    break;
                case CallState.Offhook:
                    isPhoneCalling = true;
                    startTime = DateTime.Now;
                    break;
                case CallState.Idle:
                    if (isPhoneCalling)
                    {
                        isPhoneCalling = false;
                        if (CallEndedEvent != null)
                        {
                            CallEndedEvent(startTime, DateTime.Now);
                        }
                    }
                    break;
            }
            base.OnCallStateChanged(state, incomingNumber);
        }
    }
}
#endif
