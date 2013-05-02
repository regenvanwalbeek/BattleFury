using BattleFury.Components.Animated;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.Components;
using BattleFury.Entities.Characters;
using BattleFury.EntitySystem;
using System.Collections.Generic;

namespace BattleFury.Entities.Items
{
    public class Projectile : Item
    {
        public Projectile(Vector3 spawnPosition, Vector3 velocity, Environment environment, Character attackingCharacter, float damage, float minFlinch, float maxFlinch)
            : base(new Box(spawnPosition, .75f, .75f, .75f, 0.01f))
        {
            this.bepuPhysicsComponent.Box.LinearVelocity = velocity;
            this.bepuPhysicsComponent.Box.IsAffectedByGravity = false;

            //List<Entity> projectilesToIgnore = environment.GetEntitiesOfType<Projectile>();

            DamageOnImpactComponent damageComponent = new DamageOnImpactComponent(this, damage, environment, minFlinch, maxFlinch);
            damageComponent.IgnoreEntity(attackingCharacter);
            this.AttachComponent(damageComponent);


            SelfDestructOnImpactComponent selfDestructComponent = new SelfDestructOnImpactComponent(this, environment, true);
            selfDestructComponent.IgnoreEntity(attackingCharacter);
            this.AttachComponent(selfDestructComponent);
            

            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(.75f, .75f, .75f);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);
        }

    }
}
