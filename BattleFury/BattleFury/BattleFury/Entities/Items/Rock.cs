using BattleFury.Components;
using BattleFury.Components.Animated;
using BattleFury.Components.Movement;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.EntitySystem;
using System;
using BattleFury.Entities.Characters;

namespace BattleFury.Entities.Items
{
    public class Rock : Item
    {
        private const int MASS = 1;

        private const float MIN_FLINCH_DISTANCE = 10;

        private const float MAX_FLINCH_DISTANCE = 25;

        private Environment environment;

        private GrabbableComponent grabbable;

        public Rock(Vector3 spawnPosition, Environment environment)
            : base(new Box(spawnPosition, 1, 1, 1, MASS))
        {
            this.environment = environment;

            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(1, 1, 1);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);

            grabbable = new GrabbableComponent(this);
            grabbable.OnThrow += OnRockThrow;
            this.AttachComponent(grabbable);
            
            
        }

        public void OnRockThrow(object sender, EventArgs e)
        {
            DamageOnImpactComponent damageComponent = new DamageOnImpactComponent(this, 10, environment, MIN_FLINCH_DISTANCE, MAX_FLINCH_DISTANCE);
            damageComponent.IgnoreEntityTemporarily((Character) grabbable.Grabber.Parent);
            this.AttachComponent(damageComponent);

            SelfDestructOnImpactComponent selfDestructComponent = new SelfDestructOnImpactComponent(this, environment, false);
            selfDestructComponent.IgnoreEntityTemporarily(grabbable.Grabber.Parent);
            this.AttachComponent(selfDestructComponent);
        }

    }

}
