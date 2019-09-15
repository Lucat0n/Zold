using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaDomowa1.CircleCollisons
{
    class GraphicCircleWithPhysics : GraphicCircle
    {
        public static Vector2 GravitationVector = new Vector2(0, 1);
        public static Vector2 MaxSpeedVector = new Vector2(3, 3);
        //public static float EnergyLossByHit = 0.1f; 

        private float startEnergy = MaxSpeedVector.Length() * 10;
        private Vector2 movementVector = new Vector2(0, 0); // + -> , - <-
        public int Mass { get { return radius * 2; } }


        public Vector2 MovementVector { get { return movementVector; } }
        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, Color color) : base(graphicsDevice, radius, color)
        {
            
        }
        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, Color color, Vector2 position) : this(graphicsDevice, radius, color)
        {
            ChangePosition(position);
        }

        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, Color color, Vector2 position, Vector2 movementVector) : this(graphicsDevice, radius, color, position)
        {
            this.movementVector = movementVector;
        }

        public void Gravity(float magnitude)
        {

            //ReduceMovementVectorXtoZero(magnitude);

            //if (movementVector.Y > 1)
            //    movementVector.Y = Math.Min(movementVector.Y, Math.Abs(movementVector.Y - magnitude));
            //else
            //    movementVector.Y = Math.Max(movementVector.Y, movementVector.Y + magnitude);

            movementVector += GravitationVector * magnitude;

            ChangePosition(Position + movementVector);

        }

        private void ReduceMovementVectorXtoZero(float magnitude)
        {
            if (movementVector.X > 0)
                movementVector.X = Math.Min(movementVector.X, Math.Abs(movementVector.X - magnitude));
            else
                movementVector.X = Math.Max(movementVector.X, movementVector.X + magnitude);

        }

        public void BounceOnCollisionWithOtherBall(GraphicCircleWithPhysics graphicCircle)
        {
            if (CheckCollisionWithBoundingSphere(graphicCircle.BoundingSphere))
            {
                var bounceVector = graphicCircle.MovementVector + MovementVector;
                bounceVector = -bounceVector * MaxSpeedVector.Length();

                bounceVector *= graphicCircle.Mass / Mass;


                movementVector += bounceVector;

                var bounceAngle = AngleOfVectors(graphicCircle.Position, Position);
                bounceAngle += 180;

                var direction = (float)Math.Sin(bounceAngle);

                if (bounceAngle > 0 && bounceAngle <= 90)
                {
                    movementVector.X -= direction;
                    movementVector.Y += direction;

                }
                else if (bounceAngle > 90 && bounceAngle <= 180)
                {
                    movementVector.X -= direction;
                    movementVector.Y -= direction;

                }
                else if (bounceAngle > 180 && bounceAngle <= 270)
                {
                    movementVector.X += direction;
                    movementVector.Y -= direction;

                }
                else if (bounceAngle > 270 && bounceAngle <= 360)
                {
                    movementVector.X += direction;
                    movementVector.Y += direction;
                }

                if (movementVector.X < 0 && movementVector.X < -MaxSpeedVector.X)
                {
                    movementVector.X = -MaxSpeedVector.X;
                }

                if (movementVector.X > 0 && movementVector.X > MaxSpeedVector.X)
                {
                    movementVector.X = MaxSpeedVector.X;
                }

                if (movementVector.Y < 0 && movementVector.Y < -MaxSpeedVector.Y)
                {
                    movementVector.Y = -MaxSpeedVector.Y;
                }

                if (movementVector.Y > 0 && movementVector.Y > MaxSpeedVector.Y)
                {
                    movementVector.Y = MaxSpeedVector.Y;
                }

                //startEnergy -= Math.Abs(Mass - graphicCircle.Mass / Mass);
                //startEnergy = Math.Max(0, startEnergy);
                //movementVector *= startEnergy;

            }
        }

        private double AngleOfVectors(Vector2 first, Vector2 secound)
        {
            return Math.Atan2(first.Y - secound.Y, first.X - secound.X) * 180 / Math.PI;
        }

    }
}
