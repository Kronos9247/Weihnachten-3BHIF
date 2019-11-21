using System;
using System.Collections.Generic;
using AdLibrary.Api;

namespace Weihnachten.Members
{
    public class AdMembers : IClassMembers
    {
        public string Class { get; }

        public AdMembers(string classname)
        {
            this.Class = classname;
        }

        public AdMembers()
            : this("3BHIF")
        { }

        private Student[] GetStudents()
        {
            string username = ReadLine.Read("Username: ");
            string password = ReadLine.ReadPassword("Passwort: ");
            
            using (AdSearcher searcher = new AdSearcher())
            {
                try
                {
                    if (searcher.Authenticate(username, password))
                    {
                        List<AdEntry> pupils = searcher.FindGroupMembers(Class);
                        List<Student> students = new List<Student>();

                        foreach (AdEntry pupil in pupils)
                        {
                            string[] names = pupil.Displayname.Split(" ");
                            string surname = names[0];
                            string name = names[1];

                            students.Add(new Student(name, surname, pupil.Email));
                        }

                        return students.ToArray();
                    }
                    else
                    {
                        Console.WriteLine("LDAP login failed!");
                    }
                }
                catch (AdException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return null;
        }

        public Student[] Members
        {
            get
            {
                return GetStudents();
            }
        }
    }
}
