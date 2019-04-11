using System;
namespace Xamarin.Forms.CommonCore
{
	public class TelephonyCompleteStatus
	{
		public DateTime Completed { get; set; }
		public bool Success { get; set; }
		public Exception Error { get; set; }
	}

    public interface ICommunication
    {
		void PlaceCall(string phoneNumber);
		/// <summary>
		/// Places the call with call back to view model (see SendViewModelMessage)/> .
		/// Returns back json object of status of completion of call.
		/// This requires the page to inherit from TelephonyPage
		/// </summary>
		/// <param name="phoneNumber">Phone number.</param>
		/// <param name="key">Key.</param>
		void PlaceCallWithCallBack(string phoneNumber, string key);

        void SendEmail(EmailMessage message);

        void SendSMS(string phoneNumber, string message);
    }
}
