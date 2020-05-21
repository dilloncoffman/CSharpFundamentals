using System;
using System.Collections.Generic;

namespace GradeBook
{
  public class NamedObject // Ideally would be in its own file, but for learning purposes it's okay to have inside Book.cs
  {
    public NamedObject(string name)
    {
      Name = name;
    }

    public string Name // Book will inherit this property
    {
      get;
      set;
    }
  }

  public abstract class Book: NamedObject // classes that derive from this class, should have different implementations of AddGrade
  {
    protected Book(string name) : base(name)
    {  
    }

    public abstract void AddGrade(double grade);
  }

  public class InMemoryBook : Book // Named InMemoryBook because it's AddGrade method will store the grades in memory, whereas another class that extends BookBase may choose to store the grades in a file or over a network
  {

    public InMemoryBook(string name) : base(name)
    {
      category = ""; // readonly vars can be changed/reinitialized in constructor
      this.Name = name;
      this.grades = new List<double>();
    }

    public void AddGrade(char letter)
    {
        switch(letter)
        {
          case 'A':
            AddGrade(90);
            break;
          case 'B':
            AddGrade(80);
            break;
          case 'C':
            AddGrade(70);
            break;
          case 'D':
            AddGrade(60);
            break;
          default:
            AddGrade(0);
            break;
        }
    }

    public override void AddGrade(double grade)
    {

      if (grade <= 100 && grade >= 0)
        this.grades.Add(grade);
      else
        throw new ArgumentException($"Invalaid {nameof(grade)}");
    }

    public Statistics GetStatistics()
    {
      var result = new Statistics();
      result.Average = 0.0;
      result.High = double.MinValue;
      result.Low = double.MaxValue;

      foreach(var grade in grades)
      {
          result.Low = Math.Min(grade, result.Low);
          result.High = Math.Max(grade, result.High);
          result.Average += grade;
      }
      result.Average /= grades.Count;

      switch(result.Average)
      {
        case var d when d >= 90.0:
          result.Letter = 'A';
          break;
        case var d when d >= 80.0:
          result.Letter = 'B';
          break;
        case var d when d >= 70.0:
          result.Letter = 'C';
          break;
        case var d when d >= 60.0:
          result.Letter = 'd';
          break;
        default:
          result.Letter = 'F';
          break;
      }

      return result;
    }

    private List<double> grades;

    // const vs readonly keywords
    readonly string category = "Science";
    public const string CATEGORY = "Math";
  }
}