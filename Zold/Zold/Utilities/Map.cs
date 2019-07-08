using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Utilities
{
    class Map
    {

        private List<mapNode> locations;
        private mapNode playerLocation;

        public Map()
        {
            Locations = new List<mapNode>();
            /*PlayerLocation = new mapNode(new Point(310, 310), "Dormitory", null);
            PlayerLocation.IsVisited = true;
            PlayerLocation.IsFriendly = true;
            PlayerLocation.Name = "Akademik";
            mapNode[] mn = new mapNode[] { PlayerLocation, null, null, null };
            mapNode dormitoryOutside = new mapNode(new Point(400, 200), "DormOutside", mn);
            mapNode[] mn2 = new mapNode[] { null, null, PlayerLocation, null };
            mapNode dormitoryOutside2 = new mapNode(new Point(50, 450), "DormOutside2", mn2);
            dormitoryOutside.Name = "Dziedziniec akademika";
            dormitoryOutside2.Name = "Dziedziniec akademika2";
            PlayerLocation.Neighbours[3] = dormitoryOutside;
            PlayerLocation.Neighbours[1] = dormitoryOutside2;*/
            //Locations.Add(PlayerLocation);
            //Locations.Add(dormitoryOutside);
            //Locations.Add(dormitoryOutside2);
        }

        internal List<mapNode> Locations { get => locations; set => locations = value; }
        internal mapNode PlayerLocation { get => playerLocation; set => playerLocation = value; }

        internal class mapNode
        {
            private Point location;
            private mapNode[] neighbours;
            private String name;
            private String targetMapID;
            private bool isFriendly = false;
            private bool isVisited = false;

            public mapNode()
            {
                location = new Point();
                neighbours = new mapNode[4];
                targetMapID = "";
            }

            public mapNode(Point location, String targetMapID, mapNode[] neighbours)
            {
                this.location = location;
                this.neighbours = neighbours ?? new mapNode[4];
                this.targetMapID = targetMapID;
            }

            public mapNode(Point location, String targetMapID, bool isFriendly, bool isVisited, mapNode[] neighbours)
            {
                this.location = location;
                this.neighbours = neighbours ?? new mapNode[4];
                this.targetMapID = targetMapID;
                this.isFriendly = isFriendly;
                this.isVisited = isVisited;
            }

            public string Name { get => name; set => name = value; }
            internal bool IsFriendly { get => isFriendly; set => isFriendly = value; }
            internal bool IsVisited { get => isVisited; set => isVisited = value; }
            internal Point Location { get => location; set => location = value; }
            internal mapNode[] Neighbours { get => neighbours; }
            internal String TargetMapID { get => targetMapID; set => targetMapID = value; }
        }

        public void addLocation(mapNode mapNode)
        {
            Locations.Add(mapNode);
        }

    }
}
