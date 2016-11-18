using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    public class FileRead
    {
        private String[]file;
        public String[] readFile(string patch)
        {
            file = File.ReadAllLines(patch);
            return file;
        }
        public FileRead()
        {

        }
        public FileRead(string patch)
        {
           file=readFile(patch);            
        }

    }
}
