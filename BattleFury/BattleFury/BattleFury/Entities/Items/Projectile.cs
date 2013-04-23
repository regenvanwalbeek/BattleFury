﻿using BattleFury.Components.Animated;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.Components;

namespace BattleFury.Entities.Items
{
    public class Projectile : Item
    {
        public Projectile(Vector3 spawnPosition, Vector3 velocity, Environment environment)
            : base(new Box(spawnPosition, .75f, .75f, .75f))
        {
            this.bepuPhysicsComponent.Box.LinearVelocity = velocity;
            this.bepuPhysicsComponent.Box.IsAffectedByGravity = false;

            SelfDestructOnImpactComponent selfDestructComponent = new SelfDestructOnImpactComponent(this, environment, true);
            this.AttachComponent(selfDestructComponent);

            DamageOnImpactComponent damageComponent = new DamageOnImpactComponent(this, 10, environment);
            this.AttachComponent(damageComponent);

            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(.75f, .75f, .75f);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);
        }

    }
}
