using MnMFiber.Common.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MnMFiber.Views.PatrollerDailySurveillance
{
    public partial class RouteMapPage : ContentPage
    {
        public RouteMapPage()
        {
            InitializeComponent();

            var customMap = new CustomMap
            {
                MapType = MapType.Street,
                WidthRequest = 300,
                HeightRequest = 300
            };

            customMap.RouteCoordinates.Add(new Position(37.785559, -122.396728));
            customMap.RouteCoordinates.Add(new Position(37.780624, -122.390541));
            customMap.RouteCoordinates.Add(new Position(37.777113, -122.394983));
            customMap.RouteCoordinates.Add(new Position(37.776831, -122.394627));

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMiles(1.0)));

            MyMap = customMap;
        }
    }
}
