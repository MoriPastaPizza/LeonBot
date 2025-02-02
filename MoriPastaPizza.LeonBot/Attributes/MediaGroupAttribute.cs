using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriPastaPizza.LeonBot.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class MediaGroupAttribute : Attribute
    {
        public string Group;
        public int Index;

        public MediaGroupAttribute(string group, int index = 0)
        {
            Group = group;
            Index = index;
        }
    }
}
