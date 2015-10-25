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
        public Point SpawnPoint { get; set; }
        public int DeathCount { get; set; }

        public PlayerObject(Vector2 position, Texture2D sprite, bool isVisible, PhysicsType type) : base(position, sprite, isVisible, type)
        {
            setDefaults();
        }


        public PlayerObject(int x, int y, Texture2D sprite, bool isVisible, PhysicsType type) : base(x, y, sprite, isVisible, type)
        {
            setDefaults();
        }

        public void Respawn(Point position)
        {
            this.Location = position.ToVector2();
        }

        public void Respawn()
        {
            DeathCount++;
            this.Respawn(SpawnPoint);
            Game1.AddDeathSymbols(DeathCount);
        }

        /// <summary>
        /// Set the default values for properties of PlayerObject
        /// </summary>
        private void setDefaults()
        {
            HorizontalAcceleration = 10.0f;
            MaxHorizontalVelocity = 300f;
            IsJumping = false;
            SpawnPoint = Game1.Screen.Center;
            DeathCount = 0;

        }
    }
}
