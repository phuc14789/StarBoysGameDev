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
    class Level1 : Level
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

        List<Effect> LiHearth;

        public Level1(Game g, ContentManager Content)
        {
            camera = new Camera(g.GraphicsDevice.Viewport);
            player = new Player(Content.Load<Texture2D>("Images/Player/Player_Standing"),g.Content , new Vector2(0, 0), 1000, 20 , 100, 0, 2, 4, 50f, 0.7f);

            //enemy = new Enemy[3];          
            //enemy[0] = new Enemy(Content.Load<Texture2D>("Images/Enemy/Enemy1/Enemy01_walk"), g.Content , new Vector2(800, 200), 300, 3, 0, 3, 5, 50f, 0.66f);
            //enemy[1] = new Enemy(Content.Load<Texture2D>("Images/Enemy/Enemy2/Enemy02_walk"), g.Content, new Vector2(800, 200), 300, 3, 0, 4, 4, 50f, 0.66f);
            //enemy[2] = new Enemy(Content.Load<Texture2D>("Images/Enemy/Boss/Boss_walk"), g.Content, new Vector2(800, 200), 300, 3, 0, 3, 4, 50f, 0.66f);


            // Khởi tạo list Enemy
            liEnemy = new List<Enemy>();
            LiHearth = new List<Effect>();
            

            background = Content.Load<Texture2D>("Images/Background/Level1/Level1");
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


        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime, player);
            player.Update(gameTime, g.Content);

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timer_hearth +=(float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Thêm Enemy với số lượng nhỏ hơn 10
            if (timer > delay && liEnemy.Count < 10)
            {
                liEnemy.Add(new Enemy(g.Content.Load<Texture2D>("Images/Enemy/Enemy1/Enemy01_walk"), g.Content, new Vector2(800, 200), 300, 20, 0, 3, 5, 50f, 0.66f));

                timer = 0;
            }

            
            // Duyệt qua tất cả các Enemy
            for(int i = 0;i< liEnemy.Count; i++)//foreach (Enemy e in liEnemy) //Enemy e in enemy
            {
                // Cập nhật Enemy
                liEnemy[i].Update(gameTime, g.Content);

                // Cho Enemy di chuyển xung quanh nhân vật
                liEnemy[i].f_MoveAroundPlayer(gameTime, g.Content, (int)player._vt2_position.X, (int)player._vt2_position.Y);

                // Lấy toa độ của người chơi (Tọa độ này là tọa độ khác với vẽ để dựa vào đó là lấy destangle)
                position = new Vector2((int)player._vt2_position.X + 135, (int)player._vt2_position.Y + 100);

                // Xem Enemy có va chạm với player thì sử dụng skill của Enemy
                liEnemy[i].f_UpdateEnemy(gameTime, g.Content, player.f_Rectangle_dest(position)); // player._rect_destinationRectangle

                // Nếu Enemy đánh player thì player mất 1 máu

                if (liEnemy[i].Collision == true)
                {
                    if (timer_hearth > 1000)
                    {
                        player.curHealth -= liEnemy[i].I_Damage;
                        timer_hearth = 0;
                    }   
                }
                // Xử lý Player va chạm với Enemy
                f_CollisionPlayer_Enemy(gameTime);
            }
        }


        public void f_CollisionPlayer_Enemy(GameTime gameTime)
        {

            player.flagCollection = false;

            // Duyệt qua tất cả các Enemy
            for (int i = 0; i < liEnemy.Count; i++)
            {
                
                // Sử dụng Skill thường loại 1
                if (player.curAction == ActionState.Skill1)
                {
                    timer_enemy += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    

                    if (  player.f_Rectangle_dest(position).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        if (timer_enemy > 1000)
                        {
                            HitInstance.Volume = 0.7f;
                            HitInstance.Play();
                            liEnemy[i].curHealth -= player.damge;
                            timer_enemy = 0f;
                        }
                        //effect._vt2_position = new Vector2((int)liEnemy[i]._vt2_position.X - 20, (int)liEnemy[i]._vt2_position.Y - 50);
                        player.flagCollection = true;
                        effect._vt2_position = new Vector2((int)liEnemy[i]._vt2_position.X - 20, (int)liEnemy[i]._vt2_position.Y - 50);
                        effect.Update(gameTime, g.Content);

                       
                    }
                }


                // Sử dụng Skill thường loại 2
                if (player.curAction == ActionState.Skill2)
                {
                    timer_enemy += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    
                    if (player.f_Rectangle_dest(position).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        if (timer_enemy > 1000)
                        {
                            HitInstance.Volume = 0.7f;
                            HitInstance.Play();
                            liEnemy[i].curHealth -= player.damge;
                            timer_enemy = 0f;
                        }
                    }
                }

                // Sử dụng Skill thường loại 3
                if (player.curAction == ActionState.Skill3)
                {
                    timer_enemy += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    
                    if (player.f_Rectangle_dest(position).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        if (timer_enemy > 1000)
                        {
                            HitInstance.Volume = 0.7f;
                            HitInstance.Play();
                            liEnemy[i].curHealth -= player.damge;
                            timer_enemy = 0f;
                        }

                    }
                }

                // Xét va chạm trong 1 vùng kích thước 400x400
                Vector2 vector_src = new Vector2((int)position.X - 170, (int)position.Y - 200);

                // Sử dụng Combo thường loại 1
                if (player.curAction == ActionState.Combo1)
                {
                    timer_enemy += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (player.f_Rectangle_srcPlayer(vector_src).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy))) //(player.f_Rectangle_dest(position).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        // Mất 30% máu
                        if (timer_enemy > 1000)
                        {
                            liEnemy[i].curHealth -= player.damge + 20;
                            timer_enemy = 0f;
                        }

                    }
                }

                // Sử dụng Combo thường loại 2
                if (player.curAction == ActionState.Combo2)
                {
                    timer_enemy += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (player.f_Rectangle_srcPlayer(vector_src).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        // Mất 60% máu
                        if (timer_enemy > 1000)
                        {
                            liEnemy[i].curHealth -= player.damge + 30;
                            timer_enemy = 0f;
                        }
                    }
                }

                // Sử dụng Combo thường loại 3
                if (player.curAction == ActionState.Combo3)
                {
                    timer_enemy += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (player.f_Rectangle_srcPlayer(vector_src).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        // die luôn
                        if (timer_enemy > 1000)
                        {
                            liEnemy[i].curHealth -= player.damge + 50;
                            timer_enemy = 0f;
                        }
                    }
                }


                // Nếu máu <=0 hoặc là Enemy đã chết
                if (liEnemy[i].curHealth <= 0 || liEnemy[i].B_Life == false)
                {
                    // Enemy die 
                    timer += (float)gameTime.ElapsedGameTime.Milliseconds;

                    // Khởi tạo thong tin cho Enemy khi die
                    liEnemy[i]._t_Image = g.Content.Load<Texture2D>("Images/Enemy/Enemy1/Enemy01_fall");
                    liEnemy[i]._i_Rows = 2;
                    liEnemy[i]._i_Columns = 4;
                    liEnemy[i]._f_delay = 1000f;

                    liEnemy[i].calculateFrame();

                    if (timer >= liEnemy[i]._f_delay)
                    {
                        if (liEnemy[i]._i_currentFrame < liEnemy[i]._i_totalFrame - 1)
                        {
                            liEnemy[i]._i_currentFrame++;
                        }
                        else
                        {
                            liEnemy[i]._i_currentFrame = 0;
                        }
                        timer = 0;
                    }
                    

                    // Thực hiện animation
                    liEnemy[i].animationCharacter();

                    // Xóa Enemy ra khỏi List khi frame của Enemy >= tổng frame - 1
                    if (liEnemy[i]._i_currentFrame >= liEnemy[i]._i_totalFrame - 1)
                    {
                        Random rd = new Random();
                        int Number = rd.Next(0, 101);

                        //Add heart
                        if (f_Check_Hearth(Number))
                            LiHearth.Add(new Effect(g.Content.Load<Texture2D>("Images/Effect/hearth"), new Vector2(liEnemy[i].Vt2_PositionEnemy.X + 110, liEnemy[i].Vt2_PositionEnemy.Y + 110), 0, 1, 1, 100, 1f));

                        liEnemy.RemoveAt(i);

                    }


                }

            }


            for(int i =0; i < LiHearth.Count; i++)
            {
                Rectangle dst = new Rectangle((int)LiHearth[i]._vt2_position.X, (int)LiHearth[i]._vt2_position.Y, 28, 22);

                if (player.f_Rectangle_dest(position).Intersects(dst))
                {
                    //Increase blood of player
                    if (player .curHealth < player.health )
                        player.curHealth += 100;
                    HealthInstance.Volume = 1f;
                    HealthInstance.Play();
                    //Remove heart after collision
                    LiHearth.RemoveAt(i);
                }
            }
        }


        public bool f_Check_Hearth(int Number)
        {
            if ((0 <= Number && Number <= 15) || (50 <= Number && Number <= 65) || (85 <= Number && Number <= 100))
                return true;
            return false;
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

            //Draw Every Enemy in Level
            foreach (Enemy e in liEnemy)
                e.Draw(spriteBatch);

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

           
            spriteBatch.End();

        }



    }
}
