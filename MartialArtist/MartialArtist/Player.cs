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

        //Varible for control character
        const float gravity = 50f;
        Vector2 velocity;
        float moveSpeed = 500f;
        float jumpSpeed = 1000f;
        bool jump = false;

        public Player(Texture2D player, Vector2 position, int health, int life, int currentFrame, int rows, int columns,float delay,float scale) : base(player ,position, currentFrame ,rows ,columns ,delay,scale )
        {
            this.health = health;
            this.life = life;

            textureData = new Color[_i_width * _i_heigth];
            player.GetData(textureData);
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

            //Move to left or right
            if (key.IsKeyDown(Keys.D))
            {
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (key.IsKeyDown(Keys.A))
            {
                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
                velocity.X = 0;

            //Jump
            if (key.IsKeyDown(Keys.W) && jump)
            {
                velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                jump = false;
            }
            if (!jump)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;

            _vt2_position += velocity;
            jump = _vt2_position.Y >= 200;
            if (jump)
                _vt2_position.Y = 200;
        
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
