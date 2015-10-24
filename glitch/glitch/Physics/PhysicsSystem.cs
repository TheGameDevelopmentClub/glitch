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
        public double gravity = 1000;

        public PlayerObject player;
        List<PhysicsComponent> staticObjects;
        public PhysicsComponent door;

        List<PhysicsComponent> possiblePlayerCollisions;
        List<PhysicsComponent> playerCollisions;

        private PhysicsSystem()
        {
            staticObjects = new List<PhysicsComponent>();
            possiblePlayerCollisions = new List<PhysicsComponent>();
            playerCollisions = new List<PhysicsComponent>();
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
            //player.physComp.velocity.Y = (float)Math.Min(player.HorizontalAcceleration, player.physComp.velocity.Y + (this.gravity * time.ElapsedGameTime.TotalSeconds));
            player.physComp.velocity.Y += (float)(this.gravity * time.ElapsedGameTime.TotalSeconds);
        }

        public void checkPlayerCollisions()
        {
            possiblePlayerCollisions.Clear();
            playerCollisions.Clear();

            foreach(PhysicsComponent phys in staticObjects)
            {
                Vector2 diffVector = phys.hitBox.overall.Center.ToVector2() - player.physComp.hitBox.overall.Center.ToVector2();
                
                if (player.physComp.hitBox.isNearby(phys.hitBox))
                    possiblePlayerCollisions.Add(phys);
            }

            foreach(PhysicsComponent phys in possiblePlayerCollisions)
            {
                if (player.physComp.hitBox.isTouching(phys.hitBox))
                    playerCollisions.Add(phys);
            }
        }

        public void handleCollisions()
        {
            PhysicsComponent playerPhys = player.physComp;
            foreach(PhysicsComponent phys in playerCollisions)
            {
                HitboxHit result = player.physComp.hitBox.Intersects(phys.hitBox);
                Vector2 offset = Vector2.Zero;

                switch (result)
                {
                    case HitboxHit.Bottom:
                        offset.Y -= player.physComp.hitBox.overall.Bottom - phys.hitBox.overall.Top;
                        player.physComp.velocity.Y = 0;
                        player.IsJumping = false;
                        break;

                    case HitboxHit.Right:
                        offset.X -= phys.hitBox.overall.Left - player.physComp.hitBox.overall.Right;
                        player.physComp.velocity.X = 0;
                        break;

                    case HitboxHit.Left:
                        offset.X += player.physComp.hitBox.overall.Left - phys.hitBox.overall.Right;
                        player.physComp.velocity.X = 0;
                        break;

                    case HitboxHit.Top:
                        offset.Y += phys.hitBox.overall.Bottom - player.physComp.hitBox.overall.Top;
                        player.physComp.velocity.Y = 0;
                        break;

                    case HitboxHit.None:
                        continue;
                }

                player.Location += offset;
            }
            
            if(player.position.Y > Game1.Screen.Height + 100)
            {
                player.Respawn();
            }
        }

    }
}
