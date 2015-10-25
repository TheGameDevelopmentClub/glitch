using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace glitch.Physics
{
    public class TrampolineComponent : MechanicsBaseComponent
    {
        float xVelocity, yVelocity;
        public override bool StopPlayerMovement
        {
            get
            {
                return false;
            }
        }

        public TrampolineComponent(int width, int height, float jumpMagnifier) : base(width, height, PhysicsType.MechanicsObject) { this.xVelocity = 0; this.yVelocity = -1 * (PlayerObject.JumpSpeed * jumpMagnifier); }

        public TrampolineComponent(int width, int height, int xVelocity, int yVelocity) : base (width, height, PhysicsType.MechanicsObject)
        {
            this.xVelocity = xVelocity;
            this.yVelocity = yVelocity;
        }

        public override void ApplyMechanic(PlayerObject player)
        {
            player.physComp.velocity = new Vector2(xVelocity, yVelocity);
            Game1.sounds["trampoline"].Play();
        }
    }
}
