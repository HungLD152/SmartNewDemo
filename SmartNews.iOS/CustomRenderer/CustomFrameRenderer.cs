using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using SmartNews.iOS.CustomRenderer;
using SmartNews.Models.CustomControl;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace SmartNews.iOS.CustomRenderer
{
    public class CustomFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            this.Layer.CornerRadius = 15;
            this.Layer.Bounds.Inset(1, 1);
            Layer.BorderColor = UIColor.White.CGColor;
            Layer.BorderWidth = 2;
            Layer.BackgroundColor = Color.Transparent.ToCGColor();
        }
    }
}