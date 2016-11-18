using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    class SearchCommander
    {
        private string[] file;
        private string[] commanreds;
        private string[] dataType;
        List<Leksem> list = new List<Leksem>();

        public SearchCommander()
        {
            FileRead fileRead = new FileRead();
            commanreds = fileRead.readFile("commander.txt");            
            dataType = fileRead.readFile("dataType.txt");

        }


        
        public void setFile(string[] file)
        {
            this.file = file;
        }
        public string[] getFile()
        {
            return file;
        }
        
        public List<Leksem> Search()
        {
            for (int i = 0; i < file.Length; i++)
            {
                
                file[i] = file[i].Replace('\t',' ');
            }
                string s="";
            for (int i = 0; i < file.Length; i++)
            {
                file[i] += " ";
                s = "";
                for (int j = 0; j < file[i].Length; j++)
                {
                    if(file[i][j]!=' ')
                    {
                        s += file[i][j];
                    }else if(s!="")
                    {
                        list.Add(new Leksem(s,"", new Position() { line = i, end = j, begin = j - s.Length }));
                        s = "";
                    }
                }                
            }
            for (int i = 0; i < list.Count; i++)
            {
                foreach(string str in commanreds)
                    if (list[i].getName() == str)
                    {
                        list[i].setType("control");
                        break;
                    }
                foreach (string str in dataType)
                    if (list[i].getName() == str)
                    {
                        list[i].setType("DataType");
                        break;
                    }

                if (list[i].getType() == "")
                {
                    list[i].setType("scalar");
                }
            }
                return list;
        }
    }
}