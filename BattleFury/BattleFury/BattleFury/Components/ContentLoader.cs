using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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



            contentLoaded = true;
        }

        /// <summary>
        /// Unloads content.
        /// </summary>
        public static void UnloadContent()
        {
            if (contentLoaded)
            {
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
