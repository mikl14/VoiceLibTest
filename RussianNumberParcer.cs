using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceLibTest
{
    internal class RussianNumberParcer
    {
        private static readonly Dictionary<string, long> Units = new Dictionary<string, long>
    {
        {"ноль", 0},
        {"один", 1}, {"одна", 1},
        {"два", 2}, {"две", 2},
        {"три", 3},
        {"четыре", 4},
        {"пять", 5},
        {"шесть", 6},
        {"семь", 7},
        {"восемь", 8},
        {"девять", 9},
        {"десять", 10},
        {"одиннадцать", 11},
        {"двенадцать", 12},
        {"тринадцать", 13},
        {"четырнадцать", 14},
        {"пятнадцать", 15},
        {"шестнадцать", 16},
        {"семнадцать", 17},
        {"восемнадцать", 18},
        {"девятнадцать", 19}
    };

        private static readonly Dictionary<string, long> Tens = new Dictionary<string, long>
    {
        {"двадцать", 20},
        {"тридцать", 30},
        {"сорок", 40},
        {"пятьдесят", 50},
        {"шестьдесят", 60},
        {"семьдесят", 70},
        {"восемьдесят", 80},
        {"девяносто", 90}
    };

        private static readonly Dictionary<string, long> Hundreds = new Dictionary<string, long>
    {
        {"сто", 100},
        {"двести", 200},
        {"триста", 300},
        {"четыреста", 400},
        {"пятьсот", 500},
        {"шестьсот", 600},
        {"семьсот", 700},
        {"восемьсот", 800},
        {"девятьсот", 900}
    };

        private static readonly Dictionary<string, long> Multipliers = new Dictionary<string, long>
    {
        {"тысяча", 1000},
        {"тысячи", 1000},
        {"тысяч", 1000},
        {"миллион", 1000000},
        {"миллиона", 1000000},
        {"миллионов", 1000000}
    };

        /// <summary>
        /// Основной метод парсинга русских числительных в число double.
        /// Поддерживает целую и дробную часть через слово "целая".
        /// </summary>
        public static double Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Строка не может быть пустой");

            input = input.ToLowerInvariant().Replace("-", " ").Replace(" и ", " ");
            var parts = input.Split(new[] { "целая" }, StringSplitOptions.RemoveEmptyEntries);

            double integerPart = ParseIntegerPart(parts[0].Trim());

            if (parts.Length == 1)
                return integerPart;

            // Парсим дробную часть - считаем каждое слово как цифру после запятой
            string fractionalPartStr = parts[1].Trim();
            // Преобразуем слова дробной части в цифры
            string fractionalDigits = "";

            var fractionalWords = fractionalPartStr.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in fractionalWords)
            {
                if (Units.TryGetValue(word, out long digit) && digit >= 0 && digit <= 9)
                {
                    fractionalDigits += digit.ToString();
                }
                else if (long.TryParse(word, out long numericDigit) && numericDigit >= 0 && numericDigit <= 9)
                {
                    fractionalDigits += numericDigit.ToString();
                }
                else
                {
                    // Если встретили неизвестное слово - можно выбросить исключение или игнорировать
                    throw new FormatException($"Не удалось распознать слово '{word}' в дробной части");
                }
            }

            if (fractionalDigits.Length == 0)
                return integerPart;

            double fractionalPart = double.Parse("0." + fractionalDigits, CultureInfo.InvariantCulture);

            return integerPart + fractionalPart;
        }

        private static long ParseIntegerPart(string input)
        {
            var words = input.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            long total = 0;
            long current = 0;
            long lastMultiplier = 1;

            foreach (var word in words)
            {
                if (Hundreds.TryGetValue(word, out long hundred))
                {
                    current += hundred;
                }
                else if (Tens.TryGetValue(word, out long ten))
                {
                    current += ten;
                }
                else if (Units.TryGetValue(word, out long unit))
                {
                    current += unit;
                }
                else if (Multipliers.TryGetValue(word, out long multiplier))
                {
                    if (current == 0)
                        current = 1;

                    current *= multiplier;

                    total += current;
                    current = 0;
                    lastMultiplier = multiplier;
                }
                else
                {
                    throw new FormatException($"Неизвестное слово: {word}");
                }
            }

            total += current;

            return total;
        }
    }
}
