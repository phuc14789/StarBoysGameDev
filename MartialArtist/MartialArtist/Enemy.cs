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
    class Enemy
    {
        // Dùng để load hình ảnh
        private SpriteBatch _p_SpriteBatch;

        // GraphicsDevice
        private GraphicsDevice _g_GraphicsDevice;

        // Texture để load Enemy    Có thế kế thừa từ Animation        
        private Texture2D _t_Enemy;
        
        // Vị trí Enemy
        private Vector2 _vt2_PositionEnemy;

        // Tốc độ Enemy
        private int  _f_Speed;

        // Mạng sống của Enemy
        private bool _b_Life;

        // Sức mạnh của Enemy                          
        private int _i_Damage;

        // Máu của Enemy
        private int _i_Heatlh;

        // Cho Enemy xuất hiện ngẫu nhiên
        private Random _r_Random;        
        
        // Khởi tạo cho Animation             
        private Animation animation;

        public GraphicsDevice G_GraphicsDevice
        {
            get { return _g_GraphicsDevice; }
            set { _g_GraphicsDevice = value; }
        }

        public SpriteBatch P_SpriteBatch
        {
            get { return _p_SpriteBatch; }
            set { _p_SpriteBatch = value; }
        }

        public Texture2D T_Enemy
        {
            get { return _t_Enemy; }
            set { _t_Enemy = value; }
        }

        public Vector2 Vt2_PositionEnemy
        {
            get { return _vt2_PositionEnemy; }
            set { _vt2_PositionEnemy = value; }
        }

        public int F_Speed
        {
            get { return _f_Speed; }
            set { _f_Speed = value; }
        }

        public bool B_Life
        {
            get { return _b_Life; }
            set { _b_Life = value; }
        }

        public int I_Damage
        {
            get { return _i_Damage; }
            set { _i_Damage = value; }
        }

        public int I_Heatlh
        {
            get { return _i_Heatlh; }
            set { _i_Heatlh = value; }
        }

        public Enemy(Texture2D enemy, Vector2 position, int health, int damage,int currentFrame, int rows, int columns,float delay)           
        {
            this._t_Enemy = enemy;

            // Vị trí để Enemy xuất hiện  
            _vt2_PositionEnemy = position;

            //Health of enemy
            _i_Heatlh = health;

            //Damage of enemy
            _i_Damage = damage;

            animation = new Animation(enemy, currentFrame, rows, columns, delay);
        }

        public void Initialize()
        {
            _r_Random = new Random();                                                                          

            // Trạng thái Enemy còn sống           
            _b_Life = true;

            // Máu Enemy                                                                                 
            _i_Heatlh = 100;

            // Sức mạnh Enemy                                                                                
            _i_Damage = 100;                                                                                                  

        }        

        public void LoadContent(ContentManager Content)
        {                                                  
           
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {          
            //spriteBatch.Draw(_t_Enemy, Vector2.Zero, Color.White);
            animation.Draw(spriteBatch);
        }
    }
}
