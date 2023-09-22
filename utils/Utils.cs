using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
    }
}