using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolizje.Collisions
{
    public class BoundingBox2D
    {
        private Rectangle rectangle;

        public Point Position { get; set; } = new Point(0, 0);

        public Point LeftTopCorner { get { return PointTranslation(Position, 0, 0); } }
        public Point RightTopCorner { get { return PointTranslation(Position, rectangle.Width, 0); } }
        public Point RightBottomCorner { get { return PointTranslation(Position, rectangle.Width, rectangle.Height); } }
        public Point LeftBottomCorner { get { return PointTranslation(Position, 0, rectangle.Height); } }


        public List<Point> TopWall
        {
            get
            {
                return new List<Point>();
            }
        }

        public BoundingBox2D(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }

        private Point PointTranslation(Point original, int x, int y)
        {
            return new Point(original.X + x, original.Y + y);
        }

        
    }




}
