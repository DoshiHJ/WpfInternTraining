using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace POC01_ConsoleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            decimal addition(decimal no1,decimal no2) {
               return no1 + no2;
              
            }

            decimal subtraction(decimal no1,decimal no2) {  return no1 - no2; }

            decimal multiplication(decimal no1,decimal no2)
            {
                return no1 * no2;
            }

            decimal division(decimal no1,decimal no2) { return (no1 / no2); }

            string choice;
            do
            {
                Console.WriteLine("\n=== Simple Calculator ===");
                Console.WriteLine("\n 1. Addition \n 2.Subtraction \n 3. Multiplication \n 4. Division \n 5.Exit");

                Console.WriteLine("Choose Operation (1-5): ");
                choice = Console.ReadLine();



                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter first number: ");
                        decimal no1 = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter second number: ");
                        decimal no2 = decimal.Parse(Console.ReadLine());

                        decimal sum = addition(no1,no2);
                        Console.WriteLine($"Result: {no1} + {no2} = {sum}");

                        break;

                    case "2":
                        Console.WriteLine("\nEnter first number: ");
                        no1 = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter second number: ");
                        no2 = decimal.Parse(Console.ReadLine());

                        decimal sub = subtraction(no1,no2);
                        Console.WriteLine($"Result: {no1} - {no2} = {sub}");
                        break;

                    case "3":
                        Console.WriteLine("Enter first number: ");
                        no1 = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter second number: ");
                        no2 = decimal.Parse(Console.ReadLine());

                        decimal mul = multiplication(no1,no2);
                        Console.WriteLine($"Result: {no1} * {no2} = {mul}");
                        break;

                    case "4":
                        try
                        {
                            Console.WriteLine("Enter first number: ");
                            no1 = decimal.Parse(Console.ReadLine());
                            Console.WriteLine("Enter second number: ");
                            no2 = decimal.Parse(Console.ReadLine());
                            decimal div = division(no1,no2);

                            Console.WriteLine($"Result: {no1} / {no2} = {div}");
                        }
                        catch (Exception ex) { 
                            Console.WriteLine (ex.Message);
                        }
                        break;

                    case "5":
                        break;

                    default:
                        Console.WriteLine("Enter Valid options");
                        break;
                }

            }
            while (choice != "5");

 
            }
        }
    }

