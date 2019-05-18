using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;

namespace Zold.Inventory
{
    class Weapon:Item
    {
        private short baseDmg = 5;
        private short range = 32;
        private float dmgMultiplier = 1.0f;
        private float defMultiplier = 1.0f;
        private float sigma = 0.25f;
        //długość ataku względem jednej sekundy
        private float speed = 1.0f;

        #region constructors
        public Weapon(String name, ItemManager itemManager) : base(name, itemManager, "weapons")
        {
            JObject itemsBase = itemManager.ItemsBase;
            this.baseDmg = itemsBase["weapons"][name]["baseDmg"]!=null ? (short)itemsBase["weapons"][name]["baseDmg"] : (short)5;
            this.range = itemsBase["weapons"][name]["range"]!=null ? (short)itemsBase["weapons"][name]["range"] : range;
            this.dmgMultiplier = itemsBase["weapons"][name]["dmgMultiplier"]!=null ? (float)itemsBase["weapons"][name]["dmgMultiplier"] : dmgMultiplier;
            this.defMultiplier = itemsBase["weapons"][name]["defMultiplier"]!=null ? (float)itemsBase["weapons"][name]["defMultiplier"] : defMultiplier;
            this.sigma = itemsBase["weapons"][name]["sigma"]!=null ? (float)itemsBase["weapons"][name]["sigma"] : sigma;
            this.speed = itemsBase["weapons"][name]["speed"]!=null ? (float)itemsBase["weapons"][name]["speed"] : speed;
        }
        #endregion

        #region properties
        public short BaseDmg { get => baseDmg; set => baseDmg = value; }
        public short Range { get => range; set => range = value; }
        public float DmgMultiplier { get => dmgMultiplier; set => dmgMultiplier = value; }
        public float DefMultiplier { get => defMultiplier; set => defMultiplier = value; }
        public float Speed { get => speed; set => speed = value; }
        public float Sigma { get => sigma; set => sigma = value; }
        #endregion
    }
}
