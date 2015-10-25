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
        public Vector2 position;

        public Point Size
        {
            get { return drawSpace.Size;}
            set { drawSpace.Size = value; this.physComp.ReSize(value); }
        }

        public Vector2 Location
        {
            get { return position; }
            set { position = value;  drawSpace.Location = value.ToPoint(); this.physComp.UpdateHitBoxPosition(value); }
        }
        public GameObject(Vector2 position, Texture2D sprite, bool isVisible, PhysicsType type)
        {
            initGameObject(position, sprite, isVisible, type);
        }

        public GameObject(int x, int y, Texture2D sprite, bool isVisible, PhysicsType type)
        {
            initGameObject(new Vector2(x, y), sprite, isVisible, type);
        }

        public GameObject(Vector2 position, Texture2D sprite, bool isVisible, MechanicsBaseComponent component)
        {
            initGameObject(position, sprite, isVisible, PhysicsType.MechanicsObject);
            this.physComp = component;
            PhysicsSystem.Instance.addMechanicObject(component);
            this.physComp.UpdateHitBoxPosition(position);
        }

        private void initGameObject(Vector2 position, Texture2D sprite, bool isVisible, PhysicsType type)
        {
            //this.position = position;
            this.position = position;
            this.drawSpace = new Rectangle(position.ToPoint().X, position.ToPoint().Y, sprite.Width, sprite.Height);
            this.isVisible = isVisible;

            this.rendComp = new RenderComponent(sprite);

            if(type != PhysicsType.MechanicsObject)
            {
                this.physComp = new PhysicsComponent(sprite.Width, sprite.Height, type);
                PhysicsSystem.Instance.addStaticObject(this.physComp);
                this.physComp.UpdateHitBoxPosition(position);
            }
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
