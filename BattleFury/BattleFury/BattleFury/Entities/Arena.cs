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

        protected List<Platform> platforms;

        protected BoundingBox boundingBox;

        public Arena(string id)
            : base(id)
        {
        }

    }
}
