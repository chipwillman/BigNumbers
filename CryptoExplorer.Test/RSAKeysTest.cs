using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoExplorer.Test
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Bcpg.OpenPgp;
    using Org.BouncyCastle.Crypto.Engines;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.X509;

    using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

    [TestClass]
    public class RSAKeysTest
    {
        private string pubRsa1 =
            @"ssh-rsa AAAAB3NzaC1yc2EAAAABIwAAAQEA4gmV63vQrBXHXK3+nSUS+1tHoL7OjXIG+b498+0dcXdhkD++tEIkNaMlIiRTu4qGE+9EpcOXzf6X6y/YTOylazKcWAJ7cmMq7hJeF3GOSirQwUKQ/yq2uABwSm8dkFfwsCAzGgHg02UBTl091OfbtxlSD0ENO6A4z7MYDyjQI3cMYNYl8x06qlW0iPahKt6OzDRSTrkCRHK8J1Ib8spwzNNdcuIHSyeQjV8xaX+J3dMJ6ejg9RXZhB7npfpfifvYCJ16BgYCLMYuFH/vRfJk1uPPe0hP8nqKawBP5iPB7K0EQ13wRIHtOGEro1jrHbWSgiDVhsQGdyZYKggMcGyCfw== chip@pacific.net.au
";

        private string ColKey2 = @"
-----BEGIN RSA PUBLIC KEY-----
iQIcBAEBAgAGBQJTXY+2AAoJEOstoML+FoNpSd8QAJPwbs/UtP428eYybeNIZrlT
nENlpXVbMw8lqeqNPG+E/DnRAWKqdBm6qetYGhBG9lNyccFE8t+XCDmpJ/8/mC/p
GWGv/Kphqx81+y2IoZbdkhE3SipWzKxPdxVNfemyLTPy8VAQrlt55PSIXCJeMIUe
y1Z1rUEyOIYkp2x5/orhFI2QgN/LBrnUtQU2aDunTXFl2ZF1iBXYTvoHEaFNFbSn
F3qlz/UhZ1bnHWXxqMss3cdBWUh+Mh/G+m9qL9NlzTk5vztnx1UG/hQBZOgx5A7C
N2kjb7OkBlAkiOXN/UiZ2yzgCstd2RAXDU2SYrCx+O8Zs+Gwi9NqwtoGCLMsNXSz
qSafQkiJKmjZeIv/nLP90uNzCnQ7U5Grvau7ANVFtbcrwbhRZoXOC0yc7OoYAYUT
54R+8XXmDRtMGHtmdmJ1Sej4rFXG1fYFn0JefA8JnE2vNb3b1sMuJa75rXUnCsiw
2GhP5esZzXFPJN2Ut+kclCfGbDkRn8HFgB1A9Ega2TwkI4W1qrfkY66i2XawinEH
7vhvqg1SDqC9lCB5NM12++PETJ+3sAZbfndjINzJf69Mwtu/fkkpU8f+X47Kfrrw
N6h6ys3WmPKjjdlz5jMhmQ6kU1C8w+jJ7pZ4j4dEY0gfPM1yWllioK48sAqJAJML
WCGvHcWSA5DUoKzejrV0
-----END RSA PUBLIC KEY-----
";

        [TestMethod]
        public void TestReadKey()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(ColKey2));
            var inputStream = PgpUtilities.GetDecoderStream(stream);
            PgpPublicKey result = null;
            // RsaEngine 
            var bytes =
                Convert.FromBase64String(
                    "AAAAB3NzaC1yc2EAAAABIwAAAQEA4gmV63vQrBXHXK3+nSUS+1tHoL7OjXIG+b498+0dcXdhkD++tEIkNaMlIiRTu4qGE+9EpcOXzf6X6y/YTOylazKcWAJ7cmMq7hJeF3GOSirQwUKQ/yq2uABwSm8dkFfwsCAzGgHg02UBTl091OfbtxlSD0ENO6A4z7MYDyjQI3cMYNYl8x06qlW0iPahKt6OzDRSTrkCRHK8J1Ib8spwzNNdcuIHSyeQjV8xaX+J3dMJ6ejg9RXZhB7npfpfifvYCJ16BgYCLMYuFH/vRfJk1uPPe0hP8nqKawBP5iPB7K0EQ13wRIHtOGEro1jrHbWSgiDVhsQGdyZYKggMcGyCfw==");
            var cert = new X509CertificateParser().ReadCertificate(bytes);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.PersistKeyInCsp = false;
                var filename = @"C:\Users\Chip\.ssh\id_rsa";
                Assert.IsTrue(File.Exists(filename));
                rsa.FromXmlString(File.ReadAllText(filename));
                Assert.IsNotNull(rsa);
            }

            X509Certificate2 certificate = new X509Certificate2(@"C:\Users\Chip\.ssh\id_rsa.pub");

            RSACryptoServiceProvider rsaprovider =
                    (RSACryptoServiceProvider)certificate.PublicKey.Key;

            PgpPublicKeyRingBundle pgpPub = new PgpPublicKeyRingBundle(inputStream);
            foreach (PgpPublicKeyRing keyRing in pgpPub.GetKeyRings())
            {
                foreach (PgpPublicKey key in keyRing.GetPublicKeys())
                {
                    if (key.IsEncryptionKey)
                    {
                        result = key;
                    }
                }
            }
            Assert.IsNotNull(result);
        }
    }
}
