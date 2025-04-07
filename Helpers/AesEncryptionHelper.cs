using System.Security.Cryptography;

public static class AesEncryptionHelper
{
    // Método para generar una clave aleatoria de 32 bytes (256 bits) para AES-256
    private static readonly byte[] key = GenerateRandomBytes(32);  // Clave de 32 bytes para AES-256
    private static readonly byte[] iv = GenerateRandomBytes(16);   // IV de 16 bytes (128 bits)

    // Generar bytes aleatorios de longitud especificada
    private static byte[] GenerateRandomBytes(int length)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);
            return randomBytes;
        }
    }

    // Método para encriptar texto
    public static string Encrypt(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;   // Usar la clave generada aleatoriamente
            aesAlg.IV = iv;     // Usar el IV generado aleatoriamente

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    // Método para desencriptar texto
    public static string Decrypt(string cipherText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;   // Usar la clave generada aleatoriamente
            aesAlg.IV = iv;     // Usar el IV generado aleatoriamente

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
