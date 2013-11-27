using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MartialArtist
{
    public class Boss:Animation
    {

        // Máu của Enemy
        private int _i_Health;

        public int curHealth;
        Texture2D healthbar;
        Rectangle rectHealthBar;
        float healthPercentage;
        float visibleHealth;

        public Boss(Texture2D boss, ContentManager Content, Vector2 position, int health, int damage, int currentFrame, int rows, int columns, float delay, float scale)
            : base(boss, position, currentFrame, rows, columns, delay, scale)           
        {
            _i_Health = health;
            curHealth = health;
            healthbar = Content.Load<Texture2D>("Images/Enemy/healthBar");
  
        }

        public override void Update(GameTime gameTime, ContentManager Content)
        {
            //Calculate health of enemy
            healthPercentage = (float)curHealth / (float)_i_Health;
            visibleHealth = healthbar.Width * healthPercentage;
            rectHealthBar = new Rectangle(0, 0, (int)visibleHealth, healthbar.Height);


            //f_MoveAroundPlayer(gameTime, Content, X, Y);
            //f_Boss(gameTime);

            //base.Update(gameTime, Content);
        }


        int _f_Speed = 1;

        public void f_MoveAroundPlayer(GameTime gameTime, ContentManager Content, int X, int Y)
        {
            // Tạo tốc độ cho Enemy         

            if (_vt2_position.X + 220 < X)       // 60 là gần đến với vị trí player
            {
                _vt2_position.X += _f_Speed;
                flip = SpriteEffects.None;

                moveFrame(gameTime);
                animationCharacter();
            }
            else
                if (_vt2_position.X - 130 > X)   // 60 là gần đến với vị trí player
                {
                    _vt2_position.X -= _f_Speed;
                    flip = SpriteEffects.FlipHorizontally;
                    moveFrame(gameTime);
                    animationCharacter();
                }
        }


        public void f_BossDie(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Enemy/Boss/Boss_die");
            _i_Rows = 1;
            _i_Columns = 6;
            _f_delay = 100f;
            calculateFrame();
        }

        public void f_BossFall(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Enemy/Boss/Boss_fall");
            _i_Rows = 1;
            _i_Columns = 2;
            _f_delay = 100f;
            calculateFrame();
        }

        public void f_BossHack01(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Enemy/Boss/Boss_hack_01");
            _i_Rows = 1;
            _i_Columns = 4;
            _f_delay = 100f;
            
            calculateFrame();
        }

        public void f_BossHack02(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Enemy/Boss/Boss_hack_02");
            _i_Rows = 2;
            _i_Columns = 4;
            _f_delay = 100f;
            calculateFrame();
        }

        public void f_BossWalk(ContentManager Content)
        {
            _t_Image = Content.Load<Texture2D>("Images/Enemy/Boss/Boss_walk");
            _i_Rows = 3;
            _i_Columns = 4;
            _f_delay = 100f;
            calculateFrame();
        }

        public Rectangle f_Rectangle_srcBoss(Vector2 position)
        {
            return new Rectangle((int)position.X, (int)position.Y, 350, 250);
        }


        /// <summary>
        /// Dùng cho khi player va chạm ( Cái này dùng để vẽ ra 1 cái khung nhỏ ) 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Rectangle f_Rectangle_srcBoss_Player(Vector2 position)
        {
            return new Rectangle((int)position.X, (int)position.Y, 150, 250);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw Health Bar of Enemy
            spriteBatch.Draw(healthbar, _vt2_position + new Vector2(240, 135), rectHealthBar, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }
    }
}
