using System.Text;
using SimpleTrading.Common.Helpers;

namespace Service.PersonalData.Domain.Models
{
    public static class AesEncodeDecodeUtil
    {
        public static string EncryptString(this string str, byte[] key)
        {
            var data = Encoding.UTF8.GetBytes(str);

            var result = AesEncodeDecode.Encode(data, key);

            return HexConverterUtils.ToHexString(result);
        }

        public static string DecryptString(this string str, byte[] key)
        {
            var data = HexConverterUtils.HexStringToByteArray(str);

            return Encoding.UTF8.GetString(AesEncodeDecode.Decode(data, key));
        }
    }
}