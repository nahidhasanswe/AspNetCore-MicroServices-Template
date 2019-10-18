namespace AspCoreMicroservice.Core
{
    public interface ICryptoGraphy
    {
        string EncryptString(string message);
        string DecryptString(string message);
        string GenerateCode();
        string GeneratePassword();
        string Encrypt(string plainText);
        string Decrypt(string encryptedText);
    }
}
