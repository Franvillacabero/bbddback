using System.Security.Cryptography;
using System.Text;

public static class AesEncryptionHelper
{
    // Usa una clave de 32 bytes (256 bits) y un IV de 16 bytes (128 bits)
    private static readonly byte[] key = GetValidKey("NetyMediaSecretKey32BytesLong!");
    private static readonly byte[] iv = GetValidIV("NetyMediaIV16Bytes!");

    private static byte[] GetValidKey(string originalKey)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(originalKey));
        }
    }

    private static byte[] GetValidIV(string originalIV)
    {
        using (var md5 = MD5.Create())
        {
            return md5.ComputeHash(Encoding.UTF8.GetBytes(originalIV)).Take(16).ToArray();
        }
    }

    public static string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return string.Empty;

        try 
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
        catch (Exception ex)
        {
            // Log de error real
            Console.WriteLine($"Error de encriptación: {ex.Message}");
            throw;
        }
    }

    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return string.Empty;

        try 
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                
                using (var msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log de error real
            Console.WriteLine($"Error de desencriptación: {ex.Message}");
            Console.WriteLine($"Texto cifrado recibido: {cipherText}");
            throw;
        }
    }
}