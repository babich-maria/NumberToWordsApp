using System;

namespace ProcessService.Interfaces
{
    public class EnglishTranslator : ITranslator
    {
        const int MILLION = 1000000;
        const int THOUSAND = 1000;
        const int HUNDRED = 100;
        const int TEN = 10;
        const int UNIT = 1;

        const int FRACTION = 2;

        public string Translate(double number)
        {
            if (number < 0 || number >= 1000000000)
                return "Please enter positive number not greater than 999999999,99";

            if (number == 0)
                return EnglishDictionary.Numbers[0] + " " + PluralOrSingle(EnglishDictionary.Money["CURRENCY"], 0);

            string integerPart = ConvertIntegerPart((int)number);
            string fractionPart = ConvertFractionPart(GetFractionPart(number));

            var AND = !(string.IsNullOrWhiteSpace(integerPart) || string.IsNullOrWhiteSpace(fractionPart)) ? $" {EnglishDictionary.Money["AND"]} " : string.Empty;
            return integerPart + AND + fractionPart;
        }

        private string ConvertFractionPart(int fractionPart)
        {
            var result = string.Empty;
          
            if (fractionPart != 0)
            {
                result = $"{ParseDigitClass(fractionPart)}" +
                    $"{PluralOrSingle(EnglishDictionary.Money["FRACTION"], fractionPart)}";
            }

            return result;
        }

        private string ConvertIntegerPart(int integerPart)
        {
            if (integerPart == 0)
                return string.Empty;

            var classUnits = GetDigit(integerPart, 3, UNIT);
            var classThousands = GetDigit(integerPart, 3, THOUSAND);
            var classMillions = GetDigit(integerPart, 3, MILLION);

            var result = $"{ParseDigitClass(classMillions, EnglishDictionary.Numbers[MILLION])}" +
                $"{ParseDigitClass(classThousands, EnglishDictionary.Numbers[THOUSAND])}" +
                $"{ParseDigitClass(classUnits)}" +
                $"{PluralOrSingle(EnglishDictionary.Money["CURRENCY"], integerPart)}";

            return result;
        }

        private int GetFractionPart(double number)
        {
            double tmp = Math.Round(number, FRACTION);
            var fractionPart = (int)(Math.Round((tmp - Math.Truncate(tmp)), FRACTION) * 100);
            return fractionPart;
        }

        private string ParseDigitClass(int digitClass, string quantity = null)
        {
            if (digitClass == 0)
                return string.Empty;

            var units = GetDigit(digitClass, 1, UNIT);
            var tens = GetDigit(digitClass, 1, TEN);
            var hundreds = GetDigit(digitClass, 1, HUNDRED);

            var first = string.Empty;
            var second = string.Empty;
            var third = string.Empty;

            first = hundreds > 0 ? EnglishDictionary.Numbers[hundreds] + " " + EnglishDictionary.Numbers[HUNDRED] : first;

            if (tens == 1)
            {
                second = EnglishDictionary.Numbers[digitClass % HUNDRED];
            }
            else
            {
                second = tens > 0 ? EnglishDictionary.Numbers[tens * TEN] : second;
                third = units > 0 ? EnglishDictionary.Numbers[units] : third;
            }

            string hyphen = GetHyphen(second, third);

            return $"{first} {second}{hyphen}{third} {quantity} ";
        }

        private int GetDigit(int number, int digitAmount, int digitClass)
        {
            return number % (int)(digitClass * Math.Pow(10, digitAmount)) / digitClass;
        }

        private string PluralOrSingle(string str, int number)
        {
            return str + ((number != 1) ? "s" : string.Empty);
        }

        private string GetHyphen(string secondStr, string thirdStr)
        {
            return !string.IsNullOrWhiteSpace(secondStr) && !string.IsNullOrWhiteSpace(thirdStr) ? "-" : string.Empty;
        }
    }
}