using BrickBreaker.Scenes;
using BrickBreaker.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker.GameObjects
{
    public class Text : GameObject
    {
        private bool _isClickable;
        private bool _isHover = false;
        private bool _isClick = false;
        private bool _wasClicking = false;

        private string _text;
        private Color _color;
        private SpriteFont _font;

        private Vector2 _position;
        private Vector2 _centeredPosition;
        private Vector2 _origin;

        private Vector2 _textSize;

        private float _scale = 1f;
        private float _targetScale = 1f;
        private float _speed;

        public static Color mainColor = new Color(89, 255, 191);

        public Text (bool isClickable, string text, Color color, Vector2 offsetFromCenter, int fontSize) : base ()
        {
            _isClickable = isClickable;
            _text = text;
            _color = color;
            _font = ServiceLocator.Get<IAssetsService>().Get<SpriteFont>($"Font{fontSize}");
            _centeredPosition = new Vector2(1280 * .5f, 720 * .5f);
            _position = _centeredPosition + offsetFromCenter;
            _speed = 2f;
            _origin = _font.MeasureString(_text) * .5f;
        }

        public override void Update(float dt)
        {
            MouseState mouseState = Mouse.GetState();
            if (_isClickable) 
                CheckMouseState(mouseState);
            ManageScale(dt);
        }

        public override void Draw()
        {
            ServiceLocator.Get<SpriteBatch>().DrawString(_font, _text, _position, _color, 0f, _origin, _scale, SpriteEffects.None, 0f);
        }

        // cette fonction me permet de lancer les evenements lier a la souris pour les boutons du menu
        private void CheckMouseState(MouseState mouseState)
        {
            if (mouseState.Position.X <= _position.X + _origin.X 
                && mouseState.Position.X >= _position.X - _origin.X
                && mouseState.Position.Y <= _position.Y + _origin.Y
                && mouseState.Position.Y >= _position.Y - _origin.Y)
            {
                _isHover = true;
                HoverManager();

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    _isClick = true;
                    ClickManager();
                    _wasClicking = _isClick;
                }
            } 
            else if (mouseState.Position.X > _position.X + _origin.X
                || mouseState.Position.X < _position.X - _origin.X
                || mouseState.Position.Y > _position.Y + _origin.Y
                || mouseState.Position.Y < _position.Y - _origin.Y)
            {
                _isHover = false;
                HoverManager();
            }

            if (mouseState.LeftButton != ButtonState.Pressed)
            {
                _isClick = false;
                ClickManager();
                _wasClicking = _isClick;
            }
        }

        private void HoverManager() // le text grossit quand la souris passe dessus
        {
            if (_isHover)
            {
                _targetScale = 1.1f;
            } 
            else if (!_isHover)
            {
                if (!_isClick)
                    _targetScale = 1f;
            }
        }

        private void ClickManager() // et rapetisse lorsque l'on clique
        {
            if (!_isClick)
            {
                if (_isHover)
                {
                    _targetScale = 1.1f;
                    if (_wasClicking)
                        if (_text == "Play")
                            ServiceLocator.Get<IScenesManager>().Load<GameScene>();
                        else if (_text == "Exit game")
                            ServiceLocator.Get<Main>().Exit();
                }
                else if (!_isHover)
                    _targetScale = 1f;
            }
            else if (_isClick)
            {
                _targetScale = 0.9f;
            }
        }

        private void ManageScale(float dt) // pour plus de fluidité j'ai utilisé une scale et une targetScale
        {
            if (_scale <= _targetScale + 0.02f && _scale >= _targetScale - 0.02f)
                _scale = _targetScale;
            else if (_scale < _targetScale - 0.02f)
                _scale += _scale * _speed * dt;
            else if (_scale > _targetScale + 0.02f)
                _scale -= _scale * _speed * dt;
        }
    }
}
