using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Entities
{
    public abstract class Arena : Entity
    {

        private List<Platform> platforms;

        private BoundingBox boundingBox;

        public Arena(string id)
            : base(id)
        {
        }

    }
}
