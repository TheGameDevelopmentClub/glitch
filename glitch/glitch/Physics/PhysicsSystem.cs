using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

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
        public int minObjectDistanceToCheckCollisions = 400;

        public PlayerObject player;
        List<PhysicsComponent> staticObjects;
        public PhysicsComponent door;

        List<PhysicsComponent> possiblePlayerCollisions;
        List<PhysicsComponent> playerCollisions;

        private PhysicsSystem()
        {
        }

        public Vector2 applyEasement(GameTime time, Vector2 vect)
        {
            //throw new NotImplementedException();
            return Vector2.Zero;
        }

        public void addStaticObject(PhysicsComponent newComponent)
        {
            staticObjects.Add(newComponent);
        }

        public void applyGravityToPlayer(GameTime time)
        {
            player.physComp.velocity.Y = Math.Min(player.HorizontalAcceleration, player.physComp.velocity.Y + (int)(this.gravity * time.ElapsedGameTime.TotalMilliseconds));
        }

        public void checkPlayerCollisions()
        {
            possiblePlayerCollisions.Clear();

            foreach(PhysicsComponent phys in staticObjects)
            {
                Vector2 diffVector = phys.hitBox.Center.ToVector2() - player.physComp.hitBox.Center.ToVector2();
                if (diffVector.LengthSquared() < minObjectDistanceToCheckCollisions)
                    possiblePlayerCollisions.Add(phys);
            }
        }

        public void handleCollisions()
        {
            throw new NotImplementedException();
        }

    }
}
