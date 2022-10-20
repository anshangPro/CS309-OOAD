using System.Collections.Generic;

namespace Units
{
    public class Friendly : Unit
    {
        public LinkedList<Item> Package = new LinkedList<Item>();
        public LinkedList<Skill> Skills = new LinkedList<Skill>();
    }
}
