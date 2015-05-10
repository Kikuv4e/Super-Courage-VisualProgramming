using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualProgrammingProject
{
    public class Person : IComparable
    {
        public string Nickname { get; set; }
        public int Points { get; set; }
        public Person(string Nickname)
        {
            this.Nickname = Nickname;
            Points = 0;
        }
        public override string ToString()
        {
            return String.Format("Nickname : {0}, Points : {1}", Nickname, Points);
        }

        public  int CompareTo(object obj)
        {
            Person p = obj as Person;
            if (this.Points < p.Points) return 1;
            else if (this.Points > p.Points) return -1;
            return 0;
        }
    }
}
