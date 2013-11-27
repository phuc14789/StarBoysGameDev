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
    class Level3:Level
    {
         //Initialize Objects
        Game g;
        Texture2D background;
        Texture2D healthTexture;
        SoundEffect Hit;
        SoundEffectInstance HitInstance;
        SoundEffect Health;
        SoundEffectInstance HealthInstance;


        Effect effect;
        SpriteFont font;
        List<Effect> LiHearth;

        // Boss

        Boss boss;

        public Level3(Game g, ContentManager Content)
        {
            camera = new Camera(g.GraphicsDevice.Viewport);
            player = new Player(Content.Load<Texture2D>("Images/Player/Player_Standing"),g.Content , new Vector2(0, 0), 1000, 100 , 100, 0, 2, 4, 50f, 0.7f);
            font = g.Content.Load<SpriteFont>("Fonts/Arial");

            boss = new Boss(Content.Load<Texture2D>("Images/Enemy/Boss/Boss_walk"), g.Content, new Vector2(0, 100), 3000, 100, 0, 3, 4, 100f, 1f);

      
          
            // Khởi tạo list Enemy
            //liEnemy = new List<Enemy>();
            LiHearth = new List<Effect>();
            

            background = Content.Load<Texture2D>("Images/Background/Level3/Level3");
            healthTexture = Content.Load<Texture2D>("Images/HealthBar/Healthbar");

            effect = new Effect(Content.Load<Texture2D>("Images/Effect/Effect_01") , Vector2.Zero, 0, 1, 4, 100, 0.7f);

            this.g = g;
            Hit = Content.Load<SoundEffect>("Sounds/Hit");
            HitInstance = Hit.CreateInstance();
            Health = Content.Load<SoundEffect>("Sounds/UpLevel");
            HealthInstance = Health.CreateInstance();
    
        }

        Vector2 position;

        float timer = 0f;
        float delay = 2000f;

        float timer_hearth = 0f;
        float timer_enemy = 0f;
        
        // Khi chưa va chạm giữa Player so với Enemy


        float timerString = 0f;
        public override void Update(GameTime gameTime)
        {
            player.damge = 100;

            camera.Update(gameTime, player);
            player.Update(gameTime, g.Content);

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timer_hearth +=(float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            timerString += (float)gameTime.ElapsedGameTime.Milliseconds;

            // Boss


            boss.f_MoveAroundPlayer(gameTime, g.Content, (int)player._vt2_position.X, (int)player._vt2_position.Y);
            boss.Update(gameTime, g.Content);
            f_CollisionBoss_Player(gameTime);

            f_CollisionPlayer_Boss(gameTime);
        }


        public bool f_Check_Hearth(int Number)
        {
            if ((0 <= Number && Number <= 15) || (50 <= Number && Number <= 65) || (85 <= Number && Number <= 100))
                return true;
            return false;
        }


        float timeBoss = 0;

        public void f_CollisionBoss_Player(GameTime gameTime)
        {

            timeBoss = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (boss.f_Rectangle_srcBoss(new Vector2((int)boss._vt2_position.X + 50, (int)boss._vt2_position.Y + 100)).Intersects(player.f_Rectangle_dest(position)))
            {
                Random rd = new Random();
                int number = rd.Next(0, 2);

                boss.f_BossHack01(g.Content);

                boss._vt2_position.Y = 48;

                if (timer < 2000) // _i_currentFrame <= _i_totalFrame - 1 && 
                {
                    boss.f_BossHack01(g.Content);

                    boss.moveFrame(gameTime);
                    boss.animationCharacter();

                    player.curHealth -= 1;
                }

                // Nghĩ nghơi
                if (timer >= 2000)
                {
                    boss.f_BossHack02(g.Content);


                    boss.moveFrame(gameTime);
                    boss.animationCharacter();

                    player.curHealth -= 2;
                    if (timer >= 5000)
                        timer = 0;
                }

            }
            else
            {
                boss._vt2_position.Y = 100;
                boss.f_BossWalk(g.Content);
            }
        }


        public void f_CollisionPlayer_Boss(GameTime gameTime)
        {
            position = new Vector2((int)player._vt2_position.X + 135, (int)player._vt2_position.Y + 100);

            if (player.curAction == ActionState.Skill1)
            {
                timer_enemy += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                
                if (player.f_Rectangle_dest(position).Intersects(boss.f_Rectangle_srcBoss_Player(new Vector2((int)boss._vt2_position.X + 176, (int)boss._vt2_position.Y + 100))))
                {
                    if (timer_enemy > 100)
                    {
                        boss.curHealth -= 10;
                        timer_enemy = 0f;
                        Console.WriteLine("Mau boss " + boss.curHealth);


                        // Hiệu ứng boss khi bị đánh trúng
                        boss.f_BossDie(g.Content);
                        boss.calculateFrame();
                        boss.animationCharacter();
                        boss.moveFrame(gameTime);

                    }
                }

                
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            //Draw background Level1
            spriteBatch.Draw(background, new Vector2 (0,-50), new Rectangle(0, 0, 3000, 720), Color.White, 0, Vector2.Zero, 0.83f, SpriteEffects.None, 0f);

            //Draw HealthTexture and combod
            spriteBatch.Draw(healthTexture, new Vector2(camera.centre.X + 205, camera.centre.Y), new Rectangle(0, 0, healthTexture.Width, healthTexture.Height), Color.White, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
            spriteBatch.Draw(player.comboBar, new Vector2(camera.centre.X + 355, camera.centre.Y), player.rectComboBar, Color.White, 0, Vector2.Zero, 0.9f, SpriteEffects.None, 0);
            spriteBatch.Draw(player.healthbar, new Vector2 (camera.centre.X + 44,camera.centre.Y), player.rectHealthBar, Color.White, 0, Vector2.Zero, 0.9f, SpriteEffects.None, 0);

            
            //Draw player
            player.Draw(spriteBatch);


         
            if (player.flagCollection == true)
                effect.Draw(spriteBatch);

            if (LiHearth.Count > 0)
            {
                foreach (Effect e in LiHearth)
                {
                    e.Draw(spriteBatch);
                }
            }



            boss.Draw(spriteBatch);


            // draw start level1
            if (timerString < delay)
                spriteBatch.DrawString(font, "FINAL  ROUND", new Vector2(camera.centre.X + 350, camera.centre.Y + 200), Color.White);


            // draw score
            spriteBatch.DrawString(font, Global.score.ToString(), new Vector2(camera.centre.X + 850, camera.centre.Y + 27), Color.White);
    

            spriteBatch.End();

        }




        
    
    }
}
