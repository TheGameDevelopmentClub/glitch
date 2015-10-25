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
        public Hitbox hitBox;
        public PhysicsType type;

        public PhysicsComponent(int width, int height, PhysicsType type)
        {
            gravityScale = 1.0;
            velocity = Vector2.Zero;
            hitBox = new Hitbox(width, height);
            this.type = type;

            //PhysicsSystem.Instance.addComponent(this);
        }

        public void UpdateHitBoxPosition(int x, int y)
        {
            this.hitBox.UpdatePosition(x, y);
        }
        public void UpdateHitBoxPosition(Vector2 pos)
        {
            this.hitBox.UpdatePosition(pos);
        }

        public void ReSize(int width, int height)
        {
            this.hitBox.ReSize(width, height);
        }

        public void ReSize(Point size)
        {
            this.ReSize(size.X, size.Y);
        }

        public Vector2 ApplyVelocity(GameTime time, Vector2 position)
        {
            this.velocity.X = Math.Min(this.velocity.X, PlayerObject.MaxHorizontalVelocity);
            this.velocity.Y = Math.Min(this.velocity.Y, PlayerObject.MaxVerticalVelocity);

            Vector2 newPoint = new Vector2(position.X + (int)(this.velocity.X * time.ElapsedGameTime.TotalSeconds), position.Y + (float)(this.velocity.Y * time.ElapsedGameTime.TotalSeconds));

            this.UpdateHitBoxPosition(newPoint);

            return newPoint;
        }
    }
}