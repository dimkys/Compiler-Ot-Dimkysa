using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    class Learn
    {
       private Leksem midle=null;
        private List<Learn> list = new List<Learn>();
        public Learn(Leksem leksem)
        {
            this.midle = leksem;
        }

       /* void ProcessItems<T>(IList<T> coll)
        {

        }*/
        Learn getListElem()
        {
            return this;
        }

        public Leksem getMidle()
        {
            return midle;
        }
        public void setMidle(Leksem leksem)
        {
            this.midle = leksem;
        }
        public List<Learn> getList()
        {
            return list;
        }
        public void addListLeksem (Learn leksem)
        {
            list.Add(leksem);
        }

        public void setList(List<Learn> leksem)
        {
            this.list = leksem;
        }
    }
}
