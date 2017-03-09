using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Collection.Classes;

namespace Student_Collection
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentCollection students = new StudentCollection();
            students.Add(new Student(1, "Артем", "KhAI", 10));
            students.Add(new Student(5, "Вас", "KhNURE", 1));
            students.Add(new Student(3, "Петя", "KhNURE", 50));
            students.Add(new Student(2, "Герман", "Karazina", 8));
            students.Add(new Student(50, "Тернеок", "KhAI", 8));
            students.Add(new Student(21, "Янкевич", "Karazina", 8));
            students.Add(new Student(22, "Артем", "Karazina", 8));

            StudentCollection abc = students.GetStudentsByAssessment(8);

            foreach (Student item in abc)
            {
                Console.WriteLine(item);
            }
        }
    }
}
