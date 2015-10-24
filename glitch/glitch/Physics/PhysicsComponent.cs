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
    class PhysicsComponent
    {
        public double gravityScale = 1.0;
        public Vector2 velocity = Vector2.Zero;
        public Rectangle hitBox;

        public PhysicsComponent(int width, int height)
        {
            gravityScale = 1.0;
            velocity = Vector2.Zero;
            hitBox = new Rectangle(0, 0, width, height);

            PhysicsSystem.Instance.addComponent(this);
        }

        public void UpdatePosition(int x, int y)
        {
            this.hitBox.X = x;
            this.hitBox.Y = y;
        }
    }
}
