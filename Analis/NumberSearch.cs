using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    class NumberSearch
    {
        private string Number;
        public string getNumer()
        {
            return Number;
        }
        public void setFile(string[] file)
        {
            this.file = file;
        }
        public string[] getFile()
        {
            return file;
        }
        public bool end { get; set; }
        public struct Point
        {
            
            public Point(int line,int pos)
            {
                this.line = line;
                this.pos = pos;
            }
            public int line;
            public int pos;
        }
        String [] file;
        Point point = new Point(0,0);

        public Point getPoint()
        {
            return point;
        }
        
        public string getNumber()
        {
            bool ext = false;
            Number="";
            for (int i = point.line;i< file.Length;i++)
            {               
                for (int j = point.pos; j < file[i].Length; j++)
                {
                    // Console.WriteLine(str[i][j]);      
                    if ('0' <= file[i][j] && '9' >= file[i][j] || file[i][j] == '.')
                    {
                        Number += file[i][j];
                        if (Number == ".")
                        {
                            return null;
                        }
                        
                        file[i] = file[i].Remove(j, 1);
                        file[i] = file[i].Insert(j, " ");
                        if ((j+1<file[i].Length) && file[i][j+1] >= 'A' && file[i][j+1]<= 'z')
                        {
                            throw new System.InvalidOperationException("Неправильный чифра:"+ (Number +file[i][j+1]) + "  Line:"+i+"  Position:"+j);
                        }

                    }
                    else {
                        
                        if (Number != "")
                        {

                            point.pos = j;
                            point.line = i;
                            ext = true;
                            break;
                        }
                    }
                }
                if (ext)
                    break;else
                point.pos = 0;


                if (file.Length-1== i)
                {
                    end = true;
                }
                else {
                    end = false;
                     }
                


            }
            if (Number.Equals(""))
            {
                return null;
            }
            else {               
                return Number;
            }
        }
    }
}
