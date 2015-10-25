using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace glitch
{
    public class Level
    {

        public Point PlayerStaringPoint { get; set; }
        public Point DoorPoint { get; set; }
        public int LevelGroundLevel { get; set; }
        public int LevelNumber { get; set; }
        public GameObject Door { get; set; }
        public List<GameObject> LevelObjects { get; set; }
        public GameObject TitleFlicker { get; set; }



        public Level(int levelNumber, Texture2D doorContent)
        {
            this.LevelNumber = levelNumber;
            DefaultValues();
            LevelObjects = new List<GameObject>();
            this.Door = new GameObject(DoorPoint.ToVector2(), doorContent, true, PhysicsType.StaticObject);
            this.Door.Size = new Point(30, 60);

        }

        public Level(int levelNumber, Point playerStartingPoint, Point doorPoint, int levelHeight, Texture2D doorContent)
        {
            this.LevelNumber = levelNumber;
            this.PlayerStaringPoint = playerStartingPoint;
            this.DoorPoint = doorPoint;
            this.LevelGroundLevel = levelHeight;
            LevelObjects = new List<GameObject>();

            this.Door = new GameObject(DoorPoint.ToVector2(), doorContent, true, PhysicsType.StaticObject);
            this.Door.Size = new Point(30, 60);
        }

        private void DefaultValues()
        {
            LevelObjects = new List<GameObject>();
            this.PlayerStaringPoint = new Point(40, 600);
            this.DoorPoint = new Point(1200, 600); //@TODO: Remove hard coded values
            this.LevelGroundLevel = 600;
        }

        public void AddObject(Point groundPoint, Point assetSize, Texture2D texture, bool isVisible)
        {
            GameObject tempGameObject = new GameObject(groundPoint.ToVector2(), texture, isVisible, PhysicsType.StaticObject);
            tempGameObject.Size = assetSize;
            LevelObjects.Add(tempGameObject);
            tempGameObject = null;
        }

        public void AddObject(Point groundPoint, Texture2D texture, bool isVisible)
        {
            GameObject tempGameObject = new GameObject(groundPoint.ToVector2(), texture, isVisible, PhysicsType.StaticObject);
            LevelObjects.Add(tempGameObject);
            tempGameObject = null;
        }

        public void RenderLevel(SpriteBatch spriteBatch)
        {
            this.Door.Render(spriteBatch);

            foreach (GameObject gameObject in this.LevelObjects)
            {
                gameObject.Render(spriteBatch);
            }
        }


    }
}
