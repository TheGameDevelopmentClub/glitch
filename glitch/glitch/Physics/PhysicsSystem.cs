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
        public bool playerTouchedDoor = false;
        private bool playerCollidingWithDoor = false;

        public PlayerObject player;
        List<PhysicsComponent> staticObjects;
        List<MechanicsBaseComponent> mechanicsObjects;
        public PhysicsComponent door;

        List<PhysicsComponent> possiblePlayerStaticCollisions;
        List<PhysicsComponent> playerStaticCollisions;

        List<MechanicsBaseComponent> possiblePlayerMechanicsCollisions;
        List<MechanicsBaseComponent> playerMechanicsCollisions;

        private PhysicsSystem()
        {
            staticObjects = new List<PhysicsComponent>();
            possiblePlayerStaticCollisions = new List<PhysicsComponent>();
            playerStaticCollisions = new List<PhysicsComponent>();

            mechanicsObjects = new List<MechanicsBaseComponent>();
            possiblePlayerMechanicsCollisions = new List<MechanicsBaseComponent>();
            playerMechanicsCollisions = new List<MechanicsBaseComponent>();
        }

        public void ClearStage()
        {
            staticObjects.Clear();
            mechanicsObjects.Clear();
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

        public void addMechanicObject(MechanicsBaseComponent newComponent)
        {
            mechanicsObjects.Add(newComponent);
        }

        public void addDoorObject(PhysicsComponent newComponent)
        {
            door = newComponent;
        }

        public void applyGravityToPlayer(GameTime time)
        {
            player.physComp.velocity.Y += (float)(this.gravity * time.ElapsedGameTime.TotalSeconds);
        }

        private bool isComponentCloseEnoughToPlayer(PhysicsComponent phys)
        {
            if (phys == null) return false;

            Vector2 diffVector = phys.hitBox.overall.Center.ToVector2() - player.physComp.hitBox.overall.Center.ToVector2();

            if (player.physComp.hitBox.isNearby(phys.hitBox))
                return true;
            else
                return false;
        }

        private bool isCollidingWithPlayer(PhysicsComponent phys)
        {
            return player.physComp.hitBox.isTouching(phys.hitBox);
        } 

        public void checkPlayerCollisions()
        {
            possiblePlayerStaticCollisions.Clear();
            playerStaticCollisions.Clear();
            possiblePlayerMechanicsCollisions.Clear();
            playerMechanicsCollisions.Clear();

            foreach(PhysicsComponent phys in staticObjects)
            {
                if(isComponentCloseEnoughToPlayer(phys))
                    possiblePlayerStaticCollisions.Add(phys);
            }

            foreach(PhysicsComponent phys in possiblePlayerStaticCollisions)
            {
                if (isCollidingWithPlayer(phys))
                    playerStaticCollisions.Add(phys);
            }

            foreach(MechanicsBaseComponent mech in mechanicsObjects)
            {
                if (isComponentCloseEnoughToPlayer(mech))
                    possiblePlayerMechanicsCollisions.Add(mech);
            }

            foreach(MechanicsBaseComponent mech in possiblePlayerMechanicsCollisions)
            {
                if (isCollidingWithPlayer(mech))
                    playerMechanicsCollisions.Add(mech);
            }

            if (isComponentCloseEnoughToPlayer(door))
            {
                if (isCollidingWithPlayer(door))
                {
                    playerCollidingWithDoor = true;
                }
            }
            
        }

        private void StopPlayerMotionOnCollision(PhysicsComponent phys)
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
                    offset.X -=  player.physComp.hitBox.overall.Right - phys.hitBox.overall.Left;
                    player.physComp.velocity.X = 0;
                    break;

                case HitboxHit.Left:
                    offset.X += phys.hitBox.overall.Right - player.physComp.hitBox.overall.Left;
                    player.physComp.velocity.X = 0;
                    break;

                case HitboxHit.Top:
                    offset.Y += phys.hitBox.overall.Bottom - player.physComp.hitBox.overall.Top;
                    player.physComp.velocity.Y = 0;
                    break;

                case HitboxHit.None:
                    return;
            }

            player.Location += offset;
        }

        public void handleCollisions()
        {
            if (playerCollidingWithDoor)
            {
                playerCollidingWithDoor = false;
                playerTouchedDoor = true;
            }
            else
            {
                PhysicsComponent playerPhys = player.physComp;

                if (player.position.Y > Game1.Screen.Height + 100)
                {
                    player.Respawn();
                }

                foreach (PhysicsComponent phys in playerStaticCollisions)
                {
                    StopPlayerMotionOnCollision(phys);
                }

                foreach (MechanicsBaseComponent phys in playerMechanicsCollisions)
                {
                    if (phys.StopPlayerMovement)
                        StopPlayerMotionOnCollision(phys);

                    phys.ApplyMechanic(player);
                }
            }
        }

    }
}
