using Android.Content;
using SmartNews.Droid.CustomRenderer;
using SmartNews.Models.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace SmartNews.Droid.CustomRenderer
{
    public class CustomFrameRenderer : FrameRenderer
    {
        public CustomFrameRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
//#pragma warning disable CS0618
//            this.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.tin_24h));
//#pragma warning restore CS0618
        }
    }
}