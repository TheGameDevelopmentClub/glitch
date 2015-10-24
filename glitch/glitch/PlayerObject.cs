using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace glitch
{
    class PlayerObject : GameObject
    {

        public PlayerObject(Point position, Texture2D sprite, bool isVisible) : base(position, sprite, isVisible)
        {

        }

        public PlayerObject(int x, int y, Texture2D sprite, bool isVisible) : base(x, y, sprite, isVisible)
        {

        }


    }
}
