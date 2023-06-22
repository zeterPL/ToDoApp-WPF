using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFProjekt
{
    public class PriorityColorPicker : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            
                if ((int)value == 0) return new SolidColorBrush(Colors.Green);
                else if((int)value == 1) return new SolidColorBrush(Colors.Yellow);
                else if((int)value == 2) return new SolidColorBrush(Colors.Red);
                else return new SolidColorBrush(Colors.Gray);
            
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
