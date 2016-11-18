using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    class WalsSearch
    {
        private string[] file;
        private string[] filePunct;
        StringBuilder stringBuilder = new StringBuilder();
        List<Leksem> list = new List<Leksem>();
        List<string> charList = new List<string>();
        FileRead fileRead = new FileRead();
        public WalsSearch()
        {
            filePunct=fileRead.readFile("punct.txt");
            foreach(string str in filePunct)
            {
                charList.Add(str);
            }
        }
       public string[]  getFile()
        {
            return file;
        }
        public void setFile(string [] file)
        {
            this.file = file;
        }
        string s;
        string c;
        
        public List<Leksem>Search()
        {
           foreach (string str in charList)
            {
                for (int i = 0; i < file.Length; i++)
                {
                    for (int j = 0; j < file[i].Length; j++)
                    {
                        if (j + 1 < file[i].Length)
                        {
                            s = file[i][j].ToString() + file[i][j + 1].ToString();
                            //Console.WriteLine(s);
                            if (s == str)
                            {
                                list.Add(new Leksem(s, "punctual", new Position { begin = j, end = j+1, line = i }));
                                file[i] = file[i].Remove(j, 2);
                                file[i] = file[i].Insert(j,"  ");
                            }
                        }
                    }
                }
            }
            foreach (string str in charList)
            {
                for (int i = 0; i < file.Length; i++)
                {
                    for (int j = 0; j < file[i].Length; j++)
                    {   
                            c = file[i][j].ToString();
                            if (c == str)
                            {
                                list.Add(new Leksem(file[i][j].ToString(), "punctual", new Position { begin = j, end = j, line = i }));
                                file[i] = file[i].Remove(j, 1);
                                file[i] = file[i].Insert(j, " ");
                            }                        
                    }
                }
            }
            
            return list;
        }
    }
}
