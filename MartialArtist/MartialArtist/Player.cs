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
        public int health;
        int life;
        public Color[] textureData;

        //Varible for control character
        const float gravity = 50f;
        Vector2 velocity;
        float moveSpeed = 500f;
        float jumpSpeed = 1000f;
        bool jump = false;
        float time;


        //time

        KeyboardState key;
        //Animation State
        ActionState curAction = ActionState.Standing;
        enum ActionState
        {
            Standing,
            Skill1,//Phim J
            Skill2,//Phim K
            Skill3,//Phim L
        }

        public Player(Texture2D player, Vector2 position, int health, int life, int currentFrame, int rows, int columns, float delay, float scale)
            : base(player, position, currentFrame, rows, columns, delay, scale)
        {
            this.health = health;
            this.life = life;

            textureData = new Color[player.Width * player.Height];
            player.GetData(textureData);
        }

        public override void Update(GameTime gameTime, ContentManager Content)
        {
            characterControl(gameTime, Content);
        }

        public void characterControl(GameTime gameTime, ContentManager Content)
        {
            key = Keyboard.GetState();

            //Move to left or right
            if (curAction == ActionState.Standing)
            {
                if (key.IsKeyDown(Keys.D))
                {
                    if (key.IsKeyDown(Keys.D) && key.IsKeyDown(Keys.LeftShift))
                    {
                        Run(Content);
                        velocity.X = 2 * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        moveFrame(gameTime);
                        animationCharacter();
                        flip = SpriteEffects.None;
                    }
                    else
                    {
                        Walk(Content);
                        velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        moveFrame(gameTime);
                        animationCharacter();
                        flip = SpriteEffects.None;
                    }
                }

                else if (key.IsKeyDown(Keys.A))
                {
                    if (key.IsKeyDown(Keys.A) && key.IsKeyDown(Keys.LeftShift))
                    {
                        Run(Content);
                        velocity.X = 2 * -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        moveFrame(gameTime);
                        animationCharacter();
                        flip = SpriteEffects.FlipHorizontally;
                    }
                    else
                    {
                        Walk(Content);
                        velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        moveFrame(gameTime);
                        animationCharacter();
                        flip = SpriteEffects.FlipHorizontally;
                    }
                }
                else
                {
                    velocity.X = 0;
                    Standing(Content);
                    moveFrame(gameTime);
                    animationCharacter();
                    flip = SpriteEffects.None;
                }
            }

            //SlashDownSkill //SlashDownCombo
            if (key.IsKeyDown(Keys.J))
            {
                curAction = ActionState.Skill1;
            }

            if (curAction == ActionState.Skill1)
            {
                time += (float)gameTime.ElapsedGameTime.Milliseconds;
                SlashDownSkill(Content);//SlashDownSkill //SlashDownCombo
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

            //SlashUpSkill //SlashUpCombo
            if (key.IsKeyDown(Keys.K))
            {
                curAction = ActionState.Skill2;
            }

            if (curAction == ActionState.Skill2)
            {
                time += (float)gameTime.ElapsedGameTime.Milliseconds;
                SlashUpSkill(Content); //SlashUpSkill //SlashUpCombo
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


            //JumpSlashSkill //JumpSlashCombo
            if (key.IsKeyDown(Keys.L))
            {
                curAction = ActionState.Skill3;
            }

            if (curAction == ActionState.Skill3)
            {
                time += (float)gameTime.ElapsedGameTime.Milliseconds;
                JumpSlashSkill(Content);//JumpSlashSkill //JumpSlashCombo
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
        /// <summary>
        /// Fall: va cham voi enemy bi te'
        /// Run: chay
        /// 3 skill thuong
        /// 3 combo
        /// </summary>
        /// <param name="Content">Load hinh anh</param>
        public void Fall(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_Fall");
            _i_Rows = 3;
            _i_Columns = 5;
            _f_delay = 40f;
            calculateFrame();
        }

        public void Run(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_Run");
            _i_Rows = 1;
            _i_Columns = 6;
            _f_delay = 40f;
            calculateFrame();
        }

        public void SlashDownSkill(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_SlashDown_p1");
            _i_Rows = 1;
            _i_Columns = 6;
            _f_delay = 60f;
            calculateFrame();
        }
        public void SlashDownCombo(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_SlashUpDown");
            _i_Rows = 3;
            _i_Columns = 4;
            _f_delay = 60f;
            calculateFrame();
        }
        public void SlashUpSkill(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_SlashUp_p1");
            _i_Rows = 1;
            _i_Columns = 4;
            _f_delay = 60f;
            calculateFrame();
        }
        public void SlashUpCombo(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_SlashUp_F");
            _i_Rows = 2;
            _i_Columns = 4;
            _f_delay = 60f;
            calculateFrame();
        }
        public void JumpSlashSkill(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_JumpSlashDown_p1");
            _i_Rows = 1;
            _i_Columns = 3;
            _f_delay = 100f;
            calculateFrame();
        }
        public void JumpSlashCombo(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Player/Player_JumpSlashDown_F");
            _i_Rows = 3;
            _i_Columns = 3;
            _f_delay = 80f;
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