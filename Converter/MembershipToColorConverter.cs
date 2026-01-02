using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GymManagementApp.Converters
{
    public class MembershipToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string membership = value as string;
            switch (membership)
            {
                case "Standard":
                    return new SolidColorBrush(Color.FromRgb(173, 216, 230)); // LightBlue
                case "Premium":
                    return new SolidColorBrush(Color.FromRgb(144, 238, 144)); // LightGreen
                case "VIP":
                    return new SolidColorBrush(Color.FromRgb(255, 215, 0));   // Gold
                case "1 Year":
                    return new SolidColorBrush(Color.FromRgb(135, 206, 250)); // SkyBlue
                case "6 Months":
                    return new SolidColorBrush(Color.FromRgb(144, 238, 144)); // LightGreen
                case "3 Months":
                    return new SolidColorBrush(Color.FromRgb(255, 182, 193)); // LightPink
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
