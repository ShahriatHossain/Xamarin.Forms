﻿using Android.Gms.Maps.Model;
using MnMFiber.Common.Xaml.Controls;
using MnMFiber.Droid;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace MnMFiber.Droid
{
    public class CustomMapRenderer : MapRenderer
    {
        List<Position> routeCoordinates;
        bool isDrawn;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                routeCoordinates = formsMap.RouteCoordinates;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
            {
                var polylineOptions = new PolylineOptions();
                polylineOptions.InvokeColor(0x66FF0000);

                foreach (var position in routeCoordinates)
                {
                    polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
                }

                NativeMap.AddPolyline(polylineOptions);
                isDrawn = true;
            }
        }
    }
}