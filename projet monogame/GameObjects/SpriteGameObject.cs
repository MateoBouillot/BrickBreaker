using BrickBreaker.Scenes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BrickBreaker.GameObjects
{
    public class SpriteGameObject : GameObject
    {
        // declaration de toute mes variables communes a la plupart des objet
        public Vector2 position = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        protected float _speed = 0f;

        public Rectangle bounds = new Rectangle(0, 0, 1280, 720);

        protected Color _color = Color.White;
        protected float _rotation = 0f;

        public Vector2 origin = Vector2.Zero;
        public float offsetX;
        public float offsetY;
        public float offset;

        protected float _scale = 1f;

        protected Texture2D _texture;

        public SpriteGameObject(Texture2D texture, Vector2 position) : base ()
        {
            _texture = texture;

            this.position = position;

            GetOffset();
            origin = new Vector2(offsetX, offsetY);
        }

        public override void Update(float dt)
        {
            if (!enable) return;
            position += velocity * dt;
        }

        public override void Draw()
        {
            if (!enable) return;
            if (_texture == null) return;

            ServiceLocator.Get<SpriteBatch>().Draw(_texture, position, null, _color, _rotation, origin, _scale, SpriteEffects.None, 0f);
        }

        private void GetOffset() // calcul du centre de la texture
        {
            offsetX = _texture.Width * .5f;
            offsetY = _texture.Height * .5f;

            if (offsetX == offsetY)
                offset = offsetX;
        }
    }
}
