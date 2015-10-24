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
    public class GameObject
    {
        private Rectangle drawSpace;
        public bool isVisible;
        public PhysicsComponent physComp;
        public RenderComponent rendComp;

        public Point Size
        {
            get { return drawSpace.Size;}
            set { drawSpace.Size = value; this.physComp.ReSize(value); }
        }

        public Point Location
        {
            get { return drawSpace.Location; }
            set { drawSpace.Location = value; this.physComp.UpdateHitBoxPosition(value); }
        }
        public GameObject(Point position, Texture2D sprite, bool isVisible, PhysicsType type)
        {
            initGameObject(position, sprite, isVisible, type);
        }

        public GameObject(int x, int y, Texture2D sprite, bool isVisible, PhysicsType type)
        {
            initGameObject(new Point(x, y), sprite, isVisible, type);
        }

        private void initGameObject(Point position, Texture2D sprite, bool isVisible, PhysicsType type)
        {
            //this.position = position;
            this.drawSpace = new Rectangle(position.X, position.Y, sprite.Width, sprite.Height);
            this.isVisible = isVisible;

            this.rendComp = new RenderComponent(sprite);
            this.physComp = new PhysicsComponent(sprite.Width, sprite.Height, type);

            this.physComp.UpdateHitBoxPosition(position);
            
            if(type == PhysicsType.StaticObject)
                PhysicsSystem.Instance.addStaticObject(this.physComp);
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
