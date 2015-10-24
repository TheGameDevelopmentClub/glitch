using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace glitch
{
    public class PlayerObject : GameObject
    {

        public PlayerObject(Point position, Texture2D sprite, bool isVisible, PhysicsType type) : base(position, sprite, isVisible, type)
        {

        }

        public PlayerObject(int x, int y, Texture2D sprite, bool isVisible, PhysicsType type) : base(x, y, sprite, isVisible, type)
        {

        }


    }
}
