using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    public class Leksem
    {

        private string name;
        private string type;
        public Position position;

        public Leksem(string name,string type,Position position)
        {
            this.name = name;
            this.type = type;
            this.position = position;
        }
        public Leksem()
        {
            this.position = new Position();
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public string getName( )
        {
            return name;
        }
        public string getType()
        {
            return type;
        }
        public void setType(string type)
        {
            this.type = type;
        }
        public void setPosition(Position p)
        {
            position = p;
        }
        public Position getPosition()
        {
            return position;
        }

        public string toString()
        {
            return "\tV:"+ name + " \tT:" + type + " \tB:"+ position.begin+ "\t E:" + position.end+ " \tL:" + position.line+"\n";
        }
    }
}
