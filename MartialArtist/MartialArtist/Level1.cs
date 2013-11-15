using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace MartialArtist
{
    class Level1 : Level
    {        
        //Initialize Objects
        Game g;
        Texture2D background;
        Texture2D healthTexture;
        SpriteFont font;
        Enemy effect;

        public Level1(Game g, ContentManager Content)
        {
            camera = new Camera(g.GraphicsDevice.Viewport);
            player = new Player(Content.Load<Texture2D>("Images/Player/Player_Standing"),g.Content , new Vector2(0, 0), 1000, 100, 0, 2, 4, 50f, 0.7f);

            //enemy = new Enemy[3];          
            //enemy[0] = new Enemy(Content.Load<Texture2D>("Images/Enemy/Enemy1/Enemy01_walk"), g.Content , new Vector2(800, 200), 300, 3, 0, 3, 5, 50f, 0.66f);
            //enemy[1] = new Enemy(Content.Load<Texture2D>("Images/Enemy/Enemy2/Enemy02_walk"), g.Content, new Vector2(800, 200), 300, 3, 0, 4, 4, 50f, 0.66f);
            //enemy[2] = new Enemy(Content.Load<Texture2D>("Images/Enemy/Boss/Boss_walk"), g.Content, new Vector2(800, 200), 300, 3, 0, 3, 4, 50f, 0.66f);


            // Khởi tạo list Enemy
            liEnemy = new List<Enemy>();            

            background = Content.Load<Texture2D>("Images/Background/Level1/Level1");
            healthTexture = Content.Load<Texture2D>("Images/HealthBar/Healthbar");

            effect = new Enemy(Content.Load<Texture2D>("Images/Effect/Effect_01"), g.Content , Vector2.Zero, 100, 100, 0, 1, 4, 100, 0.7f);

            this.g = g;
        }

        Vector2 position;

        float timer = 0f;
        float delay = 2000f;

        // Khi chưa va chạm giữa Player so với Enemy
        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime, player);
            player.Update(gameTime, g.Content);

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Thêm Enemy với số lượng nhỏ hơn 10
            if (timer > delay && liEnemy.Count < 10)
            {
                liEnemy.Add(new Enemy(g.Content.Load<Texture2D>("Images/Enemy/Enemy1/Enemy01_walk"), g.Content, new Vector2(800, 200), 300, 3, 0, 3, 5, 50f, 0.66f));

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
                    player.curHealth -= 1;

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
                    if (player.f_Rectangle_dest(position).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        liEnemy[i].curHealth -= 1;

                        //effect._vt2_position = new Vector2((int)liEnemy[i]._vt2_position.X - 20, (int)liEnemy[i]._vt2_position.Y - 50);
                 
                        player.flagCollection = true;
                        effect._vt2_position = new Vector2((int) liEnemy[i]._vt2_position.X - 20, (int) liEnemy[i]._vt2_position.Y - 50);
                        effect.Update(gameTime, g.Content);
                    }
                }

                // Sử dụng Skill thường loại 2
                if (player.curAction == ActionState.Skill2)
                {
                    if (player.f_Rectangle_dest(position).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        liEnemy[i].curHealth -= 2;
                    }
                }

                // Sử dụng Skill thường loại 3
                if (player.curAction == ActionState.Skill3)
                {
                    if (player.f_Rectangle_dest(position).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        liEnemy[i].curHealth -= 3;


                    }
                }

                // Xét va chạm trong 1 vùng kích thước 400x400
                Vector2 vector_src = new Vector2((int)position.X - 170, (int)position.Y - 200);

                // Sử dụng Combo thường loại 1
                if (player.curAction == ActionState.Combo1)
                {                    
                    
                    if (player.f_Rectangle_srcPlayer(vector_src).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy))) //(player.f_Rectangle_dest(position).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        // Mất 30% máu
                        liEnemy[i].curHealth -= (int) (liEnemy[i].curHealth * 0.3f);

                    }
                }

                // Sử dụng Combo thường loại 2
                if (player.curAction == ActionState.Combo2)
                {
                    if (player.f_Rectangle_srcPlayer(vector_src).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        // Mất 60% máu
                        liEnemy[i].curHealth -= (int)(liEnemy[i].curHealth * 0.6f);
                    }
                }

                // Sử dụng Combo thường loại 3
                if (player.curAction == ActionState.Combo3)
                {
                    if (player.f_Rectangle_srcPlayer(vector_src).Intersects(liEnemy[i].f_Rectangle_dest(liEnemy[i].Vt2_PositionEnemy)))
                    {
                        // die luôn
                        liEnemy[i].B_Life = false;

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
                        liEnemy.RemoveAt(i);
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

            //Draw Every Enemy in Level
            foreach (Enemy e in liEnemy)
                e.Draw(spriteBatch);

            //Draw player
            player.Draw(spriteBatch);
         
            if (player.flagCollection == true)
                effect.Draw(spriteBatch);
           
            spriteBatch.End();

        }
    }
}
