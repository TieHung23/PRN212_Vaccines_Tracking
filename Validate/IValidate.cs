using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validate
{
    public interface IValidate
    {
        public bool isString( string str );

        public bool isInt( string str );

        public bool isDouble( string str );

        public bool isDateTime( string str );

        public bool isDecimal( string str );

        public bool isMatchEmailRegex( string str );

        public bool isMatchPhoneRegex( string str );

        public bool isTrueRangeInt( string str, int min, int max );
    }
}
