using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace glitch.Physics
{
    public class PhysicsSystem
    {
        private static PhysicsSystem instance;

        public static PhysicsSystem Instance
        {
            get
            {
                if (instance == null)
                    instance = new PhysicsSystem();

                return instance;
            }
        }
        public double gravity = 9.8;

        private PhysicsSystem()
        {
        }

        public Vector2 applyEasement(GameTime time, Vector2 vect)
        {
            throw new NotImplementedException();
        }

        public void addComponent(PhysicsComponent newComponent)
        {
            throw new NotImplementedException();
        }

        public void applyGravity(GameTime time)
        {
            throw new NotImplementedException();
        }

        public void checkCollisions()
        {
            throw new NotImplementedException();
        }

        public void handleCollisions()
        {
            throw new NotImplementedException();
        }

    }
}
