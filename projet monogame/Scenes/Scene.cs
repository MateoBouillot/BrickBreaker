using BrickBreaker.GameObjects;
using BrickBreaker.Services;
using System.Collections.Generic;

namespace BrickBreaker.Scenes
{
    public abstract class Scene
    {
        public static List<GameObject> gameObjects = new List<GameObject>();

        public virtual void Load(params object[] datas)
        { 
        }
        public virtual void unload() { }

        public virtual void Update(float dt)
        {
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                gameObjects[i].Update(dt);
            }

            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                if (gameObjects[i].isFree)
                {
                    gameObjects.RemoveAt(i);
                }
            }

            for (int i = LevelsManager.bricksList.Count - 1; i >= 0; i--)
            {
                if (LevelsManager.bricksList[i].isFree)
                    LevelsManager.bricksList.RemoveAt(i);
            }
        }

        public virtual void Draw()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw();
            }
        }

        public void Add(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

    }
}
