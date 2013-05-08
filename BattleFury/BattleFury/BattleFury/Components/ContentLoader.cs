using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace BattleFury.Components
{
    /// <summary>
    /// Loads the content required for Drawable Components for easy retrieval. 
    /// Methods should be called in LoadContent method. Accesses may be called 
    /// anywhere after methods are called. Yes, this is a hack. I'm sick of 
    /// passing 800 arguments into each constructor. And delegating loading to
    /// each component/entity just makes for a bad time when you're creating these
    /// things on the fly.
    /// </summary>
    public class ContentLoader
    {

        private static ContentManager Content;

        private static bool contentLoaded = false;

        public static Model Cube;

        public static Model Robot;

        public static Model IceCream;

        // Temporary hack due to some blender silliness
        public static Model RobotRed;
        public static Model RobotBlue;
        public static Model RobotYellow;

        public static SpriteFont HUDRageFont;

        public static SpriteFont HUDLivesFont;

        public static Effect ColorEffect;

        public static SoundEffect ProjectileSoundEffect;
        public static SoundEffect JumpSoundEffect;
        public static SoundEffect PainSound;
        public static SoundEffect BattleMusic;

        public static SoundEffect MissedPunch;
        public static SoundEffect Punch1;
        public static SoundEffect Punch2;
        public static SoundEffect Punch3;
        public static SoundEffect Punch4;

        public static SoundEffect FirstBlood;


        private ContentLoader()
        {
            // Ain't nobody got time for that.
        }

        public static void LoadContent(ContentManager content){
            if (content == null)
            {
                throw new ArgumentNullException("Content cannot be null");
            }

            if (contentLoaded)
            {
                // Been here before.
                return;
            }

            Content = content;

            // Load the cube model
            Cube = content.Load<Model>("meshes/cube");
            
            // Load the robot model
            Robot = content.Load<Model>("meshes/Robot-joined");
            RobotRed = content.Load<Model>("meshes/Robot-red");
            RobotBlue = content.Load<Model>("meshes/Robot-blue");
            RobotYellow = content.Load<Model>("meshes/Robot-yellow");

            // Load the ice cream model
            IceCream = content.Load<Model>("meshes/icecreamcone");

            // Load HUD Fonts
            HUDRageFont = content.Load<SpriteFont>("fonts/HUDFontRage");
            HUDLivesFont = content.Load<SpriteFont>("fonts/HUDFontLives");

            // Load the color shader
            ColorEffect = content.Load<Effect>("effects/ReplaceColor");

            // Load the sounds
            ProjectileSoundEffect = content.Load<SoundEffect>("sounds/projectile");
            JumpSoundEffect = content.Load<SoundEffect>("sounds/jump");
            PainSound = content.Load<SoundEffect>("sounds/pain");
            BattleMusic = content.Load<SoundEffect>("sounds/game");
            MissedPunch = content.Load<SoundEffect>("sounds/punch_missed");
            Punch1 = content.Load<SoundEffect>("sounds/punch1");
            Punch2 = content.Load<SoundEffect>("sounds/punch2");
            Punch3 = content.Load<SoundEffect>("sounds/punch3");
            Punch4 = content.Load<SoundEffect>("sounds/punch4");
            FirstBlood = content.Load<SoundEffect>("sounds/firstblood");

            contentLoaded = true;
        }

        /// <summary>
        /// Unloads content.
        /// </summary>
        public static void UnloadContent()
        {
            if (contentLoaded)
            {
                contentLoaded = false;
                Content.Unload();
            }
            else
            {
                // Content hasn't been loaded yet
                throw new InvalidOperationException();
            }
        }

    }
}
