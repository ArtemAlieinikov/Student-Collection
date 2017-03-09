using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Collection.Classes
{
    class Student
    {
        private int assessment;

        public int Id { get; set; }
        public string Name { get; set; }
        public string University { get; set; }
        public int Assessment
        {
            get
            {
                return assessment;
            }
            set
            {
                if (value > 100 || value <= 0)
                {
                    throw new AssessmentException("An assessment must have value between 1 and 100.");
                }
                else
                {
                    assessment = value;
                }
            }
        }

        public Student(int id, string name, string university, int assessment)
        {
            Id = id;
            Name = name;
            University = university;
            Assessment = assessment;
        }

        public override string ToString()
        {
            return String.Format("ID - {0}, Name - {1}, University - {2}, Assessment - {3}", Id, Name, University, Assessment);
        }
    }
}
