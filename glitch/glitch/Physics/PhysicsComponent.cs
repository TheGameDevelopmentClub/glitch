using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using glitch.Physics;

namespace glitch
{
    public enum PhysicsType
    {
        Player,
        StaticObject,
        Door
    }

    public class PhysicsComponent
    {
        public double gravityScale = 1.0;
        public Vector2 velocity = Vector2.Zero;
        public Rectangle hitBox;
        public PhysicsType type;

        public PhysicsComponent(int width, int height, PhysicsType type)
        {
            gravityScale = 1.0;
            velocity = Vector2.Zero;
            hitBox = new Rectangle(0, 0, width, height);
            this.type = type;

            //PhysicsSystem.Instance.addComponent(this);
        }

        public void UpdateHitBoxPosition(int x, int y)
        {
            this.hitBox.X = x;
            this.hitBox.Y = y;
        }

        public void UpdateHitBoxPosition(Point pos)
        {
            this.UpdateHitBoxPosition(pos.X, pos.Y);
        }

        public Point ApplyVelocity(GameTime time, Point position)
        {
            Point newPoint = new Point(position.X + (int)(this.velocity.X * time.ElapsedGameTime.TotalMilliseconds), position.Y + (int)(this.velocity.Y * time.ElapsedGameTime.TotalMilliseconds));
            this.UpdateHitBoxPosition(newPoint);

            return newPoint;
        }
    }
}
