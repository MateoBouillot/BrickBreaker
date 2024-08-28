using BrickBreaker.Scenes;
using BrickBreaker.Services;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BrickBreaker.GameObjects
{
    public class Background : SpriteGameObject
    {
        private bool _isZooming = false;
        public static float scale = 1;
        public static float targetScale;

        public Background(int windowHeight, int windowWidth) : base(ServiceLocator.Get<IAssetsService>().Get<Texture2D>("background"), new Vector2(windowWidth * .5f, windowHeight * .5f))
        {
            _speed = 1f;
        }

        public override void Update(float dt)
        {
            ManageScale(dt);
            _scale = scale;
        }

        public void ChangeTargetScale(string currentScene) // une fonction pour definir la scale en fonction de la scene
        {
            if (currentScene == "Menu")
                targetScale = 1f;
            else if (currentScene == "Game")
                targetScale = 1.5f;
            else if (currentScene == "Game Over")
                targetScale = 1.25f;
        }

        private void ManageScale(float dt) // une pour y faire augmenter la scale et faire une meilleure transition
        {
            if (scale <= targetScale + 0.05f && scale >= targetScale - 0.05f)
                scale = targetScale;
            else if (scale < targetScale - 0.05f)
                scale += scale * _speed * dt;
            else if (scale > targetScale + 0.05f)
                scale -= scale * _speed * dt;
        }

        
    }
}
