using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnLinq
{
    class Program
    {
        static void Main()
        {
            List<string> heroes =
                new List<string> {
                    "D. Va",
                    "Lucio",
                    "Mercy",
                    "Soldier 76",
                    "Pharah",
                    "Reinhardt"
                };

            //using var with LINQ.
            var shortHeroes = from h in heroes where h.Length < 8 select h;

            // Printing...
            Console.WriteLine("Your short heroes are...");

            foreach (string hero in shortHeroes)
            {
                Console.WriteLine (hero);
            }

            //method syntax.
            var longHeroes = heroes.Where(n => n.Length > 8);

            //count the longHeroes
            Console.WriteLine(longHeroes.Count());

            // Query syntax
            var queryResult =
                from x in heroes
                where x.Contains("a") select $"{x} contains an 'a'";

            // Method syntax
            var methodResult =
                heroes
                    .Where(x => x.Contains("a"))
                    .Select(x => $"{x} contains an 'a'");

            // Printing...
            Console.WriteLine("queryResult:");
            foreach (string s in queryResult)
            {
                Console.WriteLine (s);
            }

            Console.WriteLine("\nmethodResult:");
            foreach (string s in methodResult)
            {
                Console.WriteLine (s);
            }

            //selects all of the elements in heroes that contain the character "i"
            var heroesWithI =
                from hero in heroes where hero.Contains("i") select hero;

            //Write a from - select query
            var underscored = from h in heroes select $"{h.Replace(" ", "_")}";

            //Write a method-syntax query that
            var heroesC = heroes.Where(h => h.Contains("c"));
            var lowerHeroesWithC = heroesC.Select(h => h.ToLower());

            foreach (string h in lowerHeroesWithC)
            {
                Console.WriteLine (h);
            }

            //uses chained expressions
            var sameResult =
                heroes.Where(h => h.Contains("c")).Select(h => h.ToLower());

            foreach (string h in sameResult)
            {
                Console.WriteLine (h);
            }

            // a single operator query (Select()), use method syntax.
            var heroName = heroes.Select(name => $"Introducing...{name}!");

            foreach (string name in heroName)
            {
                Console.WriteLine (name);
            }
            List<string> heroesList =
                new List<string> {
                    "D. Va",
                    "Lucio",
                    "Mercy",
                    "Soldier 76",
                    "Pharah",
                    "Reinhardt"
                };

            //Since this is a multiple operator query (where and select), use query syntax.
            //returns the index of the space in each element.
            var spaceName =
                from h in heroes where h.Contains(" ") select h.IndexOf(" ");
            foreach (int h in spaceName)
            {
                Console.WriteLine (h);
            }

            // a single operator query,
            //contains a period or the number 7
            var heroesWithSpecialChars =
                heroesList.Where(h => h.Contains('.') || h.Contains('7'));

            foreach (var v in heroesWithSpecialChars)
            {
                Console.WriteLine (v);
            }
        }
    }
}
