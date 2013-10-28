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
        Vector2 _vt2_position;
        public Color[] textureData;
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
            characterControl();
            base.Update(gameTime);
        }

        public void characterControl()
        {
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.W))
            {
                _vt2_position.Y -= 3;
                if (_vt2_position.Y < 0)
                    _vt2_position.Y = 0;
            }
            if (key.IsKeyDown(Keys.S))
            {
                _vt2_position.Y += 3;
                if (_vt2_position.Y + _t_Image.Height > Global.screenHeight)
                    _vt2_position.Y = Global.screenHeight - _t_Image.Height;
            }
            if (key.IsKeyDown(Keys.A))
            {
                _vt2_position.X -= 3;
                if (_vt2_position.X < 0)
                    _vt2_position.X = 0;
            }
            if (key.IsKeyDown(Keys.D))
            {
                _vt2_position.X += 3;
                if (_vt2_position.X + _t_Image.Width > Global.screenWidth)
                    _vt2_position.X = Global.screenWidth - _t_Image.Width;

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
