using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    class Ebtvau
    {
        Object lekxem = null;
        Object parent = null;
        Object left = null;
        Object right = null;
        public Ebtvau(Object l)
        {
            lekxem = l;
        }

        public Object getParent()
        {
            return parent;
        }
        public void setParent(Object leksem)
        {
                this.parent = leksem;
        }
        public Object getRight()
        {
            return right;
        }
        public void setRight(Object leksem)
        {
                this.right = leksem;
        }
        public Object getLeft()
        {
            return left;
        }
        public void setLeft(Object leksem)
        {
                this.left = leksem;
        }
        public Object getLeksem()
        {
            return lekxem;
        }
    }
}
