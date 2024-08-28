using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BrickBreaker.GameObjects;

namespace BrickBreaker.Scenes
{
    public class GameOverScene : Scene
    {
        private SpriteFont _font24;
        private SpriteFont _font32;
        Background background;

        private Color _textColor;
        public override void Load(params object[] datas)
        {
            _textColor = Text.mainColor;

            background = new Background(720, 1280);
            background.ChangeTargetScale("Game Over");
            Add(background);

            Text gameOverText = new Text(false, "Game Over", _textColor, new Vector2(0, -100), 32);
            Add(gameOverText);

            Text enterText = new Text(false, "Press enter to restart", _textColor, Vector2.Zero, 24);
            Add(enterText);

            Text escapeText = new Text(false, "Press escape to go back to the menu", _textColor, new Vector2(0, 100), 24);
            Add( escapeText);
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
                ServiceLocator.Get<IScenesManager>().Load<GameScene>();
            else if (keyboardState.IsKeyDown(Keys.Escape))
                ServiceLocator.Get<IScenesManager>().Load<MenuScene>();
        }
        
    }
}
