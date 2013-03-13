using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.AnimationEngine;
using BattleFury.Components;
using BattleFury.Components.Animated;
using BattleFury.Entities.Hitboxes;

namespace BattleFury.Entities.Characters
{
    public abstract class Character : Entity
    {
        protected TransformComponent transformComponent;

        protected AnimationComponent animationComponent;

        protected GravityComponent gravityComponent;

        protected List<Hitbox> hitboxes;

        public Character(string id) : base(id){
            throw new NotImplementedException();
        }
    }
}
