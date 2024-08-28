using BrickBreaker.Scenes;
using BrickBreaker.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreaker.GameObjects
{
    public class Brick : SpriteGameObject
    {
        public bool isBlack;
        private Color _turnOffColor = new Color(255, 255, 255, 100);
        public Brick(Vector2 position, string brickType, string brickColor) : base(ServiceLocator.Get<IAssetsService>().Get<Texture2D>("tileBlack"), position)
        {
            if (brickColor == "red")
            {
                isBlack = false;
                _texture = ServiceLocator.Get<IAssetsService>().Get<Texture2D>("tileRed");
            }
            else if (brickColor == "black")
                isBlack = true;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            CheckIfColorMatch();
        }

        private void CheckIfColorMatch() // si la brick ne match pas la couleur de la balle elle apparait alors transparant
        {
            if (isBlack != Ball.isBlack)
                _color = _turnOffColor;
            else if (isBlack == Ball.isBlack)
                _color = Color.White;
        }
    }
}
