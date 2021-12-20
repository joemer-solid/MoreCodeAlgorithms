using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxSubstringPower
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = new string[]
            {
                "onomatopoeia",
                "amanaplanacanalpanama",
                "wowbobwow",
                "eggregious",
                "polymorphism",
                "surreptitiously",
                "eschatological",
                "deleterious",
                "anonymity"
            };

            words.ToList()
                .ForEach(word =>
                {
                    (string character, int power) = GetMaxSubstringPower(word);
                    Console.WriteLine($"For the word: {word} Character: {character} Power: {power}");
                });
         

            Console.ReadLine();

        }

        private static (string, int) GetMaxSubstringPower(string input)
        {
            IDictionary<byte, int> characterCounts = new Dictionary<byte, int>();
            byte[] asciiBytes = Encoding.ASCII.GetBytes(input.ToLower());

            asciiBytes.ToList()
                .ForEach(byteChar =>
                {
                    if (characterCounts.ContainsKey(byteChar))
                    {
                        characterCounts[byteChar] += 1;
                    }
                    else
                    {
                        characterCounts[byteChar] = 1;
                    }
                });  

            KeyValuePair<byte, int> maxPowerKvp = characterCounts
            .FirstOrDefault(x => x.Value == characterCounts
            .Max(kvp => kvp.Value));

            (string, int) subStringPowerResult =
            (
                Encoding.ASCII.GetString(new byte[] { maxPowerKvp.Key }).Trim(),
                maxPowerKvp.Value
            );

            return subStringPowerResult;

        }

    }
}
