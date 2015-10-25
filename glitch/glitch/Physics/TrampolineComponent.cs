using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace glitch.Physics
{
    class TrampolineComponent : MechanicsBaseComponent
    {
        float jumpMagnifier;
        public override bool StopPlayerMovement
        {
            get
            {
                return false;
            }
        }

        public TrampolineComponent(int width, int height, float jumpMagnifier) : base(width, height, PhysicsType.MechanicsObject) { this.jumpMagnifier = jumpMagnifier; }

        public override void ApplyMechanic(PlayerObject player)
        {
            player.physComp.velocity.Y = -(PlayerObject.JumpSpeed * jumpMagnifier);
        }
    }
}
