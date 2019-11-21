using System.Collections.Generic;
using Weihnachten.Members;

namespace Weihnachten.Shuffle
{
    interface IShuffle
    {
        public Dictionary<Student, Student> Shuffle(Student[] students);
    }
}
