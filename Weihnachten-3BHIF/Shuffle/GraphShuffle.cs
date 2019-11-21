using System;
using System.Collections.Generic;

using Weihnachten.Members;

namespace Weihnachten.Shuffle
{
    public class GraphShuffle : IShuffle
    {
        public int Rounds { get; set; } = 5;

        public Dictionary<Student, Student> Shuffle(Student[] students)
        {
            int ops = Math.Max(Rounds, 1);

            Shuffler shuffler = new Shuffler(students);
            for (int r = 0; r < ops; r++)
            {
                shuffler.Shuffle();
            }

            // check for self-feeds inside of the graph
            while (!shuffler.Intact())
            {
                shuffler.Shuffle();

                ops++;
            }

            Console.WriteLine($"Shuffle operations {ops}");
            return shuffler.ToDict();
        }

        internal class Shuffler
        {
            private List<KeyValuePair<Student, Student>> students;

            public Shuffler(Student[] students)
            {
                this.students = new List<KeyValuePair<Student, Student>>();

                foreach (Student student in students)
                {
                    KeyValuePair<Student, Student> kv = 
                        new KeyValuePair<Student, Student>(student, student);

                    this.students.Add(kv);
                }
            }

            private void NewValue(int i, Student value)
            {
                students[i] = new KeyValuePair<Student, Student>(students[i].Key, value);
            }

            private void Swap(int i, int j)
            {
                Student a = students[i].Value;
                // Student b = students[j].Value;

                NewValue(i, students[j].Value);
                NewValue(j, a);
            }

            public void Shuffle()
            {
                Random random = new Random();

                for (int i = 0; i < students.Count; i++)
                {
                    KeyValuePair<Student, Student> kv = students[i];


                    Student receiver = kv.Key;
                    Student target = kv.Value;

                    int j = -1;
                    while (j < 0 || i == j)
                    {
                        j = random.Next(0, students.Count);
                    }

                    Swap(i, j);
                }
            }

            public bool Intact()
            {
                return students.TrueForAll((kv) => kv.Key != kv.Value);
            }

            public Dictionary<Student, Student> ToDict()
            {
                Dictionary<Student, Student> dict = new Dictionary<Student, Student>();
                foreach (KeyValuePair<Student, Student> kv in students)
                {
                    dict.Add(kv.Key, kv.Value);
                }

                return dict;
            }
        }
    }
}
