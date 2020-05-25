using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SmartNews.iOS.CustomRenderer;
using SmartNews.Models.CustomControl;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace SmartNews.iOS.CustomRenderer
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        public CustomButtonRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                Control.TitleLabel.Lines = 0;
                Control.TitleLabel.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}