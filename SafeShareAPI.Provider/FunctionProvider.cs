using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SafeShareAPI.Provider
{
    public static class FunctionProvider
    {
        public static T Clone<T>(this T source)
        {
            if (source is null) { return default; }
            JsonSerializerSettings deserializeSettings = new() { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
        public static string GetOneHourDynamicPassword()
        {
            return string.Concat(DateTime.UtcNow.ToString("HHyyyyddMM").Zip("KING003GNIK", (a, b) => new[] { a, b }).SelectMany(c => c));
        }
        public static double GetDirectorySize(string path)
        {
            double size = 0;
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new(path);
                foreach (FileInfo file in directoryInfo.GetFiles()) { size += file.Length; }
                foreach (DirectoryInfo directory in directoryInfo.GetDirectories()) { size += GetDirectorySize(directory.FullName); }
            }
            return size;
        }
        public static List<int> ToIntList(this object data)
        {
            return data != null ? (data as IEnumerable<object>).Select(d => Convert.ToInt32(d)).ToList() : new();
        }
        public static string GetDescription<T>(this T data) where T : IConvertible
        {
            return typeof(T).IsEnum ? data.GetType().GetMember(data.ToString()).FirstOrDefault().GetCustomAttribute<DescriptionAttribute>().Description : string.Empty;
        }
        public static double ByteToMegabyte(this double bytes)
        {
            return Math.Round(bytes / 1024 / 1024, 2);
        }
        public static string AddSpaceBeforeCapital(this string data)
        {
            return string.Concat(data.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
        public static bool IsEqualString(string source, string compare)
        {
            return source.Equals(compare, StringComparison.OrdinalIgnoreCase);
        }
        public static object FillProperty<T>(Dictionary<string, string> propertiesList)
        {
            object typeObject = Activator.CreateInstance(typeof(T));
            foreach (KeyValuePair<string, string> property in propertiesList)
            {
                PropertyInfo propertyInfo = typeObject.GetType().GetProperty(property.Key, BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null && propertyInfo.CanWrite) { propertyInfo.SetValue(typeObject, property.Value, null); }
            }
            return typeObject;
        }

        public static string INRCurrencyToWords(this int number)
        {
            if (number == 0) { return "Zero"; }

            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new();
            if (number < 0) { sb.Append("Minus "); number = -number; }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0)
                {
                    continue;
                }

                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t -= 10 * h; // tens
                if (h > 0)
                {
                    sb.Append(words0[h] + "Hundred ");
                }

                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0)
                    {
                        sb.Append("and ");
                    }

                    if (t == 0)
                    {
                        sb.Append(words0[u]);
                    }
                    else if (t == 1)
                    {
                        sb.Append(words1[u]);
                    }
                    else
                    {
                        sb.Append(words2[t - 2] + words0[u]);
                    }
                }
                if (i != 0)
                {
                    sb.Append(words3[i - 1]);
                }
            }

            return sb.ToString().TrimEnd();
        }
        //public static string FillTemplate(object data, string body, string rootName = null)
        //{
        //    VelocityContext velocityContext = new();
        //    VelocityEngine velocityEngine = new();
        //    ExtendedProperties extendedProperties = new();
        //    if (data == null) { return body; }
        //    velocityContext.Put(string.IsNullOrWhiteSpace(rootName) ? data.GetType().Name : rootName, data);
        //    velocityEngine.Init(extendedProperties);

        //    using StringWriter stringWriter = new();
        //    velocityEngine.Evaluate(velocityContext, stringWriter, string.Empty, body);
        //    return stringWriter.GetStringBuilder().ToString();
        //}
    }

    public static class RandomProvider
    {
        private static readonly Random Random = new();
        public static string RandomString()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        public static string RandomString(int length)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        public static string RandomNumber(int length)
        {
            return new string(Enumerable.Repeat("0123456789", length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        public static string RandomAlphabets(int length)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        public static string RandomNumberOnDate()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
        }
    }

    public static class ValidationProvider
    {
        public static bool IsEmail(this string data)
        {
            return new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(data).Success;
        }
        public static bool IsMobile(this string data)
        {
            bool isMobile = data.Length == 10 && new Regex(@"^\d*$").Match(data).Success;
            return isMobile;
        }
    }

    public class CustomDateProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }

        public string Format(string format, object dateToParse, IFormatProvider formatProvider)
        {
            if (!(dateToParse is DateTime))
            {
                throw new NotSupportedException();
            }

            DateTime dateTime = (DateTime)dateToParse;

            string suffix;

            if (new[] { 11, 12, 13 }.Contains(dateTime.Day)) { suffix = "th"; }
            else if (dateTime.Day % 10 == 1) { suffix = "st"; }
            else if (dateTime.Day % 10 == 2) { suffix = "nd"; }
            else if (dateTime.Day % 10 == 3) { suffix = "rd"; }
            else { suffix = "th"; }

            return string.Format("{1}{2} {0:MMMM}, {0:yyyy}", dateToParse, dateTime.Day, suffix);
        }
    }
}
