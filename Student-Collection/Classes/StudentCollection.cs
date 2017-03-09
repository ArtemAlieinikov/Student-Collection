using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Collection.Classes
{
    class StudentCollection : ICollection<Student>
    {
        private int growIndex;
        private int currentIndex;
        private Student[] students;

        public int Count
        {
            get
            {
                return currentIndex + 1;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public int Capacity
        {
            get
            {
                return students.Length;
            }
        }
        public int GrowIndex
        {
            get
            {
                return growIndex;
            }
        }

        /// <summary>
        ///     Returns the student by Id.
        /// </summary>
        /// <param name="studentId">Id of student.</param>
        /// <returns>Requested student.</returns>
        /// <exception>IncorrectIdException - student with requested id is not exists.</exception>
        public Student this[int studentId]
        {
            get
            {
                int studentIndex;

                if (StudentValidation(new Student(studentId, "Empty", "Empty", 1), out studentIndex))
                {
                    return students[studentIndex];
                }
                else
                {
                    throw new IncorrectIdException(String.Format("There are no students with id = {0}.", studentId));
                }
            }
        }

        public StudentCollection()
        {
            students = new Student[4];
            growIndex = 2;
            currentIndex = -1;
        }
        public StudentCollection(int growIndex)
        {
            students = new Student[4];
            this.growIndex = growIndex;
            currentIndex = -1;
        }
        public StudentCollection(StudentCollection newStudents)
        {
            this.students = newStudents.students;
            this.growIndex = newStudents.growIndex;
            this.currentIndex = newStudents.currentIndex;
        }

        /// <summary>
        ///     It adds a new student.
        /// </summary>
        /// <param name="newStudent">Student which we want to add.</param>
        /// <exception>ArgumentException - There is the student with the same id.</exception>
        public void Add(Student newStudent)
        {
            int studentIndex;
            if (StudentValidation(newStudent, out studentIndex))
            {
                throw new ArgumentException(String.Format("There is the student with {0} id.", students[studentIndex].Id));
            }
            if (++currentIndex == (Capacity / 2))
            {
                GetArrayGrow();
            }

            students[currentIndex] = newStudent;
        }
        public void Clear()
        {
            students = new Student[4];
            currentIndex = -1;
        }
        public bool Contains(Student studentToCheck)
        {
            int a;
            return StudentValidation(studentToCheck, out a);
        }
        public void CopyTo(Student[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("Array is null.");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("Array index is less than 0.");
            }

            if ((currentIndex + 1 - arrayIndex) > array.Length)
            {
                throw new ArgumentException("The number of elements in array is greater than the available space from arrayIndex to the end of the destination array.");
            }

            int j = 0;
            for (int i = arrayIndex; i <= currentIndex; ++i)
            {
                array[j] = students[i];
                ++j;
            }
        }
        public bool Remove(Student studentToRemove)
        {
            int removeIndex;

            if (StudentValidation(studentToRemove, out removeIndex))
            {
                Student[] newStudentsArray = new Student[students.Length - 1];

                int j = 0;
                for (int i = 0; i < newStudentsArray.Length; ++i, ++j)
                {
                    if (j == removeIndex)
                    {
                        ++j;
                    }
                    newStudentsArray[i] = students[j];
                }

                currentIndex--;
                students = newStudentsArray;
                return true;
            }
            else
            {
                return false;
            }
        }

        public StudentCollection GetStudentsByAssessment(int assessment)
        {
            assessment = assessment > 100 ? 100 : assessment <= 0 ? 1 : assessment;

            StudentCollection result = new StudentCollection();

            for (int i = 0; i <= currentIndex; ++i)
            {
                if (students[i].Assessment.CompareTo(assessment) == 0)
                {
                    result.Add(students[i]);
                }
            }

            return result;
        }
        public StudentCollection GetStudentsByName(string name)
        {
            StudentCollection result = new StudentCollection();

            for (int i = 0; i <= currentIndex; ++i)
            {
                if (students[i].Name.CompareTo(name) == 0)
                {
                    result.Add(students[i]);
                }
            }

            return result;
        }
        public StudentCollection GetStudentsByUniversity(string university)
        {
            StudentCollection result = new StudentCollection();

            for (int i = 0; i <= currentIndex; ++i)
            {
                if (students[i].University.CompareTo(university) == 0)
                {
                    result.Add(students[i]);
                }
            }

            return result;
        }

        public void SortById()
        {
            Func<Student, Student, bool> funcOne = (a, b) => a.Id < b.Id;
            Func<Student, Student, bool> funcTwo = (a, b) => a.Id > b.Id;

            QuiqSortForStudents(students, 0, currentIndex, funcOne, funcTwo);
        }
        public void SortByAssessment()
        {
            Func<Student, Student, bool> funcOne = (a, b) => a.Assessment > b.Assessment;
            Func<Student, Student, bool> funcTwo = (a, b) => a.Assessment < b.Assessment;

            QuiqSortForStudents(students, 0, currentIndex, funcOne, funcTwo);
        }
        public void SortByName()
        {
            Func<Student, Student, bool> funcOne = (a, b) => a.Name.CompareTo(b.Name) < 0;
            Func<Student, Student, bool> funcTwo = (a, b) => a.Name.CompareTo(b.Name) > 0;

            QuiqSortForStudents(students, 0, currentIndex, funcOne, funcTwo);
        }
        public void SortByUniversity()
        {
            Func<Student, Student, bool> funcOne = (a, b) => a.University.CompareTo(b.University) < 0;
            Func<Student, Student, bool> funcTwo = (a, b) => a.University.CompareTo(b.University) > 0;

            QuiqSortForStudents(students, 0, currentIndex, funcOne, funcTwo);
        }

        public IEnumerator<Student> GetEnumerator()
        {
            for (int i = 0; i <= currentIndex; ++i)
            {
                yield return students[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     It checks students by Id. If student with same id is exists, it will return true and index of this student.
        ///     T(N) = O(N) because of linear search.
        /// </summary>
        /// <param name="newStudent">Student for check.</param>
        /// <param name="studentIndex">Student id.</param>
        /// <returns>Does student exist? yes - true, no - false.</returns>
        private bool StudentValidation(Student newStudent, out int studentIndex)
        {
            studentIndex = -1;

            for (int i = 0; i <= currentIndex; ++i)
            {
                if (students[i] != null && students[i].Id == newStudent.Id)
                {
                    studentIndex = i;
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        ///     Increasing the array by the growth index.
        /// </summary>
        private void GetArrayGrow()
        {
            Array.Resize(ref students, Capacity * GrowIndex);
        }

        /// <summary>
        ///     Quicksort method for different situations.
        ///     T(N) = O(N log N), where N - numbers of elements.
        /// </summary>
        /// <param name="array">Sourse array.</param>
        /// <param name="firstIndex">First index.</param>
        /// <param name="lastIndex">Last index.</param>
        /// <param name="funcOne">Function (delegate) for the first comparison.</param>
        /// <param name="funcTwo">Function (delegate) for the second comparison.</param>
        private void QuiqSortForStudents(Student[] array, int firstIndex, int lastIndex, Func<Student, Student, bool> funcOne, Func<Student, Student, bool> funcTwo)
        {
            int left = firstIndex;
            int right = lastIndex;
            Student pivotStudent = array[(firstIndex + lastIndex) / 2];

            while (left < right)
            {
                while (funcOne(array[left], pivotStudent))
                {
                    ++left;
                }

                while (funcTwo(array[right], pivotStudent))
                {
                    --right;
                }

                if (left <= right)
                {
                    Student temp = array[left];
                    array[left] = array[right];
                    array[right] = temp;

                    ++left;
                    --right;
                }
                else { }
            }

            if (left < lastIndex)
            {
                QuiqSortForStudents(array, left, lastIndex, funcOne, funcTwo);
            }
            else { }

            if (right > firstIndex)
            {
                QuiqSortForStudents(array, firstIndex, right, funcOne, funcTwo);
            }
            else { }
        }
    }
}
