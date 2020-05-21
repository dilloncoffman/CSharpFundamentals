using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook {
  public class NamedObject // Ideally would be in its own file, but for learning purposes it's okay to have inside Book.cs
  {
    public NamedObject (string name) {
      Name = name;
    }

    public string Name // Book will inherit this property
    {
      get;
      set;
    }
  }

  public interface IBook // C# convention to begin interface names with "I"
  {
    void AddGrade (double grade);
    Statistics GetStatistics ();
    string Name { get; } // interface guarantees Name will AT LEAST have a getter
  }

  public abstract class Book : NamedObject, IBook // classes that derive from this class, should have different implementations of AddGrade
  {
    protected Book (string name) : base (name) { }

    public abstract void AddGrade (double grade);

    public abstract Statistics GetStatistics ();
  }

  public class DiskBook : Book {
    public DiskBook (string name) : base (name) { }

    public override void AddGrade (double grade) {
      using (var writer = File.AppendText ($"{Name}.txt")) {
        writer.WriteLine (grade);
      }
    }

    public override Statistics GetStatistics () {
      var result = new Statistics();

      using(var reader = File.OpenText($"{Name}.txt"))
      {
        var line = reader.ReadLine();
        while (line != null) {
          var number = double.Parse(line);
          result.Add(number);
          line = reader.ReadLine();
        }
      }

      return result;
    }
  }

  public class InMemoryBook : Book // Named InMemoryBook because it's AddGrade method will store the grades in memory, whereas another class that extends BookBase may choose to store the grades in a file or over a network
  {

    public InMemoryBook (string name) : base (name) {
      category = ""; // readonly vars can be changed/reinitialized in constructor
      this.Name = name;
      this.grades = new List<double> ();
    }

    public void AddGrade (char letter) {
      switch (letter) {
        case 'A':
          AddGrade (90);
          break;
        case 'B':
          AddGrade (80);
          break;
        case 'C':
          AddGrade (70);
          break;
        case 'D':
          AddGrade (60);
          break;
        default:
          AddGrade (0);
          break;
      }
    }

    public override void AddGrade (double grade) {

      if (grade <= 100 && grade >= 0)
        this.grades.Add (grade);
      else
        throw new ArgumentException ($"Invalaid {nameof(grade)}");
    }

    public override Statistics GetStatistics () {
      var result = new Statistics ();

      foreach (var grade in grades) {
        result.Add (grade);
      }

      return result;
    }

    private List<double> grades;

    // const vs readonly keywords
    readonly string category = "Science";
    public const string CATEGORY = "Math";
  }
}