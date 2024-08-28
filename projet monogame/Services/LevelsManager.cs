using BrickBreaker.GameObjects;
using BrickBreaker.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BrickBreaker.Services
{
    public class BrickData
    {
        public int X;
        public int Y;
        public string brickType;
        public string brickColor;
    }

    public class Level
    {
        public int LevelNumber;
        public List<BrickData> bricks;
    }

    public class Data
    {
        public List<Level> levels;
    }

    public class LevelsManager
    {
        private int _currentLevel;
        public static List<Brick> bricksList { get; private set; } = new List<Brick>();
        Data data;

        public LevelsManager()
        {
            _currentLevel = 1;
            GetData(); 
        }

        private void GetData() // recuperation des briques et des differents niveau depuis un JSON
        {
            var json = File.ReadAllText("../../../BricksData/Levels.json");
            data = JsonConvert.DeserializeObject<Data>(json);
        }

        public void Update()
        {
            CheckIfGameOver();
            CheckIfNewLevel();
            SelectLevel();
        }

        public void CheckIfGameOver()
        {
            if (!Scene.gameObjects.OfType<Ball>().Any())
            {
                Scene.gameObjects.Clear();
                bricksList.Clear();
                ServiceLocator.Get<IScenesManager>().Load<GameOverScene>();
            }
        }

        public void CheckIfNewLevel()
        {
            if (bricksList.Count == 0)
            {
                _currentLevel++;
                Scene.gameObjects.Clear();
                LoadNewLevel();
            }
        }

        // juste une petite fonction pour permettre de démontrer plus facilement les différents niveaux
        private bool _keyAlreadyPressed = false;
        public void SelectLevel()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Add) && !_keyAlreadyPressed && _currentLevel < data.levels.Count)
            {
                _keyAlreadyPressed = true;
                _currentLevel++;
                Scene.gameObjects.Clear();
                LoadNewLevel();
            }
            else if (keyboardState.IsKeyDown(Keys.Subtract) && !_keyAlreadyPressed && _currentLevel > 1)
            {
                _keyAlreadyPressed = true;
                _currentLevel--;
                Scene.gameObjects.Clear();
                LoadNewLevel();
            }
            else if (keyboardState.IsKeyUp(Keys.Subtract) && keyboardState.IsKeyUp(Keys.Add))
                _keyAlreadyPressed = false;
        }

        public void LoadNewLevel()
        {
            // mise en place du background pour quand on arrive du menu
            Background background = new Background(720, 1280);
            background.ChangeTargetScale("Game");
            Scene.gameObjects.Add(background);

            Pad pad = new Pad();
            Scene.gameObjects.Add(pad);

            Ball ball = new Ball(pad);
            Scene.gameObjects.Add(ball);

            LoadBricks();
        }

        private void LoadBricks() // chargement des bricks nécessaire au niveau actuel
        {
            foreach (var BrickData in data.levels[_currentLevel - 1].bricks)
            {
                Vector2 position = new Vector2(BrickData.X, BrickData.Y);
                Brick brick = new Brick(position, BrickData.brickType, BrickData.brickColor);

                bricksList.Add(brick);
                Scene.gameObjects.Add(brick);
            }
        }
    }
}
