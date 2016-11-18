using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    class LeksemAnalis
    {
        List<Leksem> listLecsem;
        int countLine;
        public void analis() { 
        FileRead fileRead = new FileRead();
        FileSave fileSave = new FileSave();

        String[] fileInput = fileRead.readFile("input.txt");
        countLine = fileInput.Length;
        NumberSearch numberSearch = new NumberSearch();
        numberSearch.setFile(fileInput);
        StringSearch stringSearch = new StringSearch();
        stringSearch.setFile(fileInput);
        WalsSearch walsSearch = new WalsSearch();
        SearchCommander searchCommander = new SearchCommander();
        List<Leksem> list = new List<Leksem>();
        List<Leksem> listString = new List<Leksem>();
        List<Leksem> listNumber = new List<Leksem>();
        List<Leksem> listWals = new List<Leksem>();
        List<Leksem> listCommanders = new List<Leksem>();

        list=stringSearch.Search();
            fileSave.fileClear();
            fileInput = stringSearch.getFile();
            do{
                if (numberSearch.end|| numberSearch.getNumber()==null){
                    break;
                }
                list.Add(new Leksem(numberSearch.getNumer(),"number",new Position { begin = numberSearch.getPoint().pos - numberSearch.getNumer().Length, end = numberSearch.getPoint().pos,line= numberSearch.getPoint().line
            }));
            } while (1!=2);
            fileInput = numberSearch.getFile();
            walsSearch.setFile(fileInput);
            list.AddRange(walsSearch.Search());
            searchCommander.setFile(fileInput);
            list.AddRange (searchCommander.Search());
            list=sort(list);
            foreach (Leksem l in list)
            {
                fileSave.setFile(l.toString());
                fileSave.save();
            }
            listLecsem = list;
           Console.ReadKey();
        }

       public List<Leksem> sort(List<Leksem> first)
        {
            Leksem empty = new Leksem();
            for (int i = 0; i < first.Count; i++)
            {
                empty = first[i];
                for (int j = 0; j < first.Count; j++)
                {
                    if (empty.position.line < first[j].position.line)
                    {
                        first[i] = first[j];
                        first[j] = empty;
                        empty = first[i];
                    }
                }
            }
            List<Leksem> sortFirst = new List<Leksem>();
            List<Leksem> list;
            for (int line = 0; line < countLine; line++)
            {
                list = new List<Leksem>();
                foreach (Leksem l in first)
                {
                    if (line == l.position.line)
                    {
                        list.Add(l);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    empty = list[i];
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (empty.position.begin < list[j].position.begin)
                        {
                            list[i] = list[j];
                            list[j] = empty;
                            empty = list[i];
                        }
                    }

                }
                sortFirst.AddRange(list);
            }
            return sortFirst;
        }
        public  List<Leksem> getListLecsem()
        {
            return listLecsem;
        }
    }
}
