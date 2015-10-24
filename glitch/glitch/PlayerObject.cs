using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace glitch
{
    public class PlayerObject : GameObject
    {
        public float HorizontalAcceleration { get; set; }
        public float MaxHorizontalVelocity { get; set; }
        public Boolean IsJumping { get; set; }

        public PlayerObject(Point position, Texture2D sprite, bool isVisible, PhysicsType type) : base(position, sprite, isVisible, type)
        {
            setDefaults();
        }


        public PlayerObject(int x, int y, Texture2D sprite, bool isVisible, PhysicsType type) : base(x, y, sprite, isVisible, type)
        {
            setDefaults();
        }

        /// <summary>
        /// Set the default values for properties of PlayerObject
        /// </summary>
        private void setDefaults()
        {
            HorizontalAcceleration = 1.0f;
            MaxHorizontalVelocity = 0.5f;
            IsJumping = false;
        }
    }
}
