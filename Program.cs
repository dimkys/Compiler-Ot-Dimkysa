using Kampiler.sintax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    class Program
    {
        static void Main(string[] args)
        {
            LeksemAnalis analis = new LeksemAnalis();

            analis.analis();
            List<Leksem> list=analis.getListLecsem();
            // int i = 0;
            //Position p = new Position();
            // Console.WriteLine(p.GetType().Name);
            // typeof
            SintaxAnalis sintax = new SintaxAnalis(list);

            Console.ReadKey();
        }
    }
}
