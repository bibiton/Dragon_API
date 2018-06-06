using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Helper.EncryptExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class EncryptExtensions
    {
        private static readonly byte[] AESKeys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };
        private static readonly byte[] DESKeys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 進階加密標準（英語：Advanced Encryption Standard，縮寫：AES），在密碼學中又稱Rijndael加密法
        /// </summary>
        /// <param name="encryptString">加密字串</param>
        /// <param name="encryptKey">加密金鑰</param>
        /// <returns></returns>
        public static string AESEncode(this string encryptString, string encryptKey)
        {
            encryptKey = encryptKey.Substring(32, 0);
            encryptKey = encryptKey.PadRight(32, ' ');

            var rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rijndaelProvider.IV = AESKeys;
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string AESDecode(this string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = decryptKey.Substring(32, 0);
                decryptKey = decryptKey.PadRight(32, ' ');

                var rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
                rijndaelProvider.IV = AESKeys;
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 資料加密標準（英語：Data Encryption Standard，縮寫為 DES）是一種對稱密鑰加密塊密碼演算法
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string DESEncode(this string encryptString, string encryptKey)
        {
            encryptKey = encryptKey.Substring(8);
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = DESKeys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            var dCSP = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string DESDecode(this string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = decryptKey.Substring(8);
                decryptKey = decryptKey.PadRight(8, ' ');
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = DESKeys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                var DCSP = new DESCryptoServiceProvider();

                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// Base64是一種基於64個可列印字元來表示二進位資料的表示方法。
        /// Base64常用於在通常處理文字資料的場合，表示、傳輸、儲存一些二進位資料。包括MIME的email、在XML中儲存複雜資料。
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string Base64Encode(this string encryptString)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(encryptString);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string Base64Decode(this string decryptString)
        {
            byte[] decbuff = Convert.FromBase64String(decryptString);
            return Encoding.UTF8.GetString(decbuff);
        }

        /// <summary>
        /// RSA加密演算法是一種非對稱加密演算法。在公開金鑰加密和電子商業中RSA被廣泛使用
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RSAEncrypt(this string s, string key)
        {
            if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty string value cannot be encrypted.");

            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");

            var cspp = new CspParameters();
            cspp.Flags = CspProviderFlags.UseMachineKeyStore;
            cspp.KeyContainerName = key;

            var rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            byte[] bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(s), true);

            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RSADecrypt(this string s, string key)
        {
            string result = null;

            if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty string value cannot be encrypted.");

            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");

            var cspp = new CspParameters();
            cspp.Flags = CspProviderFlags.UseMachineKeyStore;
            cspp.KeyContainerName = key;

            var rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            string[] decryptArray = s.Split(new[] { "-" }, StringSplitOptions.None);
            byte[] decryptByteArray = Array.ConvertAll(decryptArray, (a => Convert.ToByte(byte.Parse(a, NumberStyles.HexNumber))));

            byte[] bytes = rsa.Decrypt(decryptByteArray, true);

            result = Encoding.UTF8.GetString(bytes);

            return result;
        }

        /// <summary>
        /// MD5訊息摘要演算法（英語：MD5 Message-Digest Algorithm），一種被廣泛使用的密碼雜湊函式
        /// MD5演算法無法防止碰撞，因此無法適用於安全性認證，如SSL公開金鑰認證或是數位簽章等用途
        /// 不可逆編碼(雜湊值)
        /// 常用密碼字串儲存, 避免在資料庫裡使用明碼存放密碼, 以防資料庫外洩時, 密碼在第一時間被看光光
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(this string str)
        {
            string cl1 = str;
            string pwd = "";
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(cl1));
            for (int i = 0; i < s.Length; i++)
            {

                pwd = pwd + s[i].ToString("x");
            }
            return pwd;
        }

        /// <summary>
        /// 不可逆編碼(雜湊值)
        /// 常用密碼字串儲存, 避免在資料庫裡使用明碼存放密碼, 以防資料庫外洩時, 密碼在第一時間被看光光
        /// </summary>
        /// <param name="encypStr"></param>
        /// <returns></returns>
        public static string MD5CSP(this string encypStr)
        {
            string retStr;
            var m5 = new MD5CryptoServiceProvider();


            byte[] inputBye;
            byte[] outputBye;


            inputBye = Encoding.GetEncoding("UTF8").GetBytes(encypStr);

            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToLower();
            return retStr;
        }

        /// <summary>
        /// 安全雜湊演算法（英語：Secure Hash Algorithm，縮寫為SHA）是一個密碼雜湊函式
        /// 不可逆編碼(雜湊值)
        /// 常用密碼字串儲存, 避免在資料庫裡使用明碼存放密碼, 以防資料庫外洩時, 密碼在第一時間被看光光
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SHA256(this string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            var Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            StringBuilder strB = new StringBuilder();
            for (int i = 0; i < Result.Count(); i++)
                strB.Append(Result[i].ToString("x"));

            return strB.ToString();
        }
    }
}
