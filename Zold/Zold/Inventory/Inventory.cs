using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Inventory
{
    class Inventory
    {
        private Dictionary<String, ItemAmountPair> items;

        class ItemAmountPair
        {
            private Item item;
            private byte amount;

            internal ItemAmountPair(Item item, byte amount)
            {
                this.item = item;
                this.amount = amount;
            }

            internal Item Item { get => item; set => item = value; }
            public byte Amount { get => amount; set => amount = value; }
        }
        //internal Dictionary<String, Dictionary<Item, byte>> Items { get => items; set => items = value; }

        public Inventory()
        {
            items = new Dictionary<String, ItemAmountPair>();
        }            
        /// <summary>
        /// Dodaje dokładnie jedną sztukę danego przedmiotu
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        public void InsertItem(String name, Item item)
        {
            if (items.ContainsKey(name))
            {
                items[name].Amount++;
            }
            else
            {
                items.Add(name, new ItemAmountPair(item, 1));
            }
        }
        public void InsertItem(String name, Item item, byte amount)
        {
            if (items.ContainsKey(name))
            {
                items[name].Amount += amount;
            }
            else
            {
                items.Add(name, new ItemAmountPair(item, amount));
            }
        }

        /// <summary>
        /// Usuwa wszystkie sztuki przedmiotu o podanej nazwie
        /// </summary>
        public void RemoveItem(String name)
        {
            if (items.ContainsKey(name))
            {
                items.Remove(name);
            }
        }

        /// <summary>
        /// Usuwa podaną liczbę sztuk przedmiotu o podanej nazwie
        /// </summary>
        public void RemoveItem(String name, byte amount)
        {
            if (items.ContainsKey(name) && items[name].Amount>=amount)
            {
                items[name].Amount -= amount;
            }
        }

        public byte CheckAmount(String name)
        {
            if (items.ContainsKey(name))
                return items[name].Amount;
            return 0;
        }

        public Item GetItem(String name)
        {
            if (items.ContainsKey(name))
            {
                return items[name].Item;
            }
            return null;
        }

        public void ClearInventory()
        {
            items.Clear();
        }

        /// <summary>
        /// Zwraca parę słownik par przedmiot-liczba sztuk
        /// </summary>
        /// <returns></returns>
        public Dictionary<Item, byte> GetWholeInventory()
        {
            Dictionary<Item, byte> itemsDict = new Dictionary<Item, byte>();
            foreach(KeyValuePair<String, ItemAmountPair>pair in items)
            {
                itemsDict.Add(pair.Value.Item, pair.Value.Amount);
            }
            return itemsDict;
        }
    }
}
