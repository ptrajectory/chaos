using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace chaos.utils
{
    public class Utils
    {

        static public byte[] GenerateRandomBytes(int length){
            byte[] randomBytes = new byte[length];
            new Random().NextBytes(randomBytes);
            return randomBytes;
        }

        static string BytesToHexString(byte[] bytes){
            StringBuilder hexBuilder = new StringBuilder();
            foreach(byte b in bytes)
            {
                hexBuilder.AppendFormat("{0:x2}",b);
            }
            return hexBuilder.ToString();
        }


        static public string GenerateUniqueID(string prefix ){
            byte[] randomBytes = GenerateRandomBytes(16);
            StringBuilder idBuilder = new StringBuilder();

            idBuilder.Append(prefix);
            idBuilder.Append("_");
            idBuilder.Append(BytesToHexString(randomBytes));

            return idBuilder.ToString();
        }

        static public async Task<byte[]> ConvertFormFileToByteArray(IFormFile file){
            using(var memoryStream = new MemoryStream()){
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        static public string EncryptForStorage(string token)
        {
            using (Aes aesAlg = Aes.Create())
            {   
                if(AppEnvironment.ENCRYPTION_KEY is null) throw new Exception("ENCRYPTION KEY HAS NOT BEEN PROVIDED");
                
                aesAlg.Key = Encoding.UTF8.GetBytes(AppEnvironment.ENCRYPTION_KEY);

                aesAlg.IV = new byte[16];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using(MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {

                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(token);
                        }
                    }
                    return Convert.ToHexString(msEncrypt.ToArray());
                }

            }
        }


        static public string DecryptFromStorage(string encryptedText)
        {
            if(AppEnvironment.ENCRYPTION_KEY is null) throw new Exception("ENCRYPTION KEY HAS NOT BEEN PROVIDED");

            using(Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(AppEnvironment.ENCRYPTION_KEY);
                aesAlg.IV = new byte[16];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromHexString(encryptedText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

        }


        public static string GenerateEncryptedJwt(string owner, string? prefix){
            var rsaKey = RSA.Create();
            rsaKey.ImportRSAPrivateKey(AppEnvironment.APP_RSA_KEY_PAIR, out _);

            var key = new RsaSecurityKey(rsaKey);

            var handler = new JsonWebTokenHandler();

            var token = handler.CreateToken(new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(new [] {
                    new Claim("owner", owner),
                    new Claim("scope", "app"),
                    new Claim("environment", prefix ?? "")
                }),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256),
                Expires = DateTime.MaxValue
            });

            var encrypted = EncryptForStorage(token);

            if(prefix is not null){
                return prefix + "_" + encrypted;
            }
 
            return encrypted;
        }


        public static string GetDecryptedJwt(string encryptedText)
        {
            if(encryptedText.Contains("prod") || encryptedText.Contains("test"))
            {
                var without_prefix = encryptedText.Remove(0, 5);
                Console.WriteLine(without_prefix);
                return DecryptFromStorage(without_prefix);
            }
            return DecryptFromStorage(encryptedText);
          
        }


        
    }
}