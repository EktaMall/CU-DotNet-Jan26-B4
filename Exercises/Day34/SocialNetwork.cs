using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Week6
{
    class Person
    {
        public string Name { get; set; }
        public List<Person> Friends = new List<Person>();
        public Person(string name) => Name = name;
        //public void AddFriend(Person friend) {
        //    if (!Friends.Contains(friend)){
        //        Friends.Add(friend);
        //        friend.Friends.Add(this);
        //    }
        //}
    }

    class SocialNetwork
    {
        private List<Person> members = new List<Person>();
        public void AddMember(Person member) {
            members.Add(member);
        }

        public void AddFriend(Person friend1, Person friend2)
        {
            if (!(members.Contains(friend1) && members.Contains(friend2)))
            {
                Console.WriteLine($"Friends {friend1.Name} {friend2.Name} are not on social platform");
            }
            else
            {
                if (!friend1.Friends.Contains(friend2))
                {
                    friend1.Friends.Add(friend2);
                    friend2.Friends.Add(friend1);
                }
            }
        }
        public void ShowNetwork()
        {
            foreach (Person member in members)
            {
                Console.Write(member.Name + " -> ");
                List<string> friends = new List<string>();
                foreach(var friend in member.Friends)
                {
                    friends.Add(friend.Name);
                }
                Console.WriteLine($"{string.Join(", ", friends)}");
            }
        }
    }
    internal class Practise
    {
        static void Main(string[] args)
        {
            SocialNetwork socialNetwork = new SocialNetwork();

            
            Person aman = new Person("Aman");
            Person ekta = new Person("Ekta");
            Person rishi = new Person("Rishi");
            Person aniketh = new Person("Aniketh");
            Person hrishikesh = new Person("hrishikesh");

            socialNetwork.AddMember(aman);
            socialNetwork.AddMember(ekta);
            socialNetwork.AddMember(rishi);
            socialNetwork.AddMember(aniketh);

            socialNetwork.AddFriend(aman, aniketh);
            socialNetwork.AddFriend(aman, rishi);
            socialNetwork.AddFriend(rishi, ekta);
            socialNetwork.AddFriend(ekta, aniketh);
            socialNetwork.AddFriend(aniketh, hrishikesh);
            socialNetwork.AddFriend(aniketh, rishi);
            socialNetwork.AddFriend(ekta, ekta);


            //aman.AddFriend(aniketh);
            //aman.AddFriend(rishi);
            //rishi.AddFriend(ekta);
            //ekta.AddFriend(aniketh);

            socialNetwork.ShowNetwork();
        }
    }
}
