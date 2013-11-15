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

    public enum ActionState
    {
        Standing,
        Skill1,//Phim J
        Skill2,//Phim K
        Skill3,//Phim L
        Combo1,//Phim U
        Combo2,//Phim I
        Combo3,//Phim O
    }

    public class Player : Animation
    {
        public int health;
        public int damge;
        int skillCombo;
        public Color[] textureData;

        //Varible for control character
        const float gravity = 50f;
        Vector2 velocity;
        float moveSpeed = 500f;
        float jumpSpeed = 1000f;
        bool jump = false;
        float time;

        //Health Bar for Player
        public Texture2D healthbar;     
        public Rectangle rectHealthBar;        
        public int curHealth;
        float healthPercentage;
        float visibleHealth;

        //Combo Bar
        public Texture2D comboBar;
        public Rectangle rectComboBar;
        public int curCombo;
        float comboPercentage;
        float visibleCombo;

        //Combo
        public int combo = 99;//test so combo

        KeyboardState key;
        //Animation State
        public ActionState curAction = ActionState.Standing;

        // Trạng thái chu7ava chạm
        public bool flagCollection = false;

        float timer_Skill = 0f;

        public Rectangle f_Rectangle_srcPlayer(Vector2 position)
        {
            return new Rectangle((int)position.X, (int)position.Y, 400, 400);
        }

        public Player(Texture2D player,ContentManager Content, Vector2 position, int health, int damge, int skillCombo, int currentFrame, int rows, int columns, float delay, float scale)
            : base(player, position, currentFrame, rows, columns, delay, scale)
        {
            this.health = health;
            this.skillCombo = skillCombo;
            this.damge = damge;

            //Health of enemy
            curHealth = health;
            curCombo = skillCombo;
            healthbar = Content.Load<Texture2D>("Images/HealthBar/mau");
            comboBar = Content.Load<Texture2D>("Images/HealthBar/combo");
            
        }

        public override void Update(GameTime gameTime, ContentManager Content)
        {         

            //Calculate health of enemy
            healthPercentage = (float)curHealth / (float)health;
            visibleHealth = healthbar.Width * healthPercentage;
            rectHealthBar = new Rectangle(0, 0, (int)visibleHealth, healthbar.Height);  
            
            //Calculate Skill Combo
            comboPercentage = (float)curCombo / (float)skillCombo;
            visibleCombo = comboBar.Width * comboPercentage;
            rectComboBar = new Rectangle(0, 0, (int)visibleCombo, comboBar.Height);

            characterControl(gameTime, Content);

        }

        public void characterControl(GameTime gameTime, ContentManager Content)
        {
            key = Keyboard.GetState();

            timer_Skill += (float)gameTime.ElapsedGameTime.Milliseconds;

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
                    //flip = SpriteEffects.None;
                }
            }

            //SlashDownSkill //SlashDownCombo
            if (key.IsKeyDown(Keys.J))
            {
                if (timer_Skill > 00)
                {
                    curAction = ActionState.Skill1;
                }
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
                        if (curAction == ActionState.Standing)//dieu kien 
                        {
                            curCombo = curCombo + 5;// de tru combo
                            timer_Skill = 0f;
                        }
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
                        if (curAction == ActionState.Standing)//dieu kien 
                            curCombo = curCombo + 5;// de tru combo
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
                        if (curAction == ActionState.Standing)//dieu kien 
                            curCombo = curCombo + 5;// de tru combo
                    }
                    time = 0;
                }
                animationCharacter();
            }


            //Combo1
            if (key.IsKeyDown(Keys.U) && curCombo >= 20)
            {
                curAction = ActionState.Combo1;
            }

            if (curAction == ActionState.Combo1)
            {
                time += (float)gameTime.ElapsedGameTime.Milliseconds;
                SlashDownCombo(Content);//JumpSlashCombo
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
                        if (curAction == ActionState.Standing)//dieu kien 
                            curCombo = curCombo - 20;// de tru combo
                    }
                    time = 0;
                }
                animationCharacter();
            }

            //Combo2
            if (key.IsKeyDown(Keys.I) && curCombo >= 40)
            {
                curAction = ActionState.Combo2;
            }

            if (curAction == ActionState.Combo2)
            {
                time += (float)gameTime.ElapsedGameTime.Milliseconds;
                SlashDownCombo(Content);//SlashDownCombo
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
                        if (curAction == ActionState.Standing)//dieu kien 
                            curCombo = curCombo - 40;// de tru combo
                    }
                    time = 0;
                }
                animationCharacter();
            }

            //Combo3
            if (key.IsKeyDown(Keys.O) && curCombo >= 60)
            {
                curAction = ActionState.Combo3;
            }

            if (curAction == ActionState.Combo3)
            {
                time += (float)gameTime.ElapsedGameTime.Milliseconds;
                JumpSlashCombo(Content);//SlashDownCombo
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
                        if (curAction == ActionState.Standing)//dieu kien 
                            curCombo = curCombo - 60;// de tru combo
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