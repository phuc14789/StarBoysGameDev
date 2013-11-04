using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MartialArtist
{
    class LevelManager
    {
        // Indicate the last level is complete
        bool gameOver = false;    

        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        // Counter for the current level
        int CurrentLevel = 0;

        // Maximum amount of levels
        const int MAXLEVEL = 3;

        // Collection of levels which are created as subclasses of Level
        Level[] Levels;        

        public LevelManager(Game g, ContentManager Content)
        {
            //Create Levels
            Levels = new Level[MAXLEVEL];
            Levels[0] = new Level1(g, Content);

            //Set begin Level
            Levels[0].LevelState = LEVELSTATE.PLAYING;
            
        }

        public void Update(GameTime t)
        {
            if (!gameOver)
            {
                foreach (Level l in Levels)
                {
                    if (l != null && l.LevelState == LEVELSTATE.PLAYING)
                    {   // Update the current playing level
                        l.Update(t);
                        // if the current level has finished
                        if (l.LevelState == LEVELSTATE.FINISHED)
                        {   // Get rid of the level should 
                            Levels[CurrentLevel] = null;
                            // and if the not the last level finished
                            if (++CurrentLevel < MAXLEVEL)
                                // then play the next level
                                Levels[CurrentLevel].LevelState = LEVELSTATE.PLAYING;   
                                //Or else we are finished
                            else gameOver = true;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Level l in Levels)
                // need to check for null as we 
                if(l != null)
                    l.Draw(spriteBatch);
        }

        internal Level Level
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
