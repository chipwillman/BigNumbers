
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace Factoring.Test
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Factoring
    {
        public const string prime38digits = "52145789456214851477889522145649897231";

        public const string primt38digitsClose = "52145789456214851477889522145649897293";

        public const string prime42digits = "82232145789456214851477889522145649897269";

        //  (99 digits, factors p=486...193 and q=527...217 have respectively 49 and 50 digits)
        public const string RSA99 =
            "256724393281137036243618548169692747168133997830674574560564321074494892576105743931776484232708881";

        // 59 digits, factors p=200...437 and q=357...017 have both 30 digits)
        //  357440504101388365610785389017
        //  200429218120815554269743635437
        //
        public const string RSA59 = "71641520761751435455133616475667090434063332228247871795429";

        // (79 digits, factors p=848...977 and q=859...727 have respectively 39 and 40 digits)
        public const string RSA79 = "7293469445285646172092483905177589838606665884410340391954917800303813280275279 ";

        [TestMethod]
        public void TestFindFactorsOfLargeNumber()
        {
            long MaxIterations = 10000000000;
            var factor = new Factor { Product = BigInteger.Parse(prime38digits) * BigInteger.Parse(prime42digits) };

            for (int i = 0; i < MaxIterations; i++)
            {
                if (factor.FindFactors()) break;
            }
        }

        [TestMethod]
        public void TestCreateProductOfClosePrimes()
        {
            var product = BigInteger.Parse(primt38digitsClose) * BigInteger.Parse(prime38digits);
            var sqrt = product.Sqrt();
            Assert.AreEqual("52145789456214851477889522145649897261", sqrt.ToString());

            Assert.AreEqual(
                "2719183358011887975796721083228569129647991167694879508986470100431155095683", product.ToString());

            var factor = new Factor { Product = product };

            if (factor.CheckForPrimesCloseToSquareRoot())
            {
                Assert.AreEqual(prime38digits, factor.Factors[0].ToString());
                Assert.AreEqual(primt38digitsClose, factor.Factors[1].ToString());
            }
        }

        [TestMethod]
        public void TestCreateProductOfNotSoClosePrimes()
        {
            var product = BigInteger.Parse(prime42digits) * BigInteger.Parse(prime38digits);

            var factor = new Factor { Product = product };

            if (factor.CheckForPrimesCloseToSquareRoot())
            {
                Assert.AreEqual(prime38digits, factor.Factors[0].ToString());
                Assert.AreEqual(prime42digits, factor.Factors[1].ToString());
            }

            var rsa99factor = new Factor() { Product = BigInteger.Parse(RSA99) };
            if (rsa99factor.CheckForPrimesCloseToSquareRoot())
            {
                Assert.IsTrue(rsa99factor.Factors.Length > 0);
            }

            var rsa79factor = new Factor() { Product = BigInteger.Parse(RSA79) };
            if (rsa79factor.CheckForPrimesCloseToSquareRoot())
            {
                Assert.IsTrue(rsa79factor.Factors.Length > 0);
            }
        }

        [TestMethod]
        public void TestFindProductOfNotSoClosePrimesRSA59()
        {
            var rsa59factor = new Factor() { Product = BigInteger.Parse(RSA59) };
            if (rsa59factor.CheckForPrimesCloseToSquareRoot())
            {
                Assert.IsTrue(rsa59factor.Factors.Length > 0);
            }
        }

        [TestMethod]
        public void TestFastFactorOfLargeNumber()
        {
            long MaxIterations = 10000000000;
            var factor = new Factor { Product = BigInteger.Parse(prime38digits) * BigInteger.Parse(prime42digits) };

            for (int i = 0; i < MaxIterations; i++)
            {
                if (factor.FastFactor()) break;
                //if (factor.FindFactors()) break;
            }

        }


        [TestMethod]
        public void TestSquareRootOfBigInteger()
        {

            var prime1 = BigInteger.Parse(prime38digits);
            var prime2 = BigInteger.Parse(prime42digits);

            var productOfPrimes = prime1 * prime2;

            var sqrtOfProductOfPrimes = productOfPrimes.Sqrt();
            var mod = productOfPrimes % sqrtOfProductOfPrimes;

            var upperBounds = productOfPrimes / 2;


            var sqrtOfProductFloat = (double)sqrtOfProductOfPrimes;
            var upperBoundsFloat = (double)upperBounds;

            var slope1 = (sqrtOfProductFloat - upperBoundsFloat) / (-2 - sqrtOfProductFloat);

            Assert.AreNotEqual(0f, slope1);
        }

        [TestMethod]
        public void TestFindFactorsTwoAndThree()
        {
            var factor = new Factor { Product = 2 * 3 };

            for (int i = 0; i < 5; i++)
            {
                if (factor.FindFactors())
                {
                    break;
                }
            }

            Assert.AreEqual(2, factor.Factors[0]);
            Assert.AreEqual(3, factor.Factors[1]);
        }

        [TestMethod]
        public void TestFindFactorsThirteenAndSeventeen()
        {
            var MaxIterations = 100;
            var factor = new Factor { Product = 13 * 17 };

            for (int i = 0; i < MaxIterations; i++)
            {
                if (factor.FindFactors()) break;
            }

            Assert.AreEqual(13, factor.Factors[0]);
            Assert.AreEqual(17, factor.Factors[1]);
        }

        [TestMethod]
        public void TestFindAllFactorsTo111Squared()
        {
            var MaxIterations = 1000;
            for (int x = 3; x <= 111; x = x + 2)
            {
                for (int y = 3; y <= 111; y = y + 2)
                {
                    var factor = new Factor { Product = x * y };

                    for (int i = 0; i < MaxIterations; i++)
                    {
                        //if (factor.FindFactors()) break;
                        if (factor.FastFactor()) break;
                    }

                    BigInteger product = factor.Factors.Aggregate<BigInteger, BigInteger>(1, (current, f) => current * f);

                    Assert.AreEqual(x * y, product);
                }
            }
        }

        [TestMethod]
        public void TestUsingRemainderToSearch()
        {
            var prime1 = new BigInteger(13);
            var prime2 = new BigInteger(17);
            var product = prime1 * prime2;

            var sqrt = product.Sqrt();
            var p1 = sqrt;
            var p2 = sqrt;
            var lastMod1 = BigInteger.Zero;
            var lastMod2 = BigInteger.Zero;
            var factorsFound = false;
            var changefactor1 = true;
            while (!factorsFound)
            {
                var mod1 = product % p1;
                var mod2 = product % p2;
                System.Diagnostics.Debug.WriteLine("p1 " + p1 + " p2 " + p2 + " m1 " + mod1 + " m2 " + mod2);
                if (mod1 == 0 || mod2 == 0)
                {
                    factorsFound = true;
                    break;
                }

                if ((mod1 <= lastMod1 || mod2 <= lastMod2) && (mod1 >= lastMod1 || mod2 >= lastMod2))
                {
                    changefactor1 = !changefactor1;
                    System.Diagnostics.Debug.WriteLine("Switching to factor " + (changefactor1 ? "1" : "2"));
                }

                if (changefactor1)
                {
                    var delta = BigInteger.Min(mod1, p1 - mod1);
                    p1 += delta;
                    p2 = product / p1;
                }
                else
                {
                    var delta = BigInteger.Min(mod2, p2 - mod2);
                    p2 += delta;
                    p1 = product / p2;
                }
                lastMod1 = mod1;
                lastMod2 = mod2;
            }
        }

        [TestMethod]
        public void TestGaussianRoot()
        {
            var prime1 = new BigInteger(971);
            var prime2 = new BigInteger(719);
            var product = prime1 * prime2;

            var sqrt = product.Sqrt();

            var otherSqrt = product / sqrt;

            var mod = product % sqrt;

            var factor = new Factor() { Product = mod };
            long MaxIterations = 10000000;
            for (int i = 0; i < MaxIterations; i++)
            {
                if (factor.FastFactor()) break;
                //if (factor.FindFactors()) break;
            }

            Assert.AreEqual(2, factor.Factors.Length);

            var p1 = sqrt;
            var p2 = product / sqrt;

            var p = (p1 / p2) * factor.Factors[0];
            var q = (p2 / p1) * factor.Factors[1];

            var imaginaryProduct = p1 * q - p2 * p;
            //var imaginaryProduct = ((double)p2 * Math.Sqrt((double)(p * q)))
            //            - ((double)p1 * Math.Sqrt((double)(p * q)));

            var done = false;
            var lastImaginaryProduct = imaginaryProduct;
            while (!done)
            {
                var mod1 = product % p1;
                var mod2 = product % p2;
                if (mod1 == 0 || mod2 == 0)
                {
                    done = true;
                }
                else // if (mod1 == mod2)
                {
                    factor = new Factor { Product = mod1 };
                    for (int i = 0; i < MaxIterations; i++)
                    {
                        if (factor.FastFactor()) break;
                        //if (factor.FindFactors()) break;
                    }

                    p = (p1 * factor.Factors[0]) / p2;
                    q = (p2 * factor.Factors[1]) / p1;

                    //product = p1 - p i * p2 + q i;
                    var realProduct = p1 * p2 + p * q;
                    //imaginaryProduct = (p1 * q) - (p2 * p);
                    imaginaryProduct = p1 * q - p2 * p;
                    //imaginaryProduct = ((double)p2 * Math.Sqrt((double)(p * q)))/(double)p1
                    //                        - ((double)p1 * Math.Sqrt((double)(p * q)))/(double)p2;
                    if (lastImaginaryProduct >= imaginaryProduct)
                    {
                        p1 = p1 + imaginaryProduct % p1;
                    }
                    else
                    {
                        p1 = p1 - imaginaryProduct % p1;
                    }
                    p2 = product / p1;

                    lastImaginaryProduct = imaginaryProduct;
                    Assert.AreEqual(product, realProduct);
                }
                //else
                //{
                //    Assert.Fail("Failed to get matching mods");
                //}
            }
        }

        [TestMethod]
        public void TestGaussian2()
        {
            var prime1 = new BigInteger(971);
            var prime2 = new BigInteger(719);
            var product = prime1 * prime2;

            var gaussReal1 = new BigInteger(937);
            var gaussReal2 = product / gaussReal1;

            var gaussImaginarySquared = 84;

            var testProduct = gaussReal1 * gaussReal2 + gaussImaginarySquared;

            Assert.AreEqual(testProduct, product);


        }

        private string ColKey1 = @"iQIcBAEBAgAGBQJTXN5PAAoJEOstoML+FoNpYh8P/R3L0NVR+2FTSZQD2zzly2rc
0qLN1EXihbXkR6abv9uVM9kLCm8VzK0/bPbgmXDdEnW3ffBjh0VMZZUufrI4c1v9
V4NzCplmspq5Ux3Zv7CO04GyrqxqvUTEMhld15rGepZ0BZZHg+10vQrAZYf03riv
C9+2l60rb0lF0QJPmV3tYzveDVM0AmWAT+qGLEWakAu9XJe3peJY8hLyJosl00VI
pBYPIg1KVlxYYkEdQLtR6J0o8MENaYMqYA4uJ4cQWe4a9JNKtrAHjaShLjJvPkpX
7bwXrGZd64yNpvgyY6t6LXohUWpA6CGYq0L6RG8uzlbSEMpJnvGz3LgMajeF8vtv
i2XOTErmPimpw2Xlt+DZ2y3sn2aBSbz+m2cjEvgErxgng9ltl9FkrRgK6uoZAj5t
K/fFsAUiKEzz7uDCdQRYrWkoLE7ma65H0FQPnpuHgLChdFWhejpGWzHnaC9VIXfC
ROkiviu2UgauOlm5pr56/OBSupJoh3bdQNEuzhNREkR0LiEDRqtSb6VT4pA6P7n4
Hah5NQLHtjqFTekxhWeeZM0A9YvYTPn9jwtU5MLO+H2k6uTIZWFKKyhrZiJBOAKG
6/e88iW8cBCEmgMawO+oQnjMfBxoIn/22f81JibNJ0ytyIHjsvHEIHA3Fu4X5kKm
b1hO5elC8X7wqCMxweRR";

        private string ColKey2 = @"iQIcBAEBAgAGBQJTXY+2AAoJEOstoML+FoNpSd8QAJPwbs/UtP428eYybeNIZrlT
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
WCGvHcWSA5DUoKzejrV0";

        private string SampleKey = @"iQCVAwUBMXVGMFIa2NdXHZJZAQFe4AQAz0FZrHdH8o+zkIvcI/4ABg4gfE7cG0xE
Z2J9GVWD2zi4tG+s1+IWEY6Ae17kx925JKrzF4Ti2upAwTN2Pnb/x0G8WJQVKQzP
mZcD+XNnAaYCqFz8iIuAFVLchYeWj1Pqxxq0weGCtjQIrpzrmGxV7xXzK0jus+6V
rML3TxQSwdA=";

        private string decryptedProduct =
            "0FB1199FF0733F6E805A4FD3B36CA68E94D7B974621162169C71538A539372E27F3F51DF3B08B2E111C2D6BBF9F5887F13A8DB4F1EB6DFE386C92256875212DDD00468785C18A9C96A292B067DDC71DA0D564000B8BFD80FB14C1B56744A3B5C652E8CA0EF0B6FDA64ABA47E3A4E89423C0212C07E39A5703FD467540F874987B209513429A90B09B049703D54D9A1CFE3E207E0E69785969CA5BF547A36BA34D7C6AEFE79F314E07D9F9F2DD27B72983AC14F1466754CD41262516E4A15AB1CFB622E651D3E83FA095DA630BD6D93E97B0C822A5EB4212D428300278CE6BA0CC7490B854581F0FFB4BA3D4236534DE09459942EF115FAA231B15153D67837A63";

        [TestMethod]
        public void TestPGPublicKey()
        {
            var key = BigInteger.Parse(decryptedProduct, NumberStyles.AllowHexSpecifier);
            if (key < 0) key = -key;

            Assert.AreEqual(
                BigInteger.Parse(
                    "31694494193131919450762706770809468840354434666672424008781573515431625746096585280368250621252631794631453815565120424334098050529875071792744935607775591594371021649824660139042437373082830123871741534477779584668021334953680599012465401947458226088370461865552891478585096543406113181950641833873129358146677978145875731988172846697966127755347490321808734930449792906958988143870053813662496372475791076907799415699868494450132314401903370388663057758988383314607252089667633950836452611098799199281145096780828073017774267281929381261415199175599661354830101985696624851282605981615581853627961488141725060004451"),
                key);

            var sqrt = key.Sqrt();

            Assert.AreEqual(
                BigInteger.Parse(
                    "178029475630109969300656909084112831385666751342187937568449272008557443934015764268568877031262983203075372993953361212761875209322663292191596223015185102821029188521980016728051276417831115151881801435274011935732005749661309839856836265343847106257717350385026393836994658110728633970522903093838140960577"),
                sqrt);
        }

        [TestMethod]
        public void TestCreateK()
        {
            var e = 11;
            var mod = 1716;
            var x = mod / e;

            var maxLargestNumber = 100000000;
            while (x < maxLargestNumber)
            {
                var product = e * x;
                if (product % mod == 1)
                {
                    break;
                }
                x++;
            }

            Assert.AreEqual(4148, x);
        }

        [TestMethod]
        public void TestDeterminingTheFixedDigits()
        {
            var factorValues1 = new Dictionary<int, int[]>(RSA59.Length);
            var factorValues2 = new Dictionary<int, int[]>(RSA59.Length);

            var lastDigit = int.Parse(RSA79[RSA79.Length - 1].ToString());
            Assert.AreEqual(9, lastDigit);

            var product = BigInteger.Parse(RSA59);

            var productMod4 = product % 4;
            var productMod8 = product % 8;

            Assert.AreEqual(1, productMod4, "Both Products are 1 mod 4");
            Assert.AreEqual(5, productMod8, "One Product is 1 mod 8 and the other is 5 mod 8");

            Assert.AreEqual(9, lastDigit, "Numbers that can be multiplied together to get mod 10 to get a 9 in the 1s place: 3 * 3, 7 * 7, 1 * 9");

            factorValues1[RSA59.Length - 1] = new[] { 3, 7, 1 };
            factorValues1[RSA59.Length - 1] = new[] { 3, 7, 9 };

            // case 1 3 * 3
            // valid digits #3 * #3 = __#9 {19, 29, 39, 49, 59, 69, 79, 89} 
            //      03      13      23      33      43      53      63      73      83      93
            // 03   09      39      69      99     129     159     189     219     249     279   
            // 13   39     169     299     429     559     689     819     949    1079    1209  
            // 23   69     299     529     759     989    1219    1449    1679    1909    2139         
            // 33   99     429     759    1089    1419    1749    2079    2409    2739    3069
            // 43  129     559     989    1419    1849    2279    2709    3139    3569    3999
            // 53  159     689    1219    1749    2279    2809    3339    3869    4399    4929
            // 63  189     819    1449    2079    2709    3339    3969    4599    5229    5859
            // 73  219     949    1679    2409    3139    3869    4599    5329    6059    6789
            // 83  249    1079    1909    2739    3569    4399    5229    6059    6889    7719
            // 93  279    1209    2139    3069    3999    4929    5859    6789    7719    8649

            var tensPlace = int.Parse(RSA59[RSA59.Length - 2].ToString(CultureInfo.InvariantCulture));
            Assert.AreEqual(2, tensPlace);
            // case 1 3 * 3
            // valid digits #3 * #3 = __#9 {19, 29, 39, 49, 59, 69, 79, 89} 
            //      03      13      23      33      43      53      63      73      83      93
            // 03                                  129                                           
            // 13                          429   
            // 23                  529                                                                 
            // 33          429                                                                
            // 43  129                                                                        
            // 53                                                                         4929
            // 63                                                                 5229        
            // 73                                                         5329                
            // 83                                                 5229                        
            // 93                                         4929                                

            // case 1 7 * 7
            // valid digits #7 * #7 = __#9 {17, 27, 37, 47, 57, 67, 77, 87} 
            //      07      17      27      37      47      57      67      77      87      97
            // 07   49     119     189     259     329     399     469     539     609     679   
            // 17  119     289     459     629     799     969    1139    1309    1479    1649  
            // 27  189     459     729     999    1269    1539    1809    2079    2349    2619
            // 37  259     629     999    1369    1739    2109    2479    2849    3219    3589
            // 47  329     799    1269    1739    2209    2679    3149    3619    4089    4559
            // 57  399     969    1539    2109    2679    3249    3819    4389    4959    5529
            // 67  469    1139    1809    2479    3149    3819    4489    5159    5829    6499
            // 77  539    1309    2079    2879    3619    4389    5159    5929    7031    7821
            // 87  609    1479    2349    3219    4089    4959    5829    7031    7921    8811
            // 97  679    1649    2649    3589    4559    5529    6499    7821    8811    9801

            // case 1 7 * 7
            // valid digits #7 * #7 = __29 {17, 27, 37, 47, 57, 67, 77, 87} 
            //      07      17      27      37      47      57      67      77      87      97
            // 07                                  329                                           
            // 17                          629                                                  
            // 27                  729                                                        
            // 37          629                                                                 
            // 47  329                                                                        
            // 57                                                                         5529
            // 67                                                                 5829        
            // 77                                                         5929                
            // 87                                                 5829                        
            // 97                                         5529                                


        }

        [TestMethod]
        public void TestMapMod4()
        {
            var RSA59Base10 = BigInteger.Parse(RSA59);

            var base4Result = ToBase4String(RSA59Base10);
            Assert.AreEqual("23122130101200132002130121020131301231000303230201301033211322101111323231232003320321212323003211", base4Result);
        }

        private static string ToBase4String(BigInteger RSA59Base10)
        {
            var digits = new Dictionary<int, BigInteger>();
            var length = RSA59Base10.ToByteArray().Length;

            var digit = 0;
            while (digit <= length * 4 && RSA59Base10 > 0)
            {
                digits[digit] = RSA59Base10 % 4;
                RSA59Base10 /= 4;
                digit++;
            }

            StringBuilder sb = new StringBuilder(digits.Count);
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                sb.Append(digits[i]);
            }

            var base4Result = sb.ToString();
            return base4Result;
        }

        private static string ToBase12String(BigInteger RSA59Base10)
        {
            var digits = new Dictionary<int, BigInteger>();
            var length = RSA59Base10.ToByteArray().Length;

            var digit = 0;
            while (digit <= length * 4 && RSA59Base10 > 0)
            {
                digits[digit] = RSA59Base10 % 12;
                RSA59Base10 /= 12;
                digit++;
            }

            StringBuilder sb = new StringBuilder(digits.Count);
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                if (digits[i] < 10)
                {
                    sb.Append(digits[i]);
                }
                else if (digits[i] == 10)
                {
                    sb.Append("A");
                }
                else if (digits[i] == 11)
                {
                    sb.Append("B");
                }
            }

            var base4Result = sb.ToString();
            if (base4Result.Length == 1)
            {
                base4Result = "0" + base4Result;
            }
            return base4Result;
        }

        [TestMethod]
        public void TestMod4ConstraintsOnRSA59()
        {
            var onesPlace = new [] { 1, 3 };
            var foursPlace = new[] { 0, 1, 2, 3 };
            var sixteensPlace = new[] { 0, 1, 2, 3 };

            var xList = new List<int>();
            var yList = new List<int>();

            foreach (var times16x in sixteensPlace)
            {
                foreach (var times4x in foursPlace)
                {
                    foreach (var times1x in onesPlace)
                    {
                        var number = Convert.ToInt32((times16x * 16 + times4x * 4 + times1x).ToString());
                        xList.Add(number);
                        yList.Add(number);
                    }
                }
            }

            var sb = new StringBuilder();
            sb.Append("\t");
            foreach (var x in xList)
            {
                sb.Append(ToBase4String(new BigInteger(x)) + "\t");
            }
            sb.Append("\n");

            foreach (var y in yList)
            {
                sb.Append(ToBase4String(y) + "\t");

                foreach (var x in xList)
                {
                    var product = y * x;
                    var digitsAsString = ToBase4String(new BigInteger(product));
                    string lastDigits;
                    if (x.ToString().Length <= 2)
                    {
                        if (digitsAsString.Length <= 2) lastDigits = digitsAsString;
                        else lastDigits = digitsAsString.Substring(Math.Max(digitsAsString.Length - 2, 2));
                    }
                    else
                    {
                        if (digitsAsString.Length <= 3) lastDigits = digitsAsString;
                        else lastDigits = digitsAsString.Substring(Math.Max(digitsAsString.Length - 3, 3));
                    }

                    if (x.ToString().Length == 2 && lastDigits == "11")
                    {
                        sb.Append(digitsAsString + "\t");
                    }
                    else if (x.ToString().Length == 3 && lastDigits == "112") 
                    {
                        sb.Append(digitsAsString + "\t");
                    }
                    else
                    {
                        sb.Append("\t");
                    }
                }
                sb.Append("\n");
            }
            Assert.IsTrue(sb.Length > 2);

            var sb2 = new StringBuilder();
            //foreach(var string in matrix)

        }

        [TestMethod]
        public void DisplayRSA59Mod12()
        {
            var RSA59Base10 = BigInteger.Parse(RSA59);

            var base12Result = ToBase12String(RSA59Base10);
            Assert.AreEqual("396831A485A018B1A8728AA91B21278773846B71018A064A5439205", base12Result);

            //  357440504101388365610785389017
            //  200429218120815554269743635437
            var p1 = BigInteger.Parse("357440504101388365610785389017");
            var p2 = BigInteger.Parse("200429218120815554269743635437");
            var p1base12 = ToBase12String(p1);
            var p2base12 = ToBase12String(p2);

            Assert.AreEqual("2728350B7569896710948B428335", p1base12);
            Assert.AreEqual("1561280011402A58226499018891", p2base12);
        }

        [TestMethod]
        public void TestModTwelve()
        {
            var twelvesPlace = new[] { 1, 5, 43, 47, 49, 53, 67, 71, 73, 77, 91, 95, 101, 103, 107, 109, 113, 127, 131, 133, 137 };
            var OneFortyfouronesPlace = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            var xList = new List<int>();
            var yList = new List<int>();

            foreach (var times144x in OneFortyfouronesPlace)
            {
                foreach (var times12x in twelvesPlace)
                {
                    var number = Convert.ToInt32((times144x * 144 + times12x * 1).ToString());   
                    xList.Add(number);
                    yList.Add(number);
                }
           }

            var sb = new StringBuilder();
            sb.Append("\t");
            foreach (var x in xList)
            {
                var digitString = ToBase12String(new BigInteger(x));
                sb.Append(digitString + "\t");
            }
            sb.Append("\n");

            foreach (var y in yList)
            {
                sb.Append(ToBase12String(y) + "\t");

                foreach (var x in xList)
                {
                    var product = y * x;
                    var digitsAsString = ToBase12String(new BigInteger(product));
                    if (digitsAsString.Length == 1) digitsAsString = "0" + digitsAsString;
                    string lastDigits;
                    if (x.ToString().Length <= 2)
                    {
                        if (digitsAsString.Length <= 2) lastDigits = digitsAsString;
                        else lastDigits = digitsAsString.Substring(Math.Max(digitsAsString.Length - 2, 2));
                    }
                    else
                    {
                        if (digitsAsString.Length <= 3) lastDigits = digitsAsString;
                        else lastDigits = digitsAsString.Substring(Math.Max(digitsAsString.Length - 3, 3));
                    }

                    if (x.ToString().Length <3 && lastDigits == "05")
                    {
                        sb.Append(digitsAsString + "\t");
                    }
                    else if (x.ToString().Length == 3 && lastDigits == "205")
                    {
                        sb.Append(digitsAsString + "\t");
                    }
                    else
                    {
                        sb.Append("\t");
                    }
                }
                sb.Append("\n");
            }
            Assert.IsTrue(sb.Length > 2);

            var sb2 = new StringBuilder();
            //foreach(var string in matrix)
        }

        /// <summary>
        /// product - 1:
        /// 2^2 * 7 * 617 * 4984428238241711837 * 831967292613905141715382511023497619
        /// 
        /// </summary>
        [TestMethod]
        public void TestFactorRSA59Minus1()
        {
            var rsa59 = BigInteger.Parse(RSA59);
            var lessOne = rsa59 - 1;
            var factor = new Factor() { Product = lessOne };
            int i = 0;
            for (i = 0; i < 1000000000; i++)
            {
                if (factor.FastFactor())
                {
                    break;
                }
            }
            Assert.AreEqual(6, factor.Factors.Length);
        }

        [TestMethod]
        public void TestQuadraticResidualOfFactors()
        {
            var rsa59 = BigInteger.Parse(RSA59);
            var largestFactor = BigInteger.Parse("831967292613905141715382511023497619");
            var modulus = rsa59 % largestFactor;

            Assert.AreEqual(1, modulus);

            var nextLargestFactor = BigInteger.Parse("4984428238241711837");
            modulus = rsa59 % nextLargestFactor;
            Assert.AreEqual(1, modulus);

            modulus = rsa59 % 617;
            Assert.AreEqual(1, modulus);

            modulus = rsa59 % 7;
            Assert.AreEqual(1, modulus);

            modulus = rsa59 % 2;
            Assert.AreEqual(1, modulus);

            modulus = rsa59 % 4;
            Assert.AreEqual(1, modulus);

            modulus = rsa59 % (617 * 2);

            modulus = rsa59 % (BigInteger.Parse("831967292613905141715382511023497619") * 2);

            var factor = new Factor { Product = modulus };
            int i;
            for (i = 0; i < 1000000000; i++)
            {
                if (factor.FastFactor())
                {
                    break;
                }
            }
            Assert.AreEqual(12, factor.Factors.Length);
        }

        [TestMethod]
        public void TestCreateCongurentSquares()
        {
            var first = BigInteger.Parse("831967292613905141715382511023497619") * 2;
            var second = BigInteger.Parse("831967292613905141715382511023497619") * 7;
        }

        [TestMethod]
        public void TestSumarianMethod()
        {
            var rsa59Number = BigInteger.Parse(RSA59);

            //  357440504101388365610785389017
            //  200429218120815554269743635437
            var factor1 = BigInteger.Parse("357440504101388365610785389017");
            var factor2 = BigInteger.Parse("200429218120815554269743635437");

            var productMap = MultiplicationMap(rsa59Number);
            Assert.IsNotNull(productMap);
            var factor1MultMap = MultiplicationMap(factor1);
            var factor2MultMap = MultiplicationMap(factor2);
            var comparison = factor1MultMap.PadLeft(productMap.Length) + Environment.NewLine +
                             factor2MultMap.PadLeft(productMap.Length) + Environment.NewLine +
                             productMap;
            Assert.IsNotNull(comparison);
            var sb = new StringBuilder();
            var number = rsa59Number;
            foreach (var c in productMap)
            {
                if (c == '1')
                {
                    sb.AppendLine(number.ToString().PadLeft(RSA59.Length) + " - 1");
                }
                else
                {
                    sb.AppendLine(number.ToString().PadLeft(RSA59.Length) + " - 0");
                }
                number /= 2;
            } //53897089881444119075949 
            var divMap = sb.ToString();
            Assert.IsNotNull(divMap);
        }

        [TestMethod]
        public void TestSumarianMethodOnSmall()
        {
            var factor1 = BigInteger.Parse("3253");
            var factor2 = BigInteger.Parse("3061");
            //var factor2 = BigInteger.Parse("2473");
            var product = factor1 * factor2;

            var factor1MultMap = MultiplicationMap(factor1);
            var factor2MultMap = MultiplicationMap(factor2);
            var productMap = MultiplicationMap(product);
            var comparison = new StringBuilder();
            //comparison.AppendLine(factor1MultMap.PadLeft(productMap.Length));
            for (int i = 0; i < factor1MultMap.Length - 1; i++)
            {
                comparison.Append(factor1MultMap[i].ToString().PadLeft(2));
            }
            comparison.AppendLine(factor1MultMap[factor1MultMap.Length - 1].ToString());
            for (int i = 0; i < factor2MultMap.Length - 1; i++)
            {
                comparison.Append(factor2MultMap[i].ToString().PadLeft(2));
                //comparison.Append(factor2MultMap.PadLeft(productMap.Length - i));
                //comparison.AppendLine("".PadRight(i, '0'));
            }
            comparison.AppendLine(factor2MultMap[factor2MultMap.Length - 1].ToString());
            comparison.AppendLine(productMap);
            var stringResult = comparison.ToString();
            Assert.IsNotNull(stringResult);
            var sb = new StringBuilder();
            var number = product;
            foreach (var c in productMap)
            {
                if (c == '1')
                {
                    sb.AppendLine(number.ToString().PadLeft(RSA59.Length) + " - 1");
                }
                else
                {
                    sb.AppendLine(number.ToString().PadLeft(RSA59.Length) + " - 0");
                }
                number /= 2;
            } //53897089881444119075949 
            var divMap = sb.ToString();
            Assert.IsNotNull(divMap);
        }



        private static string MultiplicationMap(BigInteger thevalue)
        {
            var number = thevalue;
            var sqrt = number.Sqrt();
            var sb = new StringBuilder();
            var oddSum = new BigInteger(0);
            var evenSum = new BigInteger(0);
            var startGuess = new BigInteger(0);
            var lowOddSum = new BigInteger(0);
            var lowEvenSum = new BigInteger(0);
            var addValue = true;
            var startCountLow = false;
            var startValue = new BigInteger(0);
            while (number > 1)
            {
                if (!startCountLow && number < sqrt)
                {
                    startCountLow = true;
                    startValue = number;
                }

                if (number.IsEven)
                {
                    sb.Append("0");
                    evenSum += number;
                    if (startCountLow) //lowOddSum > 0)
                    {
                        lowEvenSum += number;
                    }
                }
                else
                {
                    if (startCountLow)
                    {
                        if (addValue) startGuess += number;
                        else startGuess -= number;
                        addValue = !addValue;
                        lowOddSum += number;
                    }
                    sb.Append("1");
                    oddSum += number;
                }
                number /= 2;
            }
            sb.Append(number == 2 ? "01" : "1");
            var result = sb.ToString();
            Assert.IsTrue(oddSum > 0);
            return result;
        }
    }
}
