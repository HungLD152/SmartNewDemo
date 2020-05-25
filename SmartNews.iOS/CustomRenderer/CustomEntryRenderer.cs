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

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace SmartNews.iOS.CustomRenderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                UITextField textField = Control;
                textField.BorderStyle = UITextBorderStyle.None;
                Control.TextColor = UIColor.Black;
                Control.Layer.CornerRadius = 10;
                textField.ClearButtonMode = UITextFieldViewMode.WhileEditing;

            }
        }
    }
}