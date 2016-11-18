using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler.sintax
{
    class SintaxAnalis
    {
        List<Leksem> listLex;
        List<Leksem> oldlistLex;
        int iter = 0;
        //Ebtvau oldEbt;
        MapScalar map = new MapScalar();

        List<Object> tree;
        int lexemNumber=0;
        int oldLexNum = 0;
        int BracesPos = 0;
        bool forBol = false;
        Ebtvau begin = new Ebtvau(null);

        public SintaxAnalis(List<Leksem> listLex){
            this.listLex=listLex;
            tree =body(listLex);
            new TreeWriter(tree);
        }

        List<Object> body(List<Leksem> leksems)
        {
            iter++;
            List<Leksem> oldlist = leksems;
            listLex = leksems;
            
            List<Object> obj = new List<object>();
            Leksem lex = nextLexsem();

            while (listLex.Count > lexemNumber + 1)
            {

                switch (lex.getType())
                {
                    case "DataType":
                        {
                            //obj.Add(((List<Object>)DataType(lex)).First());

                            Ebtvau ebt =new Ebtvau( ((List<Object>)DataType(lex)).First());
                            ebt.setParent(begin);
                            begin.setRight(ebt);
                            begin = ebt;
                            break;
                        }
                    case "scalar":
                        {
                            //obj.Add(((List<Object>)scalar(lex)).First());
                            Ebtvau ebt = new Ebtvau(((List<Object>)scalar(lex)).First());
                            if (ebt != null)
                            {
                                ebt.setParent(begin);
                                begin.setRight(ebt);
                                begin = ebt;
                            }
                            break;
                        }
                    case "control":
                        {
                            switch (lex.getName())
                            {
                                case "if":
                                    {
                                        Ebtvau ebt = new Ebtvau(lex);
                                        Leksem l = nextLexsem();
                                        if (l.getType() == "punctual" && l.getName() == "(")
                                        {
                                            Ebtvau ifLeft = (Ebtvau)compare(getParentheses()).First();
                                            ifLeft.setParent(ebt);
                                            ebt.setLeft(ifLeft);
                                            listLex = oldlist;
                                            lexemNumber = oldLexNum;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Где круглые скобы?");
                                            break;
                                        }
                                        l = nextLexsem();
                                        if (l.getType() == "punctual" && l.getName() == "{")
                                        {
                                            Ebtvau oldEbt = begin;
                                            begin= new Ebtvau(null);
                                            int pos = 0;
                                            Ebtvau ifBody = (Ebtvau) body(getBraces(ref pos)).First();
                                            begin = oldEbt;
                                            ifBody.setParent(ebt);
                                            ebt.setRight(ifBody);
                                            listLex = oldlist;
                                            lexemNumber = pos;                                           
                                            
                                            Ebtvau newEbtvau = new Ebtvau(ebt);
                                            newEbtvau.setParent(begin);
                                            begin.setRight(newEbtvau);
                                            begin = newEbtvau;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Где фигурные скобы?");
                                            break;
                                        }

                                        break;

                                    }

                                case "for":
                                    {
                                        Ebtvau ebt = new Ebtvau(lex);
                                        Leksem l = nextLexsem();
                                        if (l.getType() == "punctual" && l.getName() == "(")
                                        {
                                            List<Object> ls = getParentheses().ConvertAll<Object>(converte);
                                            listLex = oldlist;
                                            lexemNumber = oldLexNum;

                                            int f1=0, f2=0;
                                            for (int i = 0; i < ls.Count; i++)
                                            {
                                                if (((Leksem)ls.ElementAt(i)).getName() == ";")
                                                    if (f1 <= 0)
                                                    {
                                                        f1 = i;
                                                    }
                                                    else {
                                                        if (f2 <= 0)
                                                        {
                                                            f2 = i;
                                                        }
                                                    }
                                            }
                                            List<Object> for1 = ls.GetRange(0, f1 + 1);
                                            List<Object> for2 = ls.GetRange(f1 + 1, f1 - 1);
                                            List<Object> for3 = ls.GetRange(f2 + 1, (ls.Count - (f2 + 1)));

                                            oldLexNum = lexemNumber;
                                            oldlist = listLex; 
                                            lexemNumber = 0;
                                            Ebtvau oldEbt = begin;
                                            begin = new Ebtvau(null);
                                            Object forBody = body(for1.ConvertAll<Leksem>(converte)).First();
                                            begin = oldEbt;

                                            ebt.setLeft(forBody);
                                            
                                            listLex = oldlist;
                                            lexemNumber = oldLexNum;

                                            l = nextLexsem();
                                            if (l.getType() == "punctual" && l.getName() == "{")
                                            {
                                                oldEbt = begin;
                                                begin = new Ebtvau(null);
                                                int pos = 0;
                                                oldLexNum = lexemNumber;
                                                oldlist = listLex;
                                                Ebtvau bodu= (Ebtvau)((Ebtvau)body(getBraces(ref pos)).First()).getRight();
                                                begin = oldEbt;
                                                listLex = oldlist;
                                                lexemNumber = oldLexNum;


                                                bodu.setLeft(compare(for2.ConvertAll<Leksem>(converte)).First());

                                                oldLexNum = lexemNumber;
                                                oldlist = listLex;
                                                lexemNumber = 0;
                                                listLex = for3.ConvertAll<Leksem>(converte);
                                                forBol = true;
                                                bodu.setRight(scalar(nextLexsem()).First());

                                                lexemNumber = oldLexNum;
                                                listLex= oldlist;
                                                ebt.setRight(bodu);

                                                ebt.setParent(begin);
                                                Ebtvau newEbtvau = new Ebtvau(ebt);
                                                begin.setRight(newEbtvau);
                                                begin = newEbtvau;

                                                List<Object> a = new List<object>();
                                                a.Add(begin);
                                                Console.WriteLine("========================================");
                                                new TreeWriter(a);
                                                Console.WriteLine("========================================");

                                            }
                                            else
                                            {
                                                Console.WriteLine("Где фигурные скобы?");
                                                break;
                                            }                                         
                                        }
                                        else
                                        {
                                            Console.WriteLine("Где круглые скобы?");
                                            break;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                        
                }
                lex = nextLexsem();
            }
            List<Object> lis = new List<object>();
            Object o = begin;
            while (((Ebtvau)o).getParent() != null)
            {
                o = ((Ebtvau)o).getParent();
            }

            lis.Add((Ebtvau)o);
            iter--;
            return lis;
        }
        List<Leksem> getParentheses()
        {
            List<Leksem> obj = new List<Leksem>();
            Leksem l = nextLexsem();
            int i = 0;
            while (l.getName()!=")"||i!=0)
            {
                if(l.getName() == "(")
                {
                    i++;
                }
                else
                {
                    if(l.getName() == ")")
                    {
                        i--;
                    }
                    else
                    {
                        obj.Add(l);
                    }                    
                }
                l = nextLexsem();
            }
            oldlistLex = listLex;
            oldLexNum = lexemNumber;
            lexemNumber = 0;
             return obj;
        }

        List<Leksem>  squareBrackets()
        {
            return null;
        }
        List<Leksem> getBraces(ref int pos)
        {
            List<Leksem> obj = new List<Leksem>();
            Leksem l = nextLexsem();
            int i = 0;
            while (l.getName() != "}" || i != 0)
            {
                if (l.getName() == "{")
                {
                    i++;
                    obj.Add(l);
                }
                else
                {
                    if (l.getName() == "}")
                    {
                        i--;
                        obj.Add(l);
                    }
                    else
                    {
                        obj.Add(l);
                    }
                }
                l = nextLexsem();
            }
            oldlistLex = listLex;
            oldLexNum = lexemNumber;
            pos = lexemNumber;
            lexemNumber = 0;
            
            return obj;
        }


        Leksem nextLexsem(){
            if (lexemNumber<listLex.Count)
            {
                return listLex.ElementAt(lexemNumber++);

            }
            else
            {
                return null;
            }
        }
        Leksem previousLexsem()
        {
            if (lexemNumber < listLex.Count)
            {
                return listLex.ElementAt(--lexemNumber);

            }
            else
            {
                return null;
            }
        }
        Leksem сurrentLexsem()
        {
            return listLex.ElementAt(lexemNumber);
        }
        List<Object> compare(List<Leksem> list)
        {
            List<Object> obj = list.ConvertAll<Object>(converte);
            while (obj.Count > 1)
            {
                for (int i = 0; i < obj.Count; i++)
                {
                    string name = obj[i].GetType().Name;
                    if (name == "Leksem")
                    {
                        Leksem leksem = (Leksem)obj[i];
                        if (leksem.getType() == "punctual")
                        {
                            switch (leksem.getName())
                            {
                                case "&&":
                                    {
                                        Ebtvau eb = new Ebtvau(leksem);
                                        eb.setLeft(compare(obj.GetRange(0, i).ConvertAll<Leksem>(converte)).First());
                                        eb.setRight(compare(obj.GetRange(i+1, obj.Count-(i+1)).ConvertAll<Leksem>(converte)).First());
                                        obj.RemoveRange(0, obj.Count);
                                        obj.Insert(0,eb);
                                        break;
                                    }
                                case "||":
                                    {
                                        Ebtvau eb = new Ebtvau(leksem);
                                        eb.setLeft(compare(obj.GetRange(0, i).ConvertAll<Leksem>(converte)));
                                        eb.setRight(compare(obj.GetRange(i + 1, obj.Count - (i + 1)).ConvertAll<Leksem>(converte)));
                                        obj.RemoveRange(0, obj.Count);
                                        obj.Insert(0, eb);
                                        break;
                                    }
                            }
                        }
                    }
                }
                for (int i = 0; i < obj.Count; i++)
                {
                    string name = obj[i].GetType().Name;
                    if (name == "Leksem")
                    {
                        Leksem leksem = (Leksem)obj[i];
                        if (leksem.getType() == "punctual")
                        {
                            switch (leksem.getName())
                            {
                                case ">":
                                    {
                                        obj = reObj(obj, i);
                                        break;
                                    }
                                case "<":
                                    {
                                        obj = reObj(obj, i);
                                        break;
                                    }
                                case "<=":
                                    {
                                        obj = reObj(obj, i);
                                        break;
                                    }
                                case ">=":
                                    {
                                        obj = reObj(obj, i);
                                        break;
                                    }
                                case "==":
                                    {
                                        obj = reObj(obj, i);
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            return obj;
        }

        List<Object> scalar(Leksem lexem){
            Leksem l = nextLexsem();
            Ebtvau obj = new Ebtvau(l);
            obj.setRight(lexem);
            
            if (l.getName() == "=")
            {
                l = nextLexsem();
                List<Leksem> ls = new List<Leksem>();
                while (l.getName() != ";")
                {
                    if (listLex.Count> lexemNumber)
                    {                       
                        ls.Add(l);
                    }else
                    {
                        if (forBol)
                        {
                            ls.Add(l);
                            break;
                        }
                        else
                        {
                            if (l == null)
                            {
                                Console.WriteLine("Нет ;");
                                return null;
                            }
                        }
                    }
                    
                    
                    l = nextLexsem();
                }
                if (ls.Count > 0)
                {
                    obj.setLeft(expression(ls).First());
                }
                else
                {
                    Console.WriteLine("Нет выражения");
                    return null;
                }


            }
            else
            {
                if(l.getName() == "++"|| l.getName() == "--")
                {
                    Ebtvau eb = new Ebtvau(l);

                    if (previousLexsem().getType()=="scalar")
                    {
                        eb.setLeft(сurrentLexsem());
                    }
                    else
                    {
                       // eb.setRight(сurrentLexsem);
                    }
                }
                else
                {
                   // return null;
                }
               
            }
            List<Object> list = new List<Object>();
            list.Add(obj);
            return list;
        }

        List<Object> DataType(Leksem lexem){
            //Learn learn = new Learn(lexem);
            Leksem l = lexem;
            Ebtvau obj = null;
            obj = new Ebtvau(l);
            l = nextLexsem();
            if (l.getType() == "scalar")
            {
                obj.setRight(l);
                l = nextLexsem();
                if (l.getType() == "punctual")
                {
                    if (l.getName() == "=")
                    {
                        obj.setLeft(new Ebtvau(l));
                        l = nextLexsem();

                        List<Leksem> ls = new List<Leksem>();
                        while (l.getName() != ";")
                        {
                            ls.Add(l);
                            l = nextLexsem();
                            if (l==null)
                            {
                                return null;
                            }
                           
                        }
                        if (ls.Count > 0)
                        {
                            Object a = expression(ls).First();
                            Object lef = obj.getLeft();
                            ((Ebtvau)lef).setLeft(a);
                        }

                    }
                    else
                    {
                        if (l.getName() == ";")
                        {
                            List<Object> a = new List<Object>();
                            a.Add(obj);
                            return a;
                        }
                        else
                        {
                            Console.WriteLine("Ожидается ';'");
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            List<Object> list = new List<Object>();
            list.Add(obj);
            return list;
        }
        List<Object> reObj(List<Object> obj,int i){
            string name = "";

            Ebtvau ebt = new Ebtvau((Leksem)obj[i]);

            name = obj[i - 1].GetType().Name;
            if (name != "Ebtvau")
            {
                ebt.setLeft(obj[i - 1]);
            }
            else
            {
                ebt.setLeft(obj[i - 1]);
                Ebtvau e=(Ebtvau)obj[i - 1];
                e.setParent(ebt);                
            }

            name = obj[i + 1].GetType().Name;
            if (name != "Ebtvau")
            {
                ebt.setRight(obj[i + 1]);
            }
            else
            {
                ebt.setRight(obj[i + 1]);
                Ebtvau e = (Ebtvau)obj[i + 1];
                e.setParent(ebt);
            }
            obj.RemoveRange(i - 1, 3);
            obj.Insert(i - 1, ebt);
            return obj;
        }


        Object converte(Leksem o1)
        {
            return o1;
        }
        Leksem converte(Object o1)
        {
            return (Leksem)o1;
        }

        List<Object> expression(List<Leksem> list){
            List<Object> obj = list.ConvertAll<Object>(converte);
            while (obj.Count > 1)
            {
                for (int i = 0; i < obj.Count; i++)
                {
                    string name = obj[i].GetType().Name;
                    if (name == "Leksem")
                    {
                        Leksem leksem = (Leksem)obj[i];
                       if (leksem.getType()== "punctual")
                        {
                            switch (leksem.getName())
                            {
                                case "*":
                                    {
                                        obj = reObj(obj, i);
                                        break;
                                    }
                                case "/":
                                    {
                                        obj = reObj(obj, i);
                                        break;
                                    }
                            }
                        }
                    }
                }
                for (int i = 0; i < obj.Count; i++){
                    string name = obj[i].GetType().Name;
                    if (name == "Leksem"){
                        Leksem leksem = (Leksem)obj[i];
                        if (leksem.getType() == "punctual"){
                            switch (leksem.getName()){
                                case "+":
                                    {
                                        obj = reObj(obj, i);
                                        break;
                                    }
                                case "-":
                                    {
                                        obj = reObj(obj, i);
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            return obj;
        }

    }
}