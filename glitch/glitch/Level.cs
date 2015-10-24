using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace glitch
{
    class Level
    {

        public Point PlayerStaringPoint { get; set; }
        public Point DoorPoint { get; set; }
        public int LevelGroundLevel { get; set; }
        public int LevelNumber { get; set; }
        public GameObject Door { get; set; }
        public List<GameObject> LevelGroundObjects { get; set; }



        public Level(int levelNumber, Texture2D doorContent)
        {
            this.LevelNumber = levelNumber;
            DefaultValues();
            LevelGroundObjects = new List<GameObject>();
            this.Door = new GameObject(DoorPoint, doorContent, true, PhysicsType.StaticObject);
        }

        public Level(int levelNumber, Point playerStartingPoint, Point doorPoint, int levelHeight, Texture2D doorContent)
        {
            this.LevelNumber = levelNumber;
            this.PlayerStaringPoint = playerStartingPoint;
            this.DoorPoint = doorPoint;
            this.LevelGroundLevel = levelHeight;
            LevelGroundObjects = new List<GameObject>();

            this.Door = new GameObject(DoorPoint, doorContent, true, PhysicsType.StaticObject);
        }

        private void DefaultValues()
        {
            LevelGroundObjects = new List<GameObject>();
            this.PlayerStaringPoint = new Point(40, 600);
            this.DoorPoint = new Point(1200, 600); //@TODO: Remove hard coded values
            this.LevelGroundLevel = 600;
        }

        public void AddGroundObject(Point groundPoint, Texture2D texture, bool isVisible)
        {
            LevelGroundObjects.Add(new GameObject(groundPoint, texture, isVisible, PhysicsType.StaticObject));
        }

        public void RenderLevel(SpriteBatch spriteBatch)
        {
            this.Door.Render(spriteBatch);

            foreach (GameObject gameObject in this.LevelGroundObjects)
            {
                gameObject.Render(spriteBatch);
            }
        }


    }
}
