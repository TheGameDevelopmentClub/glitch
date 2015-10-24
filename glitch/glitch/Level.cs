using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace glitch
{
    class Level
    {

        public Point PlayerStaringPoint { get; set; }
        public Point DoorPoint { get; set; }
        public int LevelHeight { get; set; }
        public int LevelNumber { get; set; }


        public Level(int levelNumber)
        {
            this.LevelNumber = levelNumber;
            DefaultValues();
        }

        public Level(int levelNumber, Point playerStartingPoint, Point doorPoint, int levelHeight)
        {
            this.LevelNumber = levelNumber;
            this.PlayerStaringPoint = playerStartingPoint;
            this.DoorPoint = doorPoint;
            this.LevelHeight = levelHeight;
        }

        private void DefaultValues()
        {
            this.PlayerStaringPoint = new Point(40, 600);
            this.DoorPoint = new Point(1200, 600); //@TODO: Remove hard coded values
            this.LevelHeight = 600;
        }


    }
}
