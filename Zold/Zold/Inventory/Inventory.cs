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

        public Dictionary<string, ItemAmountPair> Items { get => items; set => items = value; }

        public class ItemAmountPair
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
            Items = new Dictionary<String, ItemAmountPair>();
        }            
        /// <summary>
        /// Dodaje dokładnie jedną sztukę danego przedmiotu
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        public void InsertItem(String name, Item item)
        {
            if (Items.ContainsKey(name))
            {
                Items[name].Amount++;
            }
            else
            {
                Items.Add(name, new ItemAmountPair(item, 1));
            }
        }
        public void InsertItem(String name, Item item, byte amount)
        {
            if (Items.ContainsKey(name))
            {
                Items[name].Amount += amount;
            }
            else
            {
                Items.Add(name, new ItemAmountPair(item, amount));
            }
        }

        /// <summary>
        /// Usuwa wszystkie sztuki przedmiotu o podanej nazwie
        /// </summary>
        public void RemoveItem(String name)
        {
            if (Items.ContainsKey(name))
            {
                Items.Remove(name);
            }
        }

        /// <summary>
        /// Usuwa podaną liczbę sztuk przedmiotu o podanej nazwie
        /// </summary>
        public void RemoveItem(String name, byte amount)
        {
            if (Items.ContainsKey(name) && Items[name].Amount>=amount)
            {
                Items[name].Amount -= amount;
            }
        }

        /// <summary>
        /// Zwraca liczbę sztuk danego przedmiotu
        /// </summary>
        public byte GetAmount(String name)
        {
            if (Items.ContainsKey(name))
                return Items[name].Amount;
            return 0;
        }

        public Item GetItem(String name)
        {
            if (Items.ContainsKey(name))
            {
                return Items[name].Item;
            }
            return null;
        }

        public void ClearInventory()
        {
            Items.Clear();
        }

        /// <summary>
        /// Zwraca słownik par przedmiot-liczba sztuk
        /// </summary>
        /// <returns></returns>
        public Dictionary<Item, byte> GetWholeInventory()
        {
            Dictionary<Item, byte> itemsDict = new Dictionary<Item, byte>();
            foreach(KeyValuePair<String, ItemAmountPair>pair in Items)
            {
                itemsDict.Add(pair.Value.Item, pair.Value.Amount);
            }
            return itemsDict;
        }
    }
}
