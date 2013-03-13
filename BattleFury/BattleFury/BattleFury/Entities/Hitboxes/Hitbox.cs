using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components;

namespace BattleFury.Entities.Hitboxes
{
    public abstract class Hitbox : Entity
    {
        protected TransformComponent transformComponent;

        protected SphericalCollisionComponent collisionComponent;

        protected Entity owner;

        public Hitbox(string id) : base(id){
        
        }

    }
}
