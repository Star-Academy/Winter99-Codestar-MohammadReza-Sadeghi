using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    class StudentAverage
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Average { get; set; }

        public StudentAverage(string FirstName, string LastName, float Average)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Average = Average;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + ": " + Average;
        }
    }
}
