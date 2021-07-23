using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Atividdade.Entities;

namespace Atividdade
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();
            Console.Write("Enter salary: ");
            double sal = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            List<Employee> list = new List<Employee>();

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] vect = sr.ReadLine().Split(",");
                        string name = vect[0];
                        string email = vect[1];
                        double salary = double.Parse(vect[2], CultureInfo.InvariantCulture);
                        list.Add(new Employee(name, email, salary));
                    }
                }

                
                var order = list.Where(p => p.Salary > sal).OrderBy(p => p.Email).Select(p => p.Email);

                var sumMs = list.Where(p => p.Name[0] == 'M').Sum(p => p.Salary);

                Console.WriteLine("Email of people whose salary is more than " + sal.ToString("F2", CultureInfo.InvariantCulture));
                foreach (string emails in order)
                {
                    Console.WriteLine(emails);
                }

                Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sumMs.ToString("F2", CultureInfo.InvariantCulture));

            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }

        }
    }
}
