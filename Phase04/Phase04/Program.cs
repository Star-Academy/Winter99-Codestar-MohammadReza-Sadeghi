using System;
using System.Collections.Generic;
using System.Linq;

namespace Phase04
{

    class Program
    {
        private static string StudentsJsonPath = "../../../Students.json";
        private static string ScoresJsonPath = "../../../Scores.json";
        static void Main(string[] args)
        {
            var students = Parser.jsonToList<Student>(FileReader.Read(StudentsJsonPath));
            var scores = Parser.jsonToList<Mark>(FileReader.Read(ScoresJsonPath));
            var firstThreeAverages = FindTop3Students(students, scores);
            Output.PrintList(firstThreeAverages);    
        }

        static IEnumerable<StudentAverage> FindTop3Students(List<Student> students, List<Mark> scores)
        {
            return students.GroupJoin(scores, st => st.StudentNumber, sc => sc.StudentNumber, (st, sc) => new StudentAverage (st.FirstName, st.LastName,sc.Average(s => s.Score))).OrderByDescending(s => s.Average).Take(3);
        }

    }
}
