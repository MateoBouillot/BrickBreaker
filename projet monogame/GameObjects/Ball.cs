using BrickBreaker.Scenes;
using BrickBreaker.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BrickBreaker.GameObjects
{
    public class Ball : SpriteGameObject
    {
        private Pad _pad;
        private bool _isSticky;

        public static bool isBlack;
        private Texture2D _textureBlack;
        private Texture2D _textureRed;

        public Ball(Pad pad) : base(ServiceLocator.Get<IAssetsService>().Get<Texture2D>("ballBlack"), pad.position - new Vector2(0, pad.offsetY))
        {
            _textureBlack = ServiceLocator.Get<IAssetsService>().Get<Texture2D>("ballBlack");
            _textureRed = ServiceLocator.Get<IAssetsService>().Get<Texture2D>("ballRed");
            isBlack = true;

            _pad = pad;

            _speed = 350f;
            velocity = Vector2.Zero;

            _isSticky = true;
        }

        public override void Update(float dt)
        {
            BoundsRebound();
            PadRebound();
            BrickCollision();
            Sticking();
            base.Update(dt);
        }

        public override void Draw()
        {
            if (isBlack)
                _texture = _textureBlack;
            else if (!isBlack)
                _texture = _textureRed;

            ServiceLocator.Get<SpriteBatch>().Draw(_texture, position, null, _color, _rotation, origin, _scale, SpriteEffects.None, 0f);
        }

        private void BoundsRebound()
        {
            if (position.X > bounds.Right - offset)
            {
                velocity.X *= -1;
                position.X = bounds.Right - offset;
            } 
            else if (position.X < bounds.Left + offset)
            {
                velocity.X *= -1;
                position.X = bounds.Left + offset;
            }

            if (position.Y > bounds.Bottom + offset)
            {
                isFree = true;
            }
            if (position.Y < bounds.Top + offset)
            {
                velocity.Y *= -1;
                position.Y = bounds.Top + offset;
            }
        }
        // je sépare la detection et la gestion de collision pour simplifier les choses
        private void PadRebound()
        {
            if (position.X > _pad.position.X - _pad.offsetX && position.X < _pad.position.X + _pad.offsetX)
                if (position.Y + offset > _pad.position.Y - _pad.offsetY && position.Y < _pad.position.Y && velocity.Y > 0)
                {
                    velocity.Y *= -1;
                    PadReboundDirection();
                    ColorSwap();
                }
        }

        private void PadReboundDirection() // en fonction du point de rebond sur le pad la balle prend une direction différente
        {                                  // cela permet au joueur de pouvoir viser un minimum
            if (velocity.X <= 0)
            {
                velocity.X = (((position.X - _pad.position.X) / _pad.offsetX) - .5f) * _speed;
            } 
            else if (velocity.X > 0)
            {
                velocity.X = (((position.X - _pad.position.X) / _pad.offsetX) + .5f) * _speed;
            }
        }

        private void BrickCollision()
        {
            foreach (Brick brick in LevelsManager.bricksList) 
            {
                if (brick.isBlack == isBlack)// calcul du point du rectangle brick le plus proche de la balle
                {
                    float closestX = MathF.Max(brick.position.X - brick.offsetX, MathF.Min(position.X, brick.position.X + brick.offsetX));
                    float closestY = MathF.Max(brick.position.Y - brick.offsetY, MathF.Min(position.Y, brick.position.Y + brick.offsetY));

                    // verification si ce point est a l'intérieur ou non du cercle balle

                    float distance = MathF.Sqrt((closestX - position.X) * (closestX - position.X) + (closestY - position.Y) * (closestY - position.Y));
                    if (distance <= offset)
                    {
                        BrickRebound(brick, closestX, closestY);
                        brick.isFree = true;
                        return;
                    }
                }
            }
            
        }

        private void BrickRebound(Brick brick, float closestX, float closestY) // l'emplacement du point le plus proche du rectangle definit la direction du rebond
        {
            if (closestX == brick.position.X - brick.offsetX || closestX == brick.position.X + brick.offsetX && velocity.X != 0)
            {
                velocity.X *= -1;
            }
            else if (closestY == brick.position.Y - brick.offsetY || closestY == brick.position.Y + brick.offsetY && velocity.Y != 0)
            {
                velocity.Y *= -1;
            }
                
            else velocity.Y *= -1;
        }

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        private void Sticking()
        {
            currentKeyboardState = Keyboard.GetState();

            if (_isSticky)
            {

                this.position = _pad.position - new Vector2(0, offset) - new Vector2(0, _pad.offsetY);

                if (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
                {
                    UnStick();
                }

            }
            previousKeyboardState = currentKeyboardState;  
        }

        private void UnStick()
        {
            velocity.Y = -1 * _speed;
            if (_pad.velocity.X > 0)
                velocity.X = 1 * _speed;
            else if (_pad.velocity.X < 0)
                velocity.X = -1 * _speed;

            _isSticky = false;
        }

        // en fonction de la position par rapport au pad lors du rebond
        // la balle change de couleur et pourras toucher les birck de cette couleur
        private void ColorSwap() 
        {
            if (position.X <= _pad.position.X)
                isBlack = false;
            else
                isBlack = true;
        }
    }
}
