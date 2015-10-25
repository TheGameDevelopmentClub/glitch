using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace glitch.Physics
{
    public class TeleportComponent : MechanicsBaseComponent
    {
        public Point Destination { get; private set; }

        public override bool StopPlayerMovement
        {
            get
            {
                return false;
            }
        }

        public TeleportComponent(int width, int height, Point destination) : base(width, height, PhysicsType.MechanicsObject)
        {
            this.Destination = destination;
        }

        public override void ApplyMechanic(PlayerObject player)
        {
            player.Teleport(Destination);
        }
    }
}
