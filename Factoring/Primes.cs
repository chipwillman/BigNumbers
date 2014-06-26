namespace Factoring
{
    using System.Collections.Generic;
    using System.IO;
    using System.Numerics;

    public class Primes
    {
        protected static SortedList<int, int> PrimeNumbers
        {
            get
            {
                return primes ?? (primes = LoadPrimes());
            }
        }

        private static SortedList<int, int> LoadPrimes()
        {
            var result = new SortedList<int, int>();
            var path = @"e:\code\Spikes\BigNumbers\Factoring.Test\bin\debug";
            var filePath = Path.Combine(path, "PrimesList.dat").Replace("file:///", "");
            if (File.Exists(filePath))
            {
                var file = File.OpenText(filePath);
                while (!file.EndOfStream)
                {
                    var line = file.ReadLine();
                    if (line.Trim().Length > 0)
                    {
                        var numbers = line.Split(' ');
                        foreach (var number in numbers)
                        {
                            if (number.Trim().Length > 0)
                            {
                                var prime = int.Parse(number);
                                result.Add(prime, prime);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static SortedList<int, int> primes;

        public static BigInteger GetNextPrime(BigInteger start)
        {
            var index = PrimeNumbers.IndexOfKey((int)start);
            if (index < PrimeNumbers.Count)
            {
                return PrimeNumbers[PrimeNumbers.Keys[index + 1]];
            }
            return 0;
        }

        public static BigInteger GetPreviousPrime(BigInteger start)
        {
            var index = PrimeNumbers.IndexOfKey((int)start);
            if (index > 0)
            {
                return PrimeNumbers[PrimeNumbers.Keys[index - 1]];
            }
            return 0;
        }

        public static BigInteger GetNthPrime(int i)
        {
            if (i < PrimeNumbers.Count)
            {
                return PrimeNumbers[PrimeNumbers.Keys[i]];
            }
            return 0;
        }

        public static bool ContainsPrime(BigInteger bigInteger)
        {
            return PrimeNumbers.ContainsKey((int)bigInteger);
        }
    }
}
