using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace glitch
{
    public class GameObject
    {
        public Rectangle drawSpace;
        public bool isVisible;
        public PhysicsComponent physComp;
        public RenderComponent rendComp;

        public GameObject(Point position, Texture2D sprite, bool isVisible)
        {
            initGameObject(position, sprite, isVisible);
        }

        public GameObject(int x, int y, Texture2D sprite, bool isVisible)
        {
            initGameObject(new Point(x, y), sprite, isVisible);
        }

        private void initGameObject(Point position, Texture2D sprite, bool isVisible)
        {
            //this.position = position;
            this.drawSpace = new Rectangle(position.X, position.Y, sprite.Width/2, sprite.Height/2);
            this.isVisible = isVisible;

            this.rendComp = new RenderComponent(sprite);
            this.physComp = new PhysicsComponent(sprite.Width, sprite.Height);
            this.physComp.UpdatePosition(sprite.Width / 2, sprite.Height / 2);
        } 

        public void Render(SpriteBatch batch)
        {
            if (this.isVisible)
            {
                this.rendComp.Render(batch, this.drawSpace);
            }
        }
    }
}
