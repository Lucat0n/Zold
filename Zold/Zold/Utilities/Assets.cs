using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Utilities
{
    public sealed class Assets
    {
        Dictionary<string, dynamic> AssetList;

        Assets()
        {
            AssetList = new Dictionary<string, dynamic>();
        }
        private static readonly object padlock = new object();
        private static Assets instance = null;
        public static Assets Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Assets();
                    }
                    return instance;
                }
            }
        }

        public void set(string name, dynamic value)
        {
            AssetList.Add(name, value);
        }

        public dynamic get(string name)
        {
            return AssetList[name];
        }
    }
}
