using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    public class FileSave
    {
        private String file;
        /*public void setFile(string [] str)
        {
                file=str;
            
        }*/
        public FileSave()
        {

        }
        public FileSave(String s, String name)
        {
            File.Delete(name+".txt");
           
            File.AppendAllText(name + ".txt",s );
            Console.WriteLine("Save: OK.");
        }
        public void setFile(string str)
        {
            file = str;

        }
        public  String  getFile()
        {
            return file;
        }
        public void save()
        {
            try {
                File.AppendAllText("output.txt", file);
                Console.WriteLine("Save: OK.");
            }
            catch {
            }
        }
        public void fileClear()
        {
            try
            {
                File.Delete("output.txt");
                Console.WriteLine("Delete: OK.");
            }
            catch
            {
                
            }
        }
    }
}
