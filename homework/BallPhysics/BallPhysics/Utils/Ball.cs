using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.MonoGame;
using System.Diagnostics;

public enum BallTypes
{
    Green, 
    Red
}

namespace BallPhysics
{
    public class Ball
    {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }
        public BallTypes BallType { get; set; }
        public float Mass { get; set; }
        public bool ApplyGravity { get; set; }
        private int ballSegments = 36;


        public Vector2 speed;

        private float gravity;

        public Ball(BallTypes ballType, Vector2 startPosition, float radius = 20.0f, float mass = 1.0f, bool applyGravity = false, int ballSegments = 36)
        {
            BallType = ballType;
            Center = startPosition;
            Radius = radius;
            Mass = mass;
            ApplyGravity = applyGravity;
            this.ballSegments = ballSegments;
            this.speed = new Vector2(0, 0);

            gravity = Mass / 4;
        }

        public void Update(GameTime gameTime)
        {
            if (ApplyGravity)
            {
                Vector2 ballMovement = new Vector2(0, -gravity);
                Center -= ballMovement - speed;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            var ballColor = (Color)typeof(Color).GetProperty(Enum.GetName(typeof(BallTypes), BallType)).GetValue(null);
            spriteBatch.DrawCircle(Center, Radius, ballSegments, ballColor, thickness: Radius);


        }

        public bool IsCollidingWith(Ball otherBall)
        {
            return Vector2.Distance(Center, otherBall.Center) <= (Radius + otherBall.Radius);

        }

        


    }
}
