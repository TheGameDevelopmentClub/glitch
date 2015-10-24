using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace glitch
{
    class RenderComponent
    {
        Texture2D sprite;

        public RenderComponent(Texture2D sprite)
        {
            this.sprite = sprite;
        }

        public void Render(SpriteBatch batch, Vector2 pos)
        {
            batch.Draw(this.sprite, pos, Color.White);
        }

        public void Render(SpriteBatch batch, int x, int y)
        {
            this.Render(batch, new Vector2(x, y));
        }

        public void Render(SpriteBatch batch, Point pos)
        {
            this.Render(batch, pos.X, pos.Y);
        }
    }
}
