using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace glitch.Physics
{
    public abstract class MechanicsBaseComponent : PhysicsComponent
    {
        public MechanicsBaseComponent(int width, int height, PhysicsType type):base(width, height, type) { }

        public abstract bool StopPlayerMovement { get; }

        /// <summary>
        /// The mechanic doesn't necessarily have affect the player directly.
        /// The player is passed in here as a convenience
        /// </summary>
        /// <param name="player">Convenience reference to the Mechanic Method</param>
        public abstract void ApplyMechanic(PlayerObject player);
    }
}
