#if __ANDROID__
using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DroidView = Android.Views.View;
using Color = Xamarin.Forms.Color;
using Plugin.CurrentActivity;
using Android.Content;

[assembly: ExportRenderer(typeof(CoreSegmentButton), typeof(CoreSegmentButtonRenderer))]
[assembly: ExportRenderer(typeof(CoreSegment), typeof(CoreSegmentRenderer))]
namespace Xamarin.Forms.CommonCore
{
    public class CoreSegmentButtonRenderer : ButtonRenderer
    {
        public CoreSegmentButtonRenderer(Context ctx) : base(ctx)
        {

        }

        private Button _formControl
        {
            get { return Element as CoreSegmentButton; }
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            _formControl.BorderColor = Color.Black;
            _formControl.BorderWidth = 1;
        }

    }
    public class CoreSegmentRenderer : ViewRenderer<CoreSegment, DroidView>
    {
        private float _cornerRadius;
        private RectF _bounds;
        private Path _path;

        private CoreSegment _formControl
        {
            get { return Element as CoreSegment; }
        }

        public CoreSegmentRenderer(): base(CrossCurrentActivity.Current.Activity)
        {
            SetWillNotDraw(false);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CoreSegment> e)
        {
            base.OnElementChanged(e);

            var element = (CoreSegment)Element;

            _cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)element.CornerRadius,
                Context.Resources.DisplayMetrics);
        }



        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            if (w != oldw && h != oldh)
            {
                _bounds = new RectF(0, 0, w, h);
            }

            _path = new Path();
            _path.Reset();
            _path.AddRoundRect(_bounds, _cornerRadius, _cornerRadius, Path.Direction.Cw);
            _path.Close();
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Save();
            canvas.ClipPath(_path);
            base.Draw(canvas);
            canvas.Restore();
        }
    }


}
#endif

