using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    class Student
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int studentNumber, string firstName, string lastName)
        {
            this.StudentNumber = studentNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
