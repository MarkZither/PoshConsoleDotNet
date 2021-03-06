using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PoshCode.Controls
{
   public class ImageButton : Button
   {
      static ImageButton()
      {
         DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
      }

      public ImageSource Image
      {
         get { return (ImageSource)GetValue(ImageProperty); }
         set { SetValue(ImageProperty, value); }
      }

      // Using a DependencyProperty as the backing store for Default.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ImageProperty =
          DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));


      public Orientation Orientation
      {
         get { return (Orientation)GetValue(OrientationProperty); }
         set { SetValue(OrientationProperty, value); }
      }

      // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty OrientationProperty =
          DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ImageButton), new UIPropertyMetadata(Orientation.Horizontal));

   }
}
