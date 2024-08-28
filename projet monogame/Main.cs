using BrickBreaker.Scenes;
using BrickBreaker.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreaker
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ScenesManager _ScenesManager;
        private AssetsService _assetsService;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ServiceLocator.Register<ContentManager>(Content);
            ServiceLocator.Register<Main>(this);
            
            _ScenesManager = new ScenesManager();
            _ScenesManager.Register<MenuScene>();
            _ScenesManager.Register<GameScene>();
            _ScenesManager.Register<GameOverScene>();

            base.Initialize();
        } 

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ServiceLocator.Register<SpriteBatch>(_spriteBatch);
            _assetsService = new AssetsService(Content);

            _assetsService.Load<Texture2D>("ballBlack");
            _assetsService.Load<Texture2D>("ballRed");
            _assetsService.Load<Texture2D>("pad");
            _assetsService.Load<Texture2D>("tileBlack");
            _assetsService.Load<Texture2D>("tileRed");
            _assetsService.Load<Texture2D>("background");
            _assetsService.Load<SpriteFont>("Font24");
            _assetsService.Load<SpriteFont>("Font32");

            _ScenesManager.Load<MenuScene>();
        }

        protected override void Update(GameTime gameTime)
        { 
            _ScenesManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _ScenesManager.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
