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

  public class Book : NamedObject
  {
    private List<double> grades;

    // const vs readonly keywords
    readonly string category = "Science";
    public const string CATEGORY = "Math";

    public Book(string name) : base(name)
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

    public void AddGrade(double grade)
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
  }
}