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
        private static readonly int StraightAngle = 180;
        public static Vector2 GravitationVector = new Vector2(0, 1);
        public static float MaxSpeed = 4f;

        private float startEnergy = MaxSpeed;
        private Vector2 movementVector = new Vector2(0, 0);
        private int mass;
        public int Mass { get { return radius * 2; } }


        public Vector2 MovementVector { get { return movementVector; } }
        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, int mass, Color color) : base(graphicsDevice, radius, color)
        {
            this.mass = mass;
        }
        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, int mass, Color color, Vector2 position) : this(graphicsDevice, radius, mass, color)
        {
            ChangePosition(position);
        }

        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, int mass, Color color, Vector2 position, Vector2 movementVector) : this(graphicsDevice, radius, mass, color, position)
        {
            this.movementVector = movementVector;
        }

        public void Gravity(float magnitude)
        {
            movementVector += GravitationVector * magnitude;

            ChangePosition(Position + movementVector);

        }


        public void BounceOnCollisionWithOtherBall(GraphicCircleWithPhysics graphicCircle)
        {
            if (CheckCollisionWithBoundingSphere(graphicCircle.BoundingSphere))
            {
                movementVector = BounceVector(movementVector, graphicCircle);

                movementVector = DirectionVectorFromBaunding(movementVector, position, graphicCircle.Position);

                movementVector = AdjustVectorToMaxSpeed(movementVector, MaxSpeed);


                //Nie działa z powodu problemu z rozmiarem BoundingSpehere. Punkt oraz Promień odpowiadają
                //wyświetlanemu kółku, ale w prakty zaczyna łapać kolizje trochę przed lub w środku kółka.
                //Gdy tak się dzieje cała "energia" momentalnie się wyczerpuje.

                //EnergyLoss(graphicCircle);

            }
        }

        private Vector2 BounceVector(Vector2 movementVector, GraphicCircleWithPhysics graphicCircle)
        {
            var bounceVector = graphicCircle.MovementVector + movementVector;
            bounceVector = -bounceVector * MaxSpeed;

            bounceVector *= graphicCircle.Mass / Mass;

            movementVector += bounceVector;

            return movementVector;
        }

        private Vector2 DirectionVectorFromBaunding(Vector2 movement, Vector2 position, Vector2 targetPosition)
        {
            var bounceAngle = AngleOfVectors(targetPosition, position);
            bounceAngle += StraightAngle;


            var direction = (float)Math.Sin(bounceAngle);


            if (bounceAngle > 0 && bounceAngle <= 90)
            {
                movement.X -= direction;
                movement.Y += direction;

            }
            else if (bounceAngle > 90 && bounceAngle <= 180)
            {
                movement.X -= direction;
                movement.Y -= direction;

            }
            else if (bounceAngle > 180 && bounceAngle <= 270)
            {
                movement.X += direction;
                movement.Y -= direction;

            }
            else if (bounceAngle > 270 && bounceAngle <= 360)
            {
                movement.X += direction;
                movement.Y += direction;
            }


            return movement;
        }

        private Vector2 AdjustVectorToMaxSpeed(Vector2 vector, float max)
        {

            var x = Math.Abs(vector.X);
            var y = Math.Abs(vector.Y);

            if (x > max)
            {
                if (vector.X < 0)
                {
                    vector.X = -max;
                }
                if (vector.X > 0)
                {
                    vector.X = max;

                }
            }

            if (y > max)
            {
                if (vector.Y < 0)
                {
                    vector.Y = -max;
                }
                if (vector.Y > 0)
                {
                    vector.Y = max;

                }
            }

            return vector;
        }

        private void EnergyLoss(GraphicCircleWithPhysics graphicCircle)
        {
            startEnergy -= Math.Abs(Mass - graphicCircle.Mass / Mass);
            startEnergy = Math.Max(0, startEnergy);
            movementVector *= startEnergy;
        }

        private double AngleOfVectors(Vector2 first, Vector2 secound)
        {
            return Math.Atan2(first.Y - secound.Y, first.X - secound.X) * StraightAngle / Math.PI;
        }


    }
}
