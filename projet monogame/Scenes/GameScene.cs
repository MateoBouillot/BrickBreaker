using BrickBreaker.GameObjects;
using BrickBreaker.Services;

namespace BrickBreaker.Scenes
{
    public class GameScene : Scene
    {
        LevelsManager levelsManager;
        Background background;

        public override void Load(params object[] datas)
        {
            levelsManager = new LevelsManager();
            levelsManager.LoadNewLevel();
        }

        public override void Update(float dt)
        {
            levelsManager.CheckIfNewLevel();
            levelsManager.CheckIfGameOver();
            levelsManager.SelectLevel();
            base.Update(dt);
        }
    }
}
