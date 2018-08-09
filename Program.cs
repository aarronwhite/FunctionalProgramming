using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();

            var poisonedApples = program.PickApples()
                .Take(10000)
                .Where(a => a.Poisoned == true)
                .Count();
            Console.WriteLine($"Poisoned Apples in 10,000 = {poisonedApples}");

            var mostPoisonedColour = program.PickApples()
                .Take(10000)
                .Where(apple => apple.Poisoned)
                .GroupBy(apple => apple.Colour)
                .Select(apple => new { colour = apple.Key, count = apple.Count() })
                .OrderByDescending(a => a.count)
                .Skip(1)
                .First();
            Console.WriteLine($"Most Common Colour for Poisoned Apples = {mostPoisonedColour.colour} with a count of {mostPoisonedColour.count}");

            var nonPoisonedRedSuccesson = program.PickApples()
                .Take(10000)
                .TakeWhile(a => !a.Poisoned)
                .Where(apple => apple.Colour == "Red")
                .Count();
            Console.WriteLine($"Max Non Poisoned Red Apples In Succession = {nonPoisonedRedSuccesson}");

            var greenAppleCombo = program.PickApples()
                .Take(1000)
                .Skip(1)
                .Zip(program.PickApples()
                .Take(10000), (a, b) => a.Colour == "Green" && b.Colour == "Green")
                .Count();
            Console.WriteLine($"Total Number Of Green Apples In a Sequence = {greenAppleCombo}");
            Console.ReadLine();



        }
        private IEnumerable<Apple> PickApples()
        {
            int colourIndex = 1;
            int poisonIndex = 7;

            while (true)
            {
                yield return new Apple
                {
                    Colour = GetColour(colourIndex),
                    Poisoned = poisonIndex % 41 == 0
                };

                colourIndex += 5;
                poisonIndex += 37;
            }
        }

        private string GetColour(int colourIndex)
        {
            if (colourIndex % 13 == 0 || colourIndex % 29 == 0)
            {
                return "Green";
            }

            if (colourIndex % 11 == 0 || colourIndex % 19 == 0)
            {
                return "Yellow";
            }

            return "Red";
        }

        private class Apple
        {
            public string Colour { get; set; }
            public bool Poisoned { get; set; }

            public override string ToString()
            {
                return $"{Colour} apple{(Poisoned ? " (poisoned!)" : "")}";
            }
        }
    }
}
    
