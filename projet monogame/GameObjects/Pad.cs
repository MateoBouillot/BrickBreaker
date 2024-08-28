using Microsoft.Xna.Framework;
using BrickBreaker.Scenes;
using BrickBreaker.Services;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker.GameObjects
{
    public class Pad : SpriteGameObject
    {
        private Vector2 _targetPosition;
        public Vector2 lastPosition;

        public Pad() : base(ServiceLocator.Get<IAssetsService>().Get<Texture2D>("pad"), Vector2.Zero)
        {
            _speed = 500f;

            position = new Vector2(bounds.Center.X, 720 - offsetY);
            _targetPosition = position;
        }

        public override void Update(float dt)
        {
            MovePad(dt);
        }

        public void MovePad(float dt)
        {
            lastPosition = position;

            KeyboardState KeyboardState = Keyboard.GetState();
            if (KeyboardState.IsKeyDown(Keys.Left))
                _targetPosition.X -= _speed * dt;
            if (KeyboardState.IsKeyDown(Keys.Right))
                _targetPosition.X += _speed * dt;

            _targetPosition = Vector2.Clamp(_targetPosition, new Vector2(bounds.Left + offsetX, position.Y), new Vector2(bounds.Right - offsetX, position.Y));
            position = Vector2.Lerp(position, _targetPosition, 0.2f);

            velocity = (position - lastPosition) / dt;
        }
    }
}
