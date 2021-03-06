using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PoshCode.Controls
{
   public static class ColorHelper
   {
      public static Color Invert(this Color color)
      {
         return new Color { A = color.A, B = (byte)(255 - color.B), R = (byte)(255 - color.R), G = (byte)(255 - color.G) };
      }
      public static Color Invert(this Color color, byte Alpha)
      {
         return new Color { A = Alpha, B = (byte)(255 - color.B), R = (byte)(255 - color.R), G = (byte)(255 - color.G) };
      }
   }

   public sealed class ColorInverter : DependencyObject, IValueConverter
   {
      // We register an optional Alpha property
      #region Alpha=255
      public static readonly DependencyProperty AlphaProperty = DependencyProperty.Register(
          "Alpha",                              // name
          typeof(byte?), typeof(ColorInverter),  // Type information
          new FrameworkPropertyMetadata(null));  // Default Value

      public byte? Alpha
      {
         get { return (byte?)GetValue(AlphaProperty); }
         set { SetValue(AlphaProperty, value); }
      }
      #endregion

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value == null || !(value is Color))
            {
                return Colors.White;
            }

            if (Alpha != null) { return ((Color)value).Invert(Alpha.Value); }
          return ((Color)value).Invert();
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {

         if (value == null || !(value is Color))
            {
                return Colors.White;
            }

            return ((Color)value).Invert();

      }
   }


   public sealed class ColorToBrushConverter : DependencyObject, IValueConverter
   {

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
          if (value == null || !(value is Color))
            {
                return Brushes.White;
            }

            return new SolidColorBrush((Color)value);
      }

       public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {

         if (value == null || !(value is SolidColorBrush))
            {
                return Colors.White;
            }

            return ((SolidColorBrush)value).Color;

      }
   }

   public sealed class ColorToBrushInverter : DependencyObject, IValueConverter
   {
      // We register an optional Alpha property
      #region Alpha=255
      public static readonly DependencyProperty AlphaProperty = DependencyProperty.Register(
          "Alpha",                              // name
		  typeof(byte?), typeof(ColorToBrushInverter),  // Type information
          new FrameworkPropertyMetadata(null));  // Default Value

      public byte? Alpha
      {
         get { return (byte?)GetValue(AlphaProperty); }
         set { SetValue(AlphaProperty, value); }
      }
      #endregion

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value == null || !(value is Color))
            {
                return Brushes.White;
            }

            if (Alpha != null) { return new SolidColorBrush(((Color)value).Invert(Alpha.Value)); }
          return new SolidColorBrush(((Color)value).Invert());
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value == null || !(value is SolidColorBrush))
            {
                return Colors.White;
            }

            return ((SolidColorBrush)value).Color.Invert();
      }
   }
}
