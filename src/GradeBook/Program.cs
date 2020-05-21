using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
    {
      var book = new InMemoryBook("Dillon's Grade Book");

      EnterGrades(book);

      var stats = book.GetStatistics();
      Console.WriteLine($"consts are static members on a class: {InMemoryBook.CATEGORY}");
      Console.WriteLine($"For the book named {book.Name}");
      Console.WriteLine($"The lowest grade is {stats.Low}");
      Console.WriteLine($"The average grade is {stats.Average}");
      Console.WriteLine($"The highest grade is {stats.High}");
      Console.WriteLine($"The letter grade is {stats.Letter}");
    }

    private static void EnterGrades(IBook book)
    {
      while (true)
      {
        Console.WriteLine("Please enter a grade. Enter 'q' when done to quit.");
        var input = Console.ReadLine();
        if (input.Equals("q"))
        {
          break;
        }

        try
        {
          var grade = double.Parse(input);
          book.AddGrade(grade);
        }
        catch (ArgumentException ex)
        {
          Console.WriteLine(ex.Message);
        }
        catch (FormatException ex)
        {
          Console.WriteLine(ex.Message);
        }
        finally
        {
          Console.WriteLine("**");
        }

      }
    }
  }
}
