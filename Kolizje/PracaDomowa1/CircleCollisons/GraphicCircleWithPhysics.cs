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
        private static Random random = new Random();
        private Vector2 movementVector = new Vector2(0, 1); // + -> , - <-

        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, Color color) : base(graphicsDevice, radius, color)
        {
        }

        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, Color color, Vector2 position) : this(graphicsDevice, radius, color)
        {
            ChangePosition(position);
        }

        public void Gravity(float magnitude)
        {
            ReduceMovementVectorXtoZero(magnitude);

            if (movementVector.Y > 1)
                movementVector.Y = Math.Min(movementVector.Y, Math.Abs(movementVector.Y - magnitude));
            else
                movementVector.Y = Math.Max(movementVector.Y, movementVector.Y + magnitude);

            ChangePosition(Position + movementVector * magnitude * Radius);

        }

        private void ReduceMovementVectorXtoZero(float magnitude)
        {
            if (movementVector.X > 0)
                movementVector.X = Math.Min(movementVector.X, Math.Abs(movementVector.X - magnitude));
            else
                movementVector.X = Math.Max(movementVector.X, movementVector.X + magnitude);

        }

        public void BounceOnCollisionWithOtherBall(GraphicCircle graphicCircle)
        {
            if (CheckCollisionWithBoundingSphere(graphicCircle.BoundingSphere) && Vector2.Distance(graphicCircle.Position,Position) < graphicCircle.Radius + Radius)
            {
                var radiusRatio = Radius - graphicCircle.Radius;
                var massRatio = Mass - graphicCircle.Mass;

                var bounceAngle = AngleOfVectors(graphicCircle.Position, Position);
                if (bounceAngle < 0) bounceAngle = AngleOfVectors(Position, graphicCircle.Position);

                if (bounceAngle > 90)
                {
                    movementVector.X = movementVector.X + 0.1f;
                }
                else
                {
                    movementVector.X = movementVector.X - 0.1f;

                }
                movementVector.Y = movementVector.Y - 0.1f;



            }
        }

        private double AngleOfVectors(Vector2 first, Vector2 secound)
        {
            return Math.Atan2(first.Y - secound.Y, first.X - secound.X) * 180 / Math.PI;
        }

    }
}
