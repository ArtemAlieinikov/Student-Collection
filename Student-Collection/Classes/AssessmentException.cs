using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Collection.Classes
{
    public class AssessmentException : Exception
    {
        public AssessmentException(string message) 
            : base (message)
        { }
    }
}
