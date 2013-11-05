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
    class Enemy : Animation
    {
        // GraphicsDevice
        private GraphicsDevice _g_GraphicsDevice;

        // Texture để load Enemy    Có thế kế thừa từ Animation        
        private Texture2D _t_Enemy;         // Có thể bỏ
        
        // Vị trí Enemy
        private Vector2 _vt2_PositionEnemy; // Có thể bỏ

        // Tốc độ Enemy
        private int  _f_Speed = 3;

        // Mạng sống của Enemy
        private bool _b_Life; 

        // Sức mạnh của Enemy                          
        private int _i_Damage; 

        // Máu của Enemy
        private int _i_Heatlh; 

        // Cho Enemy xuất hiện ngẫu nhiên
        private Random _r_Random;     
   
        public  Color[] textureData;

        public GraphicsDevice G_GraphicsDevice
        {
            get { return _g_GraphicsDevice; }
            set { _g_GraphicsDevice = value; }
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

        public Enemy(Texture2D enemy, Vector2 position, int health, int damage, int currentFrame, int rows, int columns, float delay, float scale) 
            : base(enemy , position, currentFrame , rows , columns , delay , scale)           
        {
            //Health of enemy
            _i_Heatlh = health;

            //Damage of enemy
            _i_Damage = damage;

            // Random vị trí enemy xuất hiện
            _vt2_position = f_RandomEnemy(new Vector2(0, 100), new Vector2(700, 800), 210);

            //
            textureData = new Color[enemy.Width * enemy.Height];
            enemy.GetData(textureData);
        }        

        public void Initialize()
        {
            _r_Random = new Random();                                                                          

            // Trạng thái Enemy còn sống           
            _b_Life = true;                                                                                              

        }


        // Hàm này 

        /// <summary>
        /// Random vị trí enemy từ đâu đến đầu trong map
        /// </summary>
        /// <param name="position_1">Vùng quy định bên trái. Giả xử bên trái là từ 0 ==> 200</param>
        /// <param name="position_2">Vùng quy định bên phải. Giả xử bên phải là từ 700 ==> 800</param>
        /// <returns>Vị trí enemy được tao ra</returns>
        public  Vector2 f_RandomEnemy(Vector2 position_1, Vector2 position_2)
        {
            Random rd = new Random();
            // Trả về 0 or là 1 ==> 0: là left, 1: right
            int pos = rd.Next(0, 2);

            if (pos == 0)
            {
                // Lấy tọa độ của vị trí X khi đã random
                int number = rd.Next((int)position_1.X, (int)position_1.Y);
                // Lấy tọa độ của vị trí Y khi đã random
                int number2 = rd.Next(0, Global.screenHeight);

                // Nếu vị trí random > Kích thước hình thì
                if (number - _i_width >= 0)

                    return new Vector2(number - _i_width, (number2 - _i_heigth) >= 0 ? number2 - _i_heigth : number2);
                else
                    return new Vector2(number, (number2 - _i_heigth) >= 0 ? number2 - _i_heigth : number2);

            }
            else
            {
                // Lấy tọa độ của vị trí X khi đã random
                int number = rd.Next((int)position_2.X, (int)position_2.Y);
                // Lấy tọa độ của vị trí Y khi đã random
                int number2 = rd.Next(0, Global.screenHeight);

                // Nếu vị trí random > Kích thước hình thì
                if (number - _i_width >= 0)
                    return new Vector2(number - _i_width, (number2 - _i_heigth) >= 0 ? number2 - _i_heigth : number2);
                return new Vector2(number, (number2 - _i_heigth) >= 0 ? number2 - _i_heigth : number2);
            }
        }


        // Sửa lại
        /// <summary>
        /// Random vị trí enemy từ đâu đến đầu trong map
        /// </summary>
        /// <param name="position_1">Vùng quy định bên trái. Giả xử bên trái là từ 0 ==> 200</param>
        /// <param name="position_2">Vùng quy định bên phải. Giả xử bên phải là từ 700 ==> 800</param>
        /// <param name="Y">Y Kết hợp với X đề xuất hiện trên màn hình</param>
        /// <returns>Vị trí enemy được tao ra</returns>
        public Vector2 f_RandomEnemy(Vector2 position_1, Vector2 position_2, int Y)
        {
            Random rd = new Random();
            // Trả về 0 or là 1 ==> 0: là left, 1: right
            int pos = rd.Next(0, 2);

            if (pos == 0)
            {
                // Lấy tọa độ của vị trí X khi đã random
                int number = rd.Next((int)position_1.X, (int)position_1.Y);
                // Lấy tọa độ của vị trí Y khi đã random
                int number2 = rd.Next(0, Global.screenHeight);

                // Nếu vị trí random > Kích thước hình thì
                if (number - _i_width >= 0)

                    return new Vector2(number - _i_width, Y);
                else
                    return new Vector2(number, Y);

            }
            else
            {
                // Lấy tọa độ của vị trí X khi đã random
                int number = rd.Next((int)position_2.X, (int)position_2.Y);
                // Lấy tọa độ của vị trí Y khi đã random
                int number2 = rd.Next(0, Global.screenHeight);

                // Nếu vị trí random > Kích thước hình thì
                if (number - _i_width >= 0)
                    return new Vector2(number - _i_width, Y);
                return new Vector2(number, Y);
            }
        }

        /// <summary>
        /// Khoảng cách giữa 2 Vector dùng cho tính khoảng cách giữa Enemy với player
        /// </summary>
        /// <param name="pos1">Tọa độ 1</param>
        /// <param name="pos2">Tọa độ 2</param>
        /// <returns></returns>
        public float f_Distand(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Sqrt((pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y));
        }

        public float _Timer = 0;
        public float _Delay = 20;


        public void f_MoveEnemy(GameTime gameTime)
        {
            _Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_Timer >= _Delay)
            {
                _vt2_position.X += _f_Speed;
                _Timer = 0;
            }

        }

        /// <summary>
        /// Enemy di chuyển xung quanh nhân vật
        /// </summary>
        /// <param name="X"> X : Tọa độ Player</param>
        /// <param name="Y"> Y : Tọa độ Player</param>
        public void f_MoveAroundPlayer(int X, int Y)
        {
            // Tạo tốc độ cho Enemy
            Random rd = new Random();
            _f_Speed = rd.Next(1, 5);


            if (_vt2_position.X < X)
            {
                _vt2_position.X += _f_Speed;
                flip = SpriteEffects.None;
            }
            else
                if (_vt2_position.X > X)
                {
                    _vt2_position.X -= _f_Speed;
                    flip = SpriteEffects.FlipHorizontally;
                }
        }

        public override void Update(GameTime gameTime, ContentManager Content)
        {
            //f_MoveEnemy(gameTime);



            base.Update(gameTime, Content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            base.Draw(spriteBatch);            
        }
    }
}
