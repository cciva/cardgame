using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using System.Linq;

namespace Game.Lib
{
    public static class CollectionExtensions
    {
        // here we are using Fisher Yates Shuffle algo
        // code taken from :
        // https://web.archive.org/web/20150801085341/http://blog.thijssen.ch/2010/02/when-random-is-too-consistent.html
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> src)
        {
            RandomNumberGenerator rng = null;
            List<T> shuffled = null;
            int count = 0;

            if(src == null || src.Count() == 0)
                return src;

            rng = new RNGCryptoServiceProvider();
            shuffled = new List<T>(src);
            count = src.Count();  
            
            while (count > 1)  
            {  
                T val;
                byte[] box;
                int index;
                box = new byte[1];

                do 
                {
                    rng.GetBytes(box);
                }
                while (!(box[0] < count * (Byte.MaxValue / count)));

                index = (box[0] % count);  
                count--;
                val = shuffled[index];  

                // swap them
                shuffled[index] = shuffled[count];  
                shuffled[count] = val;  
            }

            return shuffled;
        }
    }
}