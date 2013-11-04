using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MartialArtist
{
    public enum LEVELSTATE { CREATED, PLAYING, FINISHED }

    class Level
    {

        private LEVELSTATE levelState;
        public LEVELSTATE LevelState
        {
            get { return levelState; }
            set { levelState = value; }
        }

        protected Camera camera;
        protected Player player;
        protected Enemy[] enemy;
        //protected List<Texture2D> background;

        public virtual void Update(GameTime t)
        {

        }

        public virtual void Draw(SpriteBatch sp)
        {

        }

        
    }
}
