## Introduction to LINQ

Suppose that we want to find all the names in a list which are longer than 6 letters and return them in all uppercase letters. You can see what it would look like in `Program.cs` in the code editor.

And remember that this only works in a running C# file. What if the database was stored in a separate server somewhere and it was implemented with SQL instead of C#?

The solution is LINQ. It works for complex selections and transformations, and it works on local and remote data sources. Each selection/transformation is called a query, and LINQ gives us new syntax and methods to write them.

Imagine LINQ like an add-on to C# and .NET. Once you import the LINQ features, you can write new syntax, like:

```
string[] names = { "Tiana", "Dwayne", "Helena" };
var filteredNames = from n in names
  where n.Contains("a")
  select n;
```

And you can use new methods on collections, like Where():
`var shortNames = names.Where(n => n.Length < 4);`


```
using System;
using System.Collections.Generic;

using System.Linq;

namespace LearnLinq
{
  class Program
  {
    static void Main()
    {
      List<string> heroes = new List<string> { "D. Va", "Lucio", "Mercy", "Soldier 76", "Pharah", "Reinhardt" };
      
      // Approach 1: without LINQ
      List<string> longLoudHeroes = new List<string>();
      
      foreach (string hero in heroes)
      {
        if (hero.Length > 6)
        {
          string formatted = hero.ToUpper();
          longLoudHeroes.Add(formatted);
        }
      }
      
      // Approach 2: with LINQ
      var longLoudHeroes2 = from h in heroes
            where h.Length > 6
            select h.ToUpper();
      
      // Printing...
      Console.WriteLine("Your long loud heroes are...");
      
      foreach (string hero in longLoudHeroes2)
      {
        Console.WriteLine(hero);
      }
    }
  }
}


```

---
## Importing LINQ

Before we jump into the syntax and methods, let’s import the features into our code. To use LINQ in a file, add this line to the top:
`using System.Linq;`

Often times we use LINQ with generic collections (like lists), so you may see both namespaces imported into a file:
```
using System.Collections.Generic;
using System.Linq;
```

---
## Var


Every LINQ query returns either a single value or an object of type `IEnumerable<T>`. For now, all you need to know about that second type is that:

It works with `foreach` loops, just like arrays and lists
You can check its length with `Count()`
Since the single value type and/or the parameter type `T `is not always known, it’s common to store a query’s returned value in a variable of type `var`.

`var` is just an implicitly typed variable — we let the C# compiler determine the actual type for us. Here’s one example:

```
string[] names = { "Tiana", "Dwayne", "Helena" };
var shortNames = names.Where(n => n.Length < 4);
```
In this case `shortNames` is actually of type `IEnumerable<string>`, but we don’t need to worry ourselves about that as long as we have `var`!

---
## Method and Query Syntax

In LINQ, you can write queries in two ways: in query syntax and method syntax.

Query syntax looks like a multi-line sentence. If you’ve used SQL, you might see some similarities:

```
var longLoudHeroes = from h in heroes
  where h.Length > 6
  select h.ToUpper();

```
Method syntax looks like plain old C#. We make method calls on the collection we are querying:

```
var longHeroes = heroes.Where(h => h.Length > 6);
var longLoudHeroes = longHeroes.Select(h => h.ToUpper());
```

In LINQ, we see `where/Where() and select/Select()` show up as both keywords and method calls. To cover both cases, they’re generally called operators.

---
## Basic Query Syntax
A basic LINQ query, in query syntax, has three parts:

```
string[] heroes = { "D. Va", "Lucio", "Mercy", "Soldier 76", "Pharah", "Reinhardt" };
 
var shortHeroes = from h in heroes
  where h.Length < 8
  select h;

```

- The `from` operator declares a variable to iterate through the sequence. In this case, `h` is used to iterate through `heroes`.

- The `where `operator picks elements from the sequence if they satisfy the given condition. The condition is normally written like the conditional expressions you would find in an if statement. In this case, the condition is `h.Length < 8`.

- The `select` operator determines what is returned for each element in the sequence. In this case, it’s just the element itself.

The `from` and `select` operators are required, `where` is optional. In this next example, `select` is used to make a new string starting with “Hero: “ for each element:

```
var heroTitles = from hero in heroes
  select $"HERO: {hero.ToUpper()}";
```

Each element in `heroTitles` would look like `"HERO: D. VA"`, `"HERO: LUCIO"`, etc.

---
## Basic Method Syntax: Where

In method syntax, each query operator is written as a regular method call.

```
string[] heroes = { "D. Va", "Lucio", "Mercy", "Soldier 76", "Pharah", "Reinhardt" };
var shortHeroes = heroes.Where(h => h.Length < 8);
```

The `where `operator is written as the method `Where()`, which takes a lambda expression as an argument. Remember that lambda expressions are a quick way to write a method. In this case, the method returns `true` if `h` is less than 8 characters long.


`Where()` calls this lambda expression for every element in `heroes`. If it returns `true`, then the element is added to the resulting collection.

For example, the shortHeroes sequence from above would be:
`[ D. Va, Lucio, Mercy, Pharah ]`

---
## Basic Method Syntax: Select
To transform each element in a sequence — like writing them in uppercase — we can use the `select` operator. In method syntax it’s written as the method `Select()`, which takes a lambda expression:

```
string[] heroes = { "D. Va", "Lucio", "Mercy", "Soldier 76", "Pharah", "Reinhardt" };
var loudHeroes = heroes.Select(h => h.ToUpper());
```

We can combine `Select()` with `Where()` in two ways:

1. Use separate statements:

```
var longHeroes = heroes.Where(h => h.Length > 6);
var longLoudHeroes = longHeroes.Select(h => h.ToUpper());
```

2. Chain the expressions:
```
var longLoudHeroes = heroes
.Where(h => h.Length > 6)
.Select(h => h.ToUpper());
```
---
## When To Use Each Syntax
So far you’ve seen query syntax and two flavors of method syntax.

```
// Query syntax
var longLoudheroes = from h in heroes
  where h.Length > 6
  select h.ToUpper();
 
// Method syntax - separate statements
var longHeroes = heroes.Where(h => h.Length > 6);
var longLoudHeroes = longHeroes.Select(h => h.ToUpper());
 
// Method syntax - chained expressions
var longLoudHeroes2 = heroes
  .Where(h => h.Length > 6)
  .Select(h => h.ToUpper());
```


We generally follow these rules:

- For single operator queries, use method syntax.
- For everything else, use query syntax.
---
## LINQ with Other Collections
You’ve mostly seen LINQ used with arrays, but it can be used for lists as well! The syntax is the same:

```
List<string> heroesList = new List<string> { "D. Va", "Lucio", "Soldier 76" };
 
var longLoudheroes = from h in heroesList
  where h.Length > 6
  select h.ToUpper();
 
// longLoudHeroes is [ "SOLDIER 76" ]
```
Technically, LINQ can be used with any type that supports foreach loops. 
---

## Review

- LINQ is a set of language and framework features for writing structured, type-safe queries over local object collections and remote data sources.

- Use LINQ by referencing the `System.Linq `namespace in your file.

- When a LINQ query returns a sequence of elements its type is `IEnumerable<T>`. That means it works with `foreach` loops and its length is accessible with `Count()`.

- Store a query’s result in a variable of type `var`. `var` is an implicit type, meaning it gets all of the benefits of type-checking without our specifying the actual type.

- LINQ queries can be written in method syntax or query syntax.

- We prefer method syntax for single operations and query syntax for most everything else.



- The `Where` operator is used to select certain elements from a sequence.

- The` Select` operator determines what is returned for each element in the sequence.

- The `from` operator declares a range variable that is used to traverse the sequence.

- LINQ can be used on arrays and lists, among other datatypes.

 Here are some additional resources:

- Learn more by reading [Microsoft’s guide to LINQ](https://learn.microsoft.com/en-us/dotnet/csharp/linq/).

- Find a list of keywords for [query syntax here](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/query-keywords).

- Find a list of common methods for [method syntax here](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/standard-query-operators-overview).

- Find a complete list of methods for `IEnumerable<T>` [here](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-6.0).




