using hotel_management_api_identity.Core.Helpers.Models;
using System.Dynamic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace hotel_management_api_identity.Core.Helpers.Extension
{
    public static class Extensions
    {
        private static readonly Random _random = new Random();
        public static bool IgnoreProperty<T>(this Type typeObject, string propertyName)
        {
            return Attribute.IsDefined(typeObject.GetProperty(propertyName), typeof(IgnoreDuringInsertOrUpdateAttribute), false);
        }

        public static bool IgnoreTrailProperty<T>(this Type typeObject, string propertyName)
        {
            return Attribute.IsDefined(typeObject.GetProperty(propertyName), typeof(IgnoreDuringInsertOrUpdateAttribute), false);
        }

        public static string GetTableName<T>(this Type entity)
        {
            if (!Attribute.IsDefined(entity, typeof(TableNameAttribute), false))
            {
                if (Attribute.IsDefined(entity, typeof(TableNameAttribute), false))
                {
                    var tableNameAttribute1 = Attribute.GetCustomAttribute(entity, attributeType: typeof(TableNameAttribute)) as TableNameAttribute;
                    return tableNameAttribute1.Name;
                }
                return null;
            }

            var tableNameAttribute = Attribute.GetCustomAttribute(entity, typeof(TableNameAttribute)) as TableNameAttribute;
            return tableNameAttribute.Name;
        }

        public static T GetClassInstance<T>(this Dictionary<string, object> dict, Type type) where T : class
        {
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                var prop = type.GetProperty(kv.Key);
                if (prop == null) continue;

                prop.SetValue(obj, kv.Value, null);
            }
            return obj as T;
        }

        public static object ConvertToAnonymousObject(this Dictionary<string, object> dict)
        {
            var eo = new ExpandoObject();
            var eoColl = eo as ICollection<KeyValuePair<string, object>>;

            foreach (var kvp in dict)
            {
                eoColl.Add(kvp);
            }
            dynamic eoDynamic = eo;
            return eoDynamic;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                static string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            if(phone.Length < 11) return false;
            return phone.All(c => c >= '0' && c <= '9');
        }

        

        // Generates a random number within a range.      
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        // Generates a random string with a given size.    
        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static string RandomEmployeeNumber()
        {
            var passwordBuilder = new StringBuilder();
            passwordBuilder.Append(RandomString(2));
            passwordBuilder.Append(RandomNumber(1000, 9999));            
            return $"EMP-{passwordBuilder}";
        }

        public static string RandomCustomerCode()
        {
            var passwordBuilder = new StringBuilder();
            passwordBuilder.Append(RandomString(2));
            passwordBuilder.Append(RandomNumber(1000, 9999));
            return $"CUS-{passwordBuilder}";
        }

        public static string Encrypt(string encryptString)
        {
            string EncryptionKey = "a69d4fcf-2bc3-477c-b652-1de27c2e48c9"; 
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] 
                {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "a69d4fcf-2bc3-477c-b652-1de27c2e48c9";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] 
                {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
            return cipherText;
        }
    }
}