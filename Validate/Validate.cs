using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validate
{
    public class Validate : IValidate
    {
        //private const string emailRegex = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
        private const string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        //private const string phoneRegex = @"^[0][0-9 ]{8,9}$";
        private const string phoneRegex = @"^(0|\+84)(3[2-9]|5[6|8|9]|7[06-9]|8[1-9]|9[0-9])[0-9]{7}$";

        public bool isDateTime( string str )
        {
            return DateTime.TryParse( str, out DateTime dt );
        }

        public bool isDecimal( string str )
        {
            return decimal.TryParse( str, out decimal dec );
        }

        public bool isDouble( string str )
        {
            return double.TryParse( str, out double dbl );
        }

        public bool isInt( string str )
        {
            return int.TryParse( str, out int i );
        }

        public bool isMatchEmailRegex( string str )
        {
            return Regex.IsMatch( str, emailRegex );
        }

        public bool isMatchPhoneRegex( string str )
        {
            return Regex.IsMatch( str, phoneRegex );
        }

        public bool isString( string str )
        {
            return !string.IsNullOrEmpty( str );
        }

        public bool isTrueRangeInt( string str, int min, int max )
        {
            return int.TryParse( str, out int i ) && i >= min && i <= max;
        }
    }
}
