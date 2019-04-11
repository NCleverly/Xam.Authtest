using System;
namespace Xamarin.Forms.CommonCore
{
    public interface IMapNavigate
    {
        void NavigateWithAddress(string address);
        void NavigateLatLong(double latitude, double longtitude);
    }
}
