using BrickBreaker.GameObjects;
using Microsoft.Xna.Framework;

namespace BrickBreaker.Scenes
{
    public class MenuScene : Scene
    {
        Background background;
        public override void Load(params object[] datas)
        {
            background = new Background(720, 1280);
            background.ChangeTargetScale("Menu");
            Add(background);

            Color textColor = Text.mainColor;
 
            Text playText = new Text(true, "Play", textColor, new Vector2(0, -50), 32);
            Add(playText);

            Text exitText = new Text(true, "Exit game", textColor, new Vector2(0, +50), 32);
            Add(exitText);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public override void unload()
        {
           
        }
    }
}
