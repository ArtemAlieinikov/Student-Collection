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
            students.Add(new Student(1, "Артем", "KhAI", 100));
            students.Add(new Student(2, "Вас", "KhNURE", 75));
            students.Add(new Student(3, "Петя", "KhNURE", 70));
            students.Add(new Student(4, "Герман", "Karazina", 30));
            students.Add(new Student(5, "Яся", "KhAI", 50));
            students.Add(new Student(6, "Артур", "Karazina", 65));
            students.Add(new Student(7, "Олег", "Karazina", 45));

            //students.Add(new Student(7, "Олег", "Karazina", 45)); //Исключение(описано в комментариях) - существующий студент

            //students.SortById();
            students.SortByName();
            //students.SortByUniversity();
            Console.WriteLine("Сортировка по имени");
            foreach (Student item in students)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine("Выбираем студента через ID");
            Console.WriteLine(students[1]);
            Console.WriteLine();
            //Console.WriteLine(students[50]); //Исключение(описано в комментариях) - такого студента нет

            Student studentForRemove = new Student(4, "Герман", "Karazina", 30);
            students.Remove(studentForRemove);

            students.SortByAssessment();
            Console.WriteLine("Сортировка по оценками");
            foreach (Student item in students)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Console.WriteLine("Выбираем студентов по ВУЗу");
            foreach (Student item in students.GetStudentsByUniversity("KhAI"))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}
