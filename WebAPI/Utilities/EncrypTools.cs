using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Utilities
{
    /// <summary>
    /// RSA加解密
    /// </summary>
    public static class RSACrypto
    {
        private readonly static string bobPrivateKey = @"<RSAKeyValue><Modulus>t0GbCZ+Hun1W4R28xEySCcMV9E5BQT2ddwNIlcktTu2dtlDiOdU2kNoulAlX+ywNcRvahrC7FLqP5rQNYvFa788y5DtoS+DbibAl4CaYyOLLGpJEo7eH4S2FES46ifgga7v6rE0zyo6l7IcI89YuACJKNfr8RibvuvHDi9QzteE=</Modulus><Exponent>AQAB</Exponent><P>3NfwemvYMWtg6GEZDd2yd7Cz3QCebLKNM/U/t+/8bmWsjMsTK17LUZCS0SjROYAJBR3SJIUaZU3aZ3LlR4/l1w==</P><Q>1G3egVtHfkWd85W/NN26Sc39W67oELlU0qsn4gtVktGdGAttdL/MFzdcHmP6JUb+kfsNLtacmY5j/7/Qt9dbBw==</Q><DP>Vp90ufAXKrs9laiQVToCSKRMyID3oxcd/6VQyusdDohe+BKngDl8co/MprHl7zHjV9hsltqGkfnJkw4kFL/CnQ==</DP><DQ>JDw1hyU11cE0RzeU7QShYTOE8x2rsiaa5HCTlghO6YNd45sXaaHJw3ALA5gUNEWe6PHE9udewQa74gUrKiDPAw==</DQ><InverseQ>fccY3/KGmiG74mCSsyYXjJqNm+AXrfUkj536jvY2BUuaAJ+nWIRyP356nVLlK1SZjEt0as6g7kLrRQtbjkXL/Q==</InverseQ><D>IpeUv31IrrBIPRhS8K8Phh44P7Sh5sHNvpB1HCksj1CVS7v66DXSJge5cSJOZFBNfUNnvbMQrXoF/eOQ/1NV+crWXQN01hW11Rz2f8PInkJ+3QgW+LxmWoSp90TPpi3Kli/OGRyD+vKz89Grkyvu2Q3VU2kzCrLi8shpTKPtsE0=</D></RSAKeyValue>";
        private readonly static string bobPublicKey = @"<RSAKeyValue><Modulus>t0GbCZ+Hun1W4R28xEySCcMV9E5BQT2ddwNIlcktTu2dtlDiOdU2kNoulAlX+ywNcRvahrC7FLqP5rQNYvFa788y5DtoS+DbibAl4CaYyOLLGpJEo7eH4S2FES46ifgga7v6rE0zyo6l7IcI89YuACJKNfr8RibvuvHDi9QzteE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public static string Encrypt(string val)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(bobPublicKey);

            byte[] orgData = Encoding.Default.GetBytes(val);
            byte[] encryptedData = rsa.Encrypt(orgData, false);

            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string val)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            rsa.FromXmlString(bobPrivateKey);
            byte[] encryptedData = Convert.FromBase64String(val);
            byte[] decryptedData = rsa.Decrypt(encryptedData, false);

            return Encoding.Default.GetString(decryptedData);
        }
    }
}
