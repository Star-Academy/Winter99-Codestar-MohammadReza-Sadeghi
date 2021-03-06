using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phase04
{
    class Program
    {
        static void Main(string[] args)
        {
            var studentJson = File.ReadAllText("../../../Students.json");
            var students = JsonConvert.DeserializeObject<List<Student>>(studentJson);

            var scoreJson = File.ReadAllText("../../../Scores.json");
            var scores = JsonConvert.DeserializeObject<List<Mark>>(scoreJson);

            var firstThreeStudents = students.Take(3);

            Console.WriteLine("Printing 3 first students based on their position in Students.json file...");
            foreach (Student student in firstThreeStudents)
                Console.WriteLine(student.FirstName + " " + student.LastName + " : " + scores.Where(s => s.StudentNumber == student.StudentNumber).Average(s => s.Score));
            Console.WriteLine();

            var firstThreeAverages = students.GroupJoin(scores, st => st.StudentNumber, sc => sc.StudentNumber, (st, sc) => new { FirstName = st.FirstName, LaseName = st.LastName, Average = sc.Average(s => s.Score)}).OrderByDescending(s => s.Average).Take(3);
            Console.WriteLine("Printing 3 first students based on their averages...");
            foreach (var student in firstThreeAverages)
                Console.WriteLine(student.FirstName + " " + student.LaseName + " : " + student.Average);
        }
    }
}
