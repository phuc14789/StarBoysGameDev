using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MartialArtist
{
    public class Player : Animation
    {
        int health;
        int life;
        public Color[] textureData;
        double vi, t = 0; 
        double g = 5000;
        int keyState = 0;

        public Player(Texture2D enemy, Vector2 position, int health, int life, int currentFrame, int rows, int columns,float delay,float scale) : base(enemy ,position, currentFrame ,rows ,columns ,delay,scale )
        {
            this.health = health;
            this.life = life;

            textureData = new Color[enemy.Width * enemy.Height];
            enemy.GetData(textureData);
        }

        public void Initialize()
        {
            // TODO: Add your initialization code here

        }

        public override void Update(GameTime gameTime)
        {
            characterControl(gameTime);
            base.Update(gameTime);
        }

        public void characterControl(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                keyState = 1; vi = -2020;
            }

            if (keyState == 1)
            {
                // the normal formula is: vi * t - g * t^2 / 2 and vi is positive, but here UP means decrease Y and DOWN means increase Y
                // that's why it looks strange
                _vt2_position.Y = (float)(vi * t + g * t * t / 2) + Global.screenHeight - _t_Image.Height;
                t = t + gameTime.ElapsedGameTime.TotalSeconds; // calculate the time since the ball has left the ground
            }

            if (_vt2_position.Y > Global.screenHeight - _t_Image.Height) // Don't allow the ball to go beyond the bottom side of the window
            {
                _vt2_position.Y = Global.screenHeight - _t_Image.Height;
                keyState = 0;
                t = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _vt2_position.X -= 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _vt2_position.X += 5;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
