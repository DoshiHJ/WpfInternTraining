using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC03_StudentResultProcessor
{
    class student
    {
        public int rollnumber {  get; set; }
        public string name {  get; set; }
        public int mathmarks {  get; set; }
        public int sciencemarks {  get; set; }
        public int englishmarks {  get; set; }
        public int totalmarks {  get; set; }
        public double percentage {  get; set; }
        public string grade {  get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<student> students = new List<student>()
            {
                new student
                {
                    rollnumber = 1,name="harsh",mathmarks = 85,sciencemarks = 95, englishmarks = 70, totalmarks = 250,percentage = 83.33,grade = "B"
                },

                new student
                {
                    rollnumber = 2,name="nanu",mathmarks = 80,sciencemarks = 90, englishmarks = 60, totalmarks = 230,percentage = 76.67,grade = "B"
                },

                 new student
                {
                    rollnumber = 2,name="krish",mathmarks = 20,sciencemarks = 30, englishmarks = 40, totalmarks = 90,percentage = 30,grade = "F"
                },
            };
            string choice;
            do
            {
                Console.WriteLine("\n1. Add Student \n2. view student \n3. Search student \n4. Exit");
                Console.WriteLine("\nEnter Choice:");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter RollNumber:");
                        int rollnumber = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Mathmarks:");
                        int mathmarks = int.Parse(Console.ReadLine());
                        if(mathmarks<0 || mathmarks > 100)
                        {
                            do
                            {
                                Console.WriteLine("marks must be 0 to 100");
                                Console.WriteLine("Enter Mathmarks:");
                                mathmarks = int.Parse(Console.ReadLine());
                            }
                            while (mathmarks < 0 || mathmarks > 100);
                        }
                        Console.WriteLine("Enter Sciencemarks:");
                        int sciencemarks = int.Parse(Console.ReadLine());
                        if (sciencemarks < 0 || sciencemarks > 100)
                        {
                            do
                            {
                                Console.WriteLine("marks must be 0 to 100");
                                Console.WriteLine("Enter ScienceMarks:");
                                sciencemarks = int.Parse(Console.ReadLine());
                            }
                            while (sciencemarks < 0 || sciencemarks > 100);
                        }
                        Console.WriteLine("Enter EnglishMarks:");
                        int englishmarks = int.Parse(Console.ReadLine());
                        if (englishmarks < 0 || englishmarks > 100)
                        {
                            do
                            {
                                Console.WriteLine("marks must be 0 to 100");
                                Console.WriteLine("Enter Mathmarks:");
                                englishmarks = int.Parse(Console.ReadLine());
                            }
                            while (englishmarks < 0 || englishmarks > 100);
                        }

                        int totalmarks = mathmarks + sciencemarks + englishmarks;

                        double percentage = (double)totalmarks / 3;

                        string grade = "";


                        if (percentage >= 90)
                        {
                            grade = "A";
                        }
                        else if(percentage>=75 && percentage <= 89)
                        {
                            grade = "B";
                        }

                        else if (percentage >= 60 && percentage <= 74)
                        {
                            grade = "C";
                        }

                        else if (percentage >= 40 && percentage <= 59)
                        {
                            grade = "D";
                        }

                        else
                        {
                            grade = "F";
                        }

                        students.Add(new student
                        {
                            rollnumber = rollnumber,
                            name = name,
                            mathmarks = mathmarks,
                            sciencemarks = sciencemarks,
                            englishmarks = englishmarks,
                            totalmarks = totalmarks,
                            percentage = percentage,
                            grade = grade
                        });


                        break;

                    case "2":
                        Console.WriteLine("=== Student Result Sheet ===");
                        Console.WriteLine("{0,-10} {1,-7} {2,-15} {3,-15} {4,-15} {5,-16} {6,-10} {7,-7}","rollnumber", "name","mathmarks", "sciencemarks", "englishmarks", "totalmarks", "percentage", "grade");

                        Console.WriteLine(new string('-', 100));
                        foreach (var student in students)
                        {
                            //Console.WriteLine($"rollnumber:{student.rollnumber} Name:{student.name} Mathmarks:{student.mathmarks} Sciencemarks:{student.sciencemarks} Englishmarks{student.englishmarks} Total:{student.totalmarks} Percentage:{student.percentage} Grade:{student.grade}");

                            Console.WriteLine("{0,-10} {1,-7} {2,-15} {3,-15} {4,-15} {5,-16} {6,-10} {7,-7}", student.rollnumber,student.name,student.mathmarks,student.sciencemarks,student.englishmarks,student.totalmarks,student.percentage,student.grade);
                        }

                        Console.WriteLine(new string('-', 100));

                        Console.WriteLine("\n === Class Statistics ===");

                        var count=students.Count;
                        Console.WriteLine("Total Students:"+count);

                        var average = students.Average(s => s.percentage);
                        Console.WriteLine($"Average Percentage: {Math.Round(average,2)}");
                        //Console.WriteLine($"Average Percentage: {average:F2}");             

                        var htotal = students.Max(s => s.totalmarks);
                        var htotalname=students.FirstOrDefault(s=>s.totalmarks==htotal).name;
                        Console.WriteLine($"Highest Total: {htotal} {htotalname}");

                        Console.WriteLine("\nSubject Toppers: ");

                        var math = students.Max(s => s.mathmarks);
                        var mathname = students.FirstOrDefault(s => s.mathmarks==math).name;

                        Console.WriteLine($"\nMaths:{mathname} {math}");

                        var science = students.Max( s => s.sciencemarks);
                        var sciencename = students.FirstOrDefault(s => s.sciencemarks == science).name;

                        Console.WriteLine($"Science:{sciencename} {science}");

                        var english = students.Max(s => s.englishmarks);
                        var englishname = students.FirstOrDefault(s=>s.englishmarks==english).name;

                        Console.WriteLine($"English:{englishname} {english}");

                        Console.WriteLine("\nGrade Distribution: ");

                        var group = students.GroupBy(s => s.grade);
                        foreach (var g in group)
                        {
                            Console.WriteLine($"{g.Key} {g.Count()} student(s)");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Enter RollNo you want to show result");
                        int srno = int.Parse(Console.ReadLine());

                    
                        var search=students.FirstOrDefault(s=>s.rollnumber==srno);

                        if (search != null)
                        {
                            Console.WriteLine("{0,-10} {1,-7} {2,-15} {3,-15} {4,-15} {5,-16} {6,-10} {7,-7}", "rollnumber", "name", "mathmarks", "sciencemarks", "englishmarks", "totalmarks", "percentage", "grade");
                            Console.WriteLine(new string('-', 100));
                            Console.WriteLine("{0,-10} {1,-7} {2,-15} {3,-15} {4,-15} {5,-16} {6,-10} {7,-7}", search.rollnumber, search.name, search.mathmarks, search.sciencemarks, search.englishmarks, search.totalmarks, search.percentage, search.grade);
                            Console.WriteLine(new string('-', 100));
                        }
                        else
                        {
                            Console.WriteLine("Roll No is not found");
                        }

                        break;

                    case "4":
                        break;
                    default:
                        Console.WriteLine("Enter valid choice");
                        break;
                }
            }
            while (choice != "4");

        }
    }
}
