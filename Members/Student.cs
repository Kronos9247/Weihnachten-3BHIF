namespace Weihnachten.Members
{
    public class Student
    {
        public string Surname { get; }
        public string Name { get; }

        public Student(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }

        public Student(string name, string surname, string email)
            : this(name, surname)
        {
            this.Email = email;
        }

        public virtual string Email { get; }
    }
}
