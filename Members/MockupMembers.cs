namespace Weihnachten.Members
{
    public class MockupMembers : IClassMembers
    {
        public string Email { get; set; } = "xoniral415@xmail2.net";

        public Student[] Members => new Student[] { 
            new Student("John", "Doe", Email),
            new Student("Kevin", "Musterman", Email),
            new Student("Max", "Musterman", Email),
            new Student("Rafael", "Orman", Email)
        };
    }
}
