using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    class TreeWriter
    {

        string[,] s=new string[40, 40];
        
        public TreeWriter(List<Object> obj)
        {
            Object o =obj.First();
            
            // foreach (Object item in obj)
            //{
            go(o,"midle",0,10);
            string ss = "";
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (s[i, j] == null)
                    {
                        ss += " " + s[i, j];
                    }
                    else
                    {
                        if (s[i, j] == "+")
                        {
                            int a = 1;
                        }
                        ss +=s[i, j];
                    }

                }
                ss += "\n";
            }
            new FileSave(ss, "Tree");
           // }
        }

        public void go(Object obj,string otkud, int hight, int line)
        {
            if (obj.GetType().Name == "Ebtvau")
            {
                Object left = ((Ebtvau)obj).getLeft();
                Object leksem = ((Ebtvau)obj).getLeksem();
                Object right = ((Ebtvau)obj).getRight();


                if (left != null)
                {
                    if (left.GetType().Name == "Ebtvau")
                    {
                        go((Ebtvau)left, "left", hight + 1, line - 1);
                    }
                    else
                    {
                        string ss = ((Leksem)left).getName();
                        Console.WriteLine(ss);
                    }
                }

                if (leksem != null)
                {
                    switch (leksem.GetType().Name)
                    {
                        case "Leksem":
                            {
                                string ss = ((Leksem)leksem).getName();
                                Console.WriteLine(ss);
                                break;
                            }
                        case "Ebtvau":
                            {
                                go((Ebtvau)leksem, "midle", hight + 1, line);
                                break;
                            }
                    }
                }

                if (right != null)
                {
                    if (right.GetType().Name == "Ebtvau")
                    {
                        go((Ebtvau)right, "right", hight + 1, line + 1);
                    }else
                    {
                        string ss = ((Leksem)right).getName();
                        Console.WriteLine(ss);
                    }
                }
            }
            else
            {
                string ss = ((Leksem)obj).getName();
                Console.WriteLine(ss);
            }
        }
    }
}
