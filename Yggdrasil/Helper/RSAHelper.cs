using RSAExtensions;
using System.Security.Cryptography;
using System.Text;

namespace Yggdrasil.Helper {
    public class RSAHelper {
        public static string Sign(string text, string privateKey) {
            var rsa = RSA.Create();
            rsa.ImportFromPem(privateKey);

            return Convert.ToBase64String(rsa.SignData(Encoding.UTF8.GetBytes(text), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1));
        }

        public static string GetPublicKey(string privateKey) {
            var rsa = RSA.Create();
            rsa.ImportFromPem(privateKey);
            return rsa.ExportPublicKey(RSAKeyType.Pkcs1, true);
        }
    }
}
