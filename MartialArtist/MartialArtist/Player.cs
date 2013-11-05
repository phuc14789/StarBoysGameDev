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
        float time;

        //Animation State
        ActionState curAction = ActionState.Standing;
        enum ActionState
        {
            Standing,
            SlashDownSkill,
        }

        public Player(Texture2D player, Vector2 position, int health, int life, int currentFrame, int rows, int columns,float delay,float scale) : base(player ,position, currentFrame ,rows ,columns ,delay,scale )
        {
            this.health = health;
            this.life = life;

            textureData = new Color[player.Width  * player .Height];
            player.GetData(textureData);
        }

        public override void Update(GameTime gameTime, ContentManager Content)
        {
            characterControl(gameTime, Content);      
        }

        public void characterControl(GameTime gameTime,ContentManager Content)
        {
            KeyboardState key = Keyboard.GetState();
            KeyboardState preKey = Keyboard.GetState();

            //Move to left or right
            if (curAction == ActionState.Standing)
            {
                if (key.IsKeyDown(Keys.D))
                {
                    Walk(Content);
                    velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    moveFrame(gameTime);
                    animationCharacter();
                    flip = SpriteEffects.None;
                }
                else if (key.IsKeyDown(Keys.A))
                {
                    Walk(Content);
                    velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    moveFrame(gameTime);
                    animationCharacter();
                    flip = SpriteEffects.FlipHorizontally;

                }
                else
                {
                    _i_currentFrame = 0;
                    velocity.X = 0;
                    Standing(Content);
                }
            }

            //Slash DOWN ACTION
            if (key.IsKeyDown(Keys.J))
            {
                curAction = ActionState.SlashDownSkill;
            }

            if (curAction == ActionState.SlashDownSkill)
            {
                time += (float)gameTime.ElapsedGameTime.Milliseconds;
                SlashDownSkill(Content);
                if (time >= _f_delay)
                {
                    if (_i_currentFrame < _i_totalFrame - 1)
                    {
                        _i_currentFrame++;
                    }
                    else
                    {
                        _i_currentFrame = 0;
                        Standing(Content);
                        curAction = ActionState.Standing;

                    }
                    time = 0;
                }
                animationCharacter();

            }

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

        public void SlashDownSkill(ContentManager Content)
        {

            _t_Image = Content.Load<Texture2D>("Images/Player/Player_SlashDown");
            _i_Rows = 3;
            _i_Columns = 4;
            _f_delay = 40f;
            calculateFrame();
        }

        public void Standing(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_Standing");
            _i_Rows = 2;
            _i_Columns = 4;
            _f_delay = 50f;
            calculateFrame();
        }

        public void Walk(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_Walk");
            _i_Rows = 3;
            _i_Columns = 5;
            _f_delay = 50f;
            calculateFrame();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
