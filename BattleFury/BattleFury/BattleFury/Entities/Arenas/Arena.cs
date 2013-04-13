using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BEPUphysics.Entities.Prefabs;

namespace BattleFury.Entities.Arenas
{
    /// <summary>
    /// An abstract arena entity for battling.
    /// </summary>
    public abstract class Arena : Entity
    {
        protected List<Platform> platforms;

        /// <summary>
        /// Construct the Arena Entity.
        /// </summary>
        public Arena()
        {
            platforms = new List<Platform>();
        }

        /// <summary>
        /// Returns the next position to spawn a character.
        /// </summary>
        /// <returns>Where to spawn a character</returns>
        public abstract Vector3 GetCharacterSpawnPosition();

        /// <summary>
        /// Returns the next position to spawn an item.
        /// </summary>
        /// <returns>Where to spawn an item</returns>
        public abstract Vector3 GetItemSpawnPosition(Random random);

        /// <summary>
        /// Retrieves the game's bounding box. Falling out of this bounding box will kill the character.
        /// </summary>
        /// <returns></returns>
        public abstract BoundingBox GetBoundingBox();



    }
}
