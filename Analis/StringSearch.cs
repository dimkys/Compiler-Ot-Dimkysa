using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kampiler
{
    public class StringSearch
    {
       private string[] file;
        StringBuilder stringBuilder=new StringBuilder();
        List<Leksem> list = new List<Leksem>();       
        public void setFile(string [] file)
        {
            this.file = file;
        }
        public string[]getFile()
        {
            return file;
        }
      /*  public Leksem getDisplau()
        {
           // return new Leksem { string name, string type, Position position };
        }*/
        struct disp
        {
            public string s;
            public int p;
        }
        Regex reg = new Regex(@"(\\.)");
        private List<disp> regul(int i)
        {
            List<disp> displs = new List<disp>();
            disp d;
            Match m = reg.Match(file[i]);
            if (m.Index > 0)
            {
                d = new disp();
                d.p = m.Index+2* displs.Count;
                d.s = file[i].Substring(m.Index, 2);
                file[i] = file[i].Remove(m.Index,2);
                displs.Add(d);
                while ((m = reg.Match(file[i])).Index > 0)
                {
                    d = new disp();
                    d.p = m.Index + 2 * displs.Count;
                    d.s = file[i].Substring(m.Index, 2);
                    file[i] = file[i].Remove(m.Index, 2);
                    displs.Add(d);
                    
                }
            }
            return displs;
        }
        public List<Leksem> Search()
        {
            Leksem leksem;
            for (int i = 0; i < file.Length; i++)
            {

                int begin, end = 0;
                begin = file[i].IndexOf('"');
                if (file[i].Length > begin + 1 && 0< begin - 1)
                    if (file[i][begin - 1] != '\'' && file[i][begin + 1] != '\'')
                        if (file[i].Length > begin + 1 && 0 < begin - 2)
                            if (file[i][begin - 1] != '\\' && file[i][begin - 2] != '\'' && file[i][begin + 1] != '\'')
                            { 
                                List<disp> displs = new List<disp>();
                                while (begin >= 0)
                                {
                                    Console.WriteLine("\n\n\nSubstringDo:" + file[i]);
                                    displs = regul(i);
                                    end = file[i].IndexOf('"', begin + 1);
                                    if (end >= 0 && end != begin)
                                    {
                                        stringBuilder.Append(file[i].Substring(begin, end - begin + 1));
                                        
                                        Console.WriteLine("Substring:"+ stringBuilder);
                                        string s = "";
                                        for (int j = 0; j < end - begin + 1; j++)
                                        {
                                            s += " ";
                                        }
                                        file[i] = file[i].Remove(begin, end - begin + 1).Insert(begin, s);

                                        if (displs.Count > 0)
                                        {
                                            foreach (var item in displs)
                                            {
                                                if (item.p < end+2* displs.Count && item.p > begin)
                                                {
                                                    stringBuilder.Insert(item.p - begin, item.s);
                                                }
                                            }
                                            Console.WriteLine("Substring            :" + stringBuilder);

                                        }
                                        leksem = new Leksem();
                                        leksem.position.begin = begin;
                                        leksem.position.end = end;
                                        leksem.position.line = i;
                                        leksem.setName(stringBuilder.ToString());
                                        leksem.setType("string");
                                        list.Add(leksem);
                                        stringBuilder.Clear();
                                        begin = file[i].IndexOf('\"', end + 1);
                                    }
                                    else
                                    {
                                        if (begin < 0 && end >= 0 || (begin >= 0 && end < 0))
                                        {
                                            Console.WriteLine("Нет знака окончания строки  ");
                                            throw new System.InvalidOperationException("Строка не начата не закрыта, грязь " + "  Line:" + i + "  Position:" + begin);
                                        }
                                    }

                                }

                                displs.Clear();                         
                    }
            }
            return list;
        }

    }
}
