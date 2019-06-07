using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Buffs;
using Zold.Utilities;

namespace Zold.Inventory.Items
{
    /// <summary>
    /// Itemek dający buffa postaci
    /// </summary>
    class BuffItem : Item
    {
        private IBuff[] buffs;
        public BuffItem(string id, ItemManager itemManager, string type) : base(id, itemManager, type)
        {
            JObject itemsBase = itemManager.ItemsBase;
            List<IBuff> buffsList = new List<IBuff>();
            foreach (JToken buff in itemsBase["buffItems"][id]["buffs"])
            {
                if (0 == (int)buff["amount"] || (double)buff["amount"] != Math.Floor((double)buff["amount"]))
                    buffsList.Add(BuffFactory.CreateInstantBuff((float)buff["amount"], (byte)buff["targetStat"]));
                else
                    buffsList.Add(BuffFactory.CreateInstantBuff((int)buff["amount"], (byte)buff["targetStat"]));
            }
                
            buffs = buffsList.ToArray();
        }
        internal IBuff[] Buffs { get => buffs; set => buffs = value; }
    }
}
