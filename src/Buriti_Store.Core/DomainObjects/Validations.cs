using System.Text.RegularExpressions;

namespace Buriti_Store.Core.DomainObjects
{
    public class Validations
    {
        public static void ValidateEquals(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfDifferent(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfDifferent(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(value))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateSize(string value, int maximum, string message)
        {
            var length = value.Trim().Length;
            if (length > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateSize(string value, int minimum, int maximum, string message)
        {
            var length = value.Trim().Length;
            if (length < minimum || length > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfEmpty(string value, string message)
        {
            if (value == null || value.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumMaximum(double value, double minimum, double maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumMaximum(float value, float minimum, float maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumMaximum(int value, int minimum, int maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumMaximum(long value, long minimum, long maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumMaximum(decimal value, decimal minimum, decimal maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfLessThan(long value, long minimum, string message)
        {
            if (value < minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfLessThan(double value, double minimum, string message)
        {
            if (value < minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfLessThan(decimal value, decimal minimum, string message)
        {
            if (value < minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfLessThan(int value, int minimum, string message)
        {
            if (value < minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfFalse(bool boolvalue, string message)
        {
            if (!boolvalue)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfTrue(bool boolvalue, string message)
        {
            if (boolvalue)
            {
                throw new DomainException(message);
            }
        }
    }
}
