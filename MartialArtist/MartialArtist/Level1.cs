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
        public Level1(Game g, ContentManager Content)
        {
            camera = new Camera(g.GraphicsDevice.Viewport);
            player = new Player(Content.Load<Texture2D>("Images/Player/Player_Standing"),g.Content , new Vector2(0, 0), 1000, 100, 0, 2, 4, 50f, 0.7f);
            enemy = new Enemy[1];          
            enemy[0] = new Enemy(Content.Load<Texture2D>("Images/Enemy/Enemy1/Enemy01_walk"), g.Content , new Vector2(800, 200), 300, 3, 0, 3, 5, 50f, 0.66f);
            background = Content.Load<Texture2D>("Images/Background/Level1/Level1");
            healthTexture = Content.Load<Texture2D>("Images/HealthBar/Healthbar");
            this.g = g;
        }

        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime,player);
            player.Update(gameTime, g.Content);
            foreach (Enemy e in enemy)
            {
                e.Update(gameTime, g.Content);

                e.f_MoveAroundPlayer(gameTime, g.Content, (int)player._vt2_position .X,(int)player._vt2_position .Y);

                e.f_UpdateEnemy(gameTime, g.Content, player._rect_destinationRectangle);
                
                if (e.Collision == true)
                    player.curHealth -= 1;
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
            foreach (Enemy e in enemy)
                e.Draw(spriteBatch);

            //Draw player
            player.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
