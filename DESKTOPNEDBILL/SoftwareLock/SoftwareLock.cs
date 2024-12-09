using System;
using System.Security.Cryptography;
using System.Text;

namespace SoftwareLock
{
    public class SoftwareLock
    {
        static string _CustRef = "";
        static string _SerialKey = "";

        public static string GetReferenceKey(string _AppName, string _Password, string _DiskSerial)
        {
            try
            {
                return GenerateCodesRef(_AppName, _Password, _DiskSerial);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string GetSerialKey(string _AppName, string _Password, string _DiskSerial)
        {
            try
            {
                return GenerateCodesSerial(_AppName, _Password, _DiskSerial);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string GenerateCodesRef(string _AppName, string _Password, string _DiskSerial)
        {
            try
            {
                int _CustRefLength = 6;
                string _CustRef = "";
                Byte[] HashBytes;
                string Temp;
                MD5 md5 = new MD5CryptoServiceProvider();
                Temp = _AppName + _Password + _DiskSerial;
                HashBytes = md5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(Temp));
                _CustRef = Convert.ToBase64String(HashBytes);
                if (HashBytes.Length > _CustRefLength)
                {
                    _CustRef = _CustRef.Substring(0, _CustRefLength);
                }
                _CustRef = _CustRef.ToUpper();
                Array.Clear(HashBytes, 0, _CustRefLength);
                Temp = _CustRef + _AppName + _Password;
                HashBytes = md5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(Temp));
                _SerialKey = Convert.ToBase64String(HashBytes);
                if (HashBytes.Length > _CustRefLength)
                {
                    _SerialKey = _SerialKey.Substring(0, _CustRefLength);
                }
                _SerialKey = _SerialKey.ToUpper();
                return _CustRef;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string GenerateCodesSerial(string _AppName, string _Password, string _DiskSerial)
        {
            try
            {
                int _CustRefLength = 6;
                int _SerialRefLength = 16;
                string _CustRef = "";
                Byte[] HashBytes;
                string Temp;
                MD5 md5 = new MD5CryptoServiceProvider();
                Temp = _AppName + _Password + _DiskSerial;
                HashBytes = md5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(Temp));
                _CustRef = Convert.ToBase64String(HashBytes);
                if (HashBytes.Length > _CustRefLength)
                {
                    _CustRef = _CustRef.Substring(0, _CustRefLength);
                }
                _CustRef = _CustRef.ToUpper();
                Array.Clear(HashBytes, 0, _CustRefLength);
                Temp = _CustRef + _AppName + _Password;
                HashBytes = md5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(Temp));
                _SerialKey = Convert.ToBase64String(HashBytes);
                if (HashBytes.Length > _SerialRefLength)
                {
                    _SerialKey = _SerialKey.Substring(0, _SerialRefLength);
                }
                _SerialKey = _SerialKey.ToUpper();
                return _SerialKey;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
