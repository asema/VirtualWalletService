using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationServices.Utility
{
    public class RandomGenerator
    {
        public static string GenerateAccountNumber(int length)
        {
            Random random = new Random();
            const string pool = "012345678965218093592784109379270863826379";
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[random.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }
    }
}
