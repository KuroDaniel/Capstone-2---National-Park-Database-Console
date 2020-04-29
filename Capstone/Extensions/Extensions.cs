using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Extensions
{
    public static class Extensions
    {
        public static string ToYesNo(this bool value)
        {
            return (value ? "Yes" : "No");
        }
        public static string ToYesNA(this bool value)
        {
            return (value ? "Yes" : "N/A");
        }
        public static string ToLengthNA(this int value)
        {
            if (value == 0)
            {
                return "N/A";
            }
            return value.ToString();
        }
    }
}
