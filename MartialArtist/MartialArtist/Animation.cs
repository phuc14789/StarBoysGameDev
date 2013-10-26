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
    public class Animation
    {
        /// khai bao 13 bien
        //1. khai bao bien SpriteBatch
        private SpriteBatch spriteBatch;

        //2. khai bao bien dung de tao Image
        private Texture2D _t_Image;

        //3. khai bao bien dung de xac dinh toa do
        private Vector2 _vt2_position;

        //4. khai bao bien dung de xac dinh van toc
        private Vector2 _vt2_Velocity;

        //5. khai bao bien dung de xac dinh vi tri xuat phat
        private Vector2 _vt2_Origin;

        //6.7 khai bao bien hang va cot
        private int _i_Rows;
        private int _i_Columns;

        //8.9 khai bao bien hinh anh tong va hien tai
        private int _i_totalFrame;
        private int _i_currentFrame;

        //10.11 khai bao chieu cao va chieu rong
        private int _i_width;
        private int _i_heigth;

        //12.13 khai bao bien khung hinh va diem den cua khung hinh
        private Rectangle _rect_sourceRectangle;
        private Rectangle _rect_destinationRectangle;

        private float _f_elapse;
        //Speed of frame
        private float _f_delay;

        //public 7 bien tu 1 den 7
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        public Texture2D T_Image
        {
            get { return _t_Image; }
            set { _t_Image = value; }
        }

        public Vector2 Vt2_position
        {
            get { return _vt2_position; }
            set { _vt2_position = value; }
        }

        public Vector2 Vt2_Velocity
        {
            get { return _vt2_Velocity; }
            set { _vt2_Velocity = value; }
        }

        public Vector2 Vt2_Origin
        {
            get { return _vt2_Origin; }
            set { _vt2_Origin = value; }
        }

        public int I_Rows
        {
            get { return _i_Rows; }
            set { _i_Rows = value; }
        }

        public int I_Columns
        {
            get { return _i_Columns; }
            set { _i_Columns = value; }
        }

        ///khai bao ham khoi tao voi 4 bien so
        public Animation(Texture2D texture, int currentFrame, int rows, int columns,float delay)
        {
            this.T_Image = texture;
            this.I_Rows = rows;
            this.I_Columns = columns;
            this._i_currentFrame = currentFrame;
            this._f_delay = delay;

            _i_totalFrame = I_Rows * I_Columns;

            _i_width = T_Image.Width / I_Columns;
            _i_heigth = T_Image.Height / I_Rows;
            rectSource();
        }

        ///khai bao ham Update
        public void Update(GameTime gameTime)
        {
            //Moving frame of character
            _f_elapse += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (_f_elapse >= _f_delay)
            {
                if (_i_currentFrame >= _i_totalFrame)
                    _i_currentFrame = 0;
                else
                    _i_currentFrame++;
                _f_elapse = 0;
            }
            
            _rect_destinationRectangle = new Rectangle((int)_vt2_position.X, (int)_vt2_position.Y, _i_width, _i_heigth);
        }

        public void rectSource()
        {
            //Calculate current frame
            int row = (int)((float)_i_currentFrame / (float)I_Columns);
            int column = _i_currentFrame % _i_Columns;

            //Update frame
            _rect_sourceRectangle = new Rectangle(_i_width * column, _i_heigth * row, _i_width, _i_heigth);
        }

        ///khai bao ham Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_t_Image, _rect_destinationRectangle, _rect_sourceRectangle, Color.White);
        }
    }
}
