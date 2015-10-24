using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace glitch
{
    public class PlayerObject : GameObject
    {

        public float HorizontalAcceleration { get; set; }
        public float MaxHowizontalVelocity { get; set; }
        public Boolean IsJumping { get; set; }




        public PlayerObject(Point position, bool isVisible) : base(position, isVisible)
        {
            setDefaults();
        }

        public PlayerObject(int x, int y, bool isVisible) : base(x, y, isVisible)
        {
            setDefaults();
        }

        /// <summary>
        /// Set the default values for properties of PlayerObject
        /// </summary>
        private void setDefaults()
        {
            HorizontalAcceleration = 1.0f;
            MaxHowizontalVelocity = 10.0f;
            IsJumping = false;
        }
    }
}
