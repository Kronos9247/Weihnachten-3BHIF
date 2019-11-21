using System;
using System.Collections.Generic;

using Weihnachten.Members;
using Weihnachten.EMail;
using Weihnachten.Shuffle;

namespace Weihnachten
{
    class Program
    {
        static void Main(string[] args)
        {
            // IClassMembers members = new AdMembers("3BHIF");
            IClassMembers members = new MockupMembers();
            Dictionary<Student, Student> pairs = new GraphShuffle()
                .Shuffle(members.Members);

            using (Delivery delivery = new Delivery() 
            {
                Email = "engerlbengerl.schule@gmail.com", 
                Name = "Engerl Bengerl",
                Subject = "Engerl Bengerl - 3BHIF!",

                Content = string.Join("<br>", new string[]
                {
                    "Dies ist eine automatisch generierte Email für {TO}.",
                    "Du hast <b>{0} {1}</b> im Engerl Bengerl gezogen!",
                    "",
                    "Mit freundlichen Grüßen,",
                    "Test"
                })
            })
            {
                delivery.Start();

                foreach (KeyValuePair<Student, Student> pair in pairs)
                {
                    Student receiver = pair.Key;
                    Student target = pair.Value;

                    delivery.SendTo(receiver, new string[] { target.Name, target.Surname });
                }
            }

            Console.WriteLine("Viel Spaß!");
        }
    }
}
