using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace glitch
{
    class GameObject
    {
        public Point position;
        public bool isVisible;
        public PhysicsComponent physComp;
        public RenderComponent rendComp;

        public GameObject(Point position, bool isVisible)
        {
            this.position = position;
            this.isVisible = isVisible;
        }

        public GameObject(int x, int y, bool isVisible)
        {
            this.position = new Point(x, y);
            this.isVisible = isVisible;
        }
    }
}
