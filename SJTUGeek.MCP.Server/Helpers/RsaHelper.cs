using System.Security.Cryptography;

namespace SJTUGeek.MCP.Server.Helpers
{
    public static class RsaHelper
    {
        public static byte[] RSAEncrypt(byte[] data, string publicKey)
        {
            RSA rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out _);
            byte[] encryptData = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            return encryptData;
        }
    }
}
