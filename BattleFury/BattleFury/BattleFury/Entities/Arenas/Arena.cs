using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Entities.Arenas
{
    public abstract class Arena : Entity
    {

        protected List<Platform> platforms;

        protected BoundingBox boundingBox;

        public Arena(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Returns the next position to spawn a character.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Where to spawn a character</returns>
        public abstract Vector3 GetSpawnPosition();

    }
}
