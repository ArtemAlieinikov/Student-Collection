using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Collection.Classes
{
    class IncorrectIdException : Exception
    {
        public IncorrectIdException(string message)
            : base(message)
        { }
    }
}
