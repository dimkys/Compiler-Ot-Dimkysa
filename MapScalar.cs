using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampiler
{
    class MapScalar
    {
        Dictionary<int, Dictionary<string, string>> maps = new Dictionary<int, Dictionary<string, string>>();
        
        public void add(int i,string key, string value)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add(key, value);
            maps.Add(i, map);
        }
        public Dictionary<int, Dictionary<string, string>> getMaps()
        {
            return maps;
        }

    }
}