using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    class Mark
    {
        public int StudentNumber { get; set; }
        public string Lesson { get; set; }
        public float Score { get; set; }

        public Mark(int studentNumber, string lesson, float score)
        {
            this.StudentNumber = studentNumber;
            this.Lesson = lesson;
            this.Score = score;
        }
    }
}
