﻿using BattleFury.Entities.Characters;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.Components.Animated;
using BattleFury.Components;

namespace BattleFury.Entities.Items
{
    /// <summary>
    /// Represents a Fist object that does some serious punching
    /// </summary>
    public class GroundPoundHitbox : Item
    {

        private const int WIDTH = 3;

        private const int HEIGHT = 2;

        public GroundPoundHitbox(Vector3 position, Character attackingCharacter, float damage, float minFlinch, float maxFlinch, Environment environment)
            : base(new Box(position, WIDTH, HEIGHT, 1, .01f))
        {
            // It's not going to be alive very long anyhow.
            this.bepuPhysicsComponent.Box.IsAffectedByGravity = false;
            this.bepuPhysicsComponent.Box.LinearVelocity = Vector3.Zero;

            HoldOffsetComponent offsetComponent = new HoldOffsetComponent(this, (BepuPhysicsComponent) attackingCharacter.GetComponent("BepuPhysicsComponent"));
            this.AttachComponent(offsetComponent);

            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(WIDTH, HEIGHT, 1);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);

            DamageOnImpactComponent damageComponent = new DamageOnImpactComponent(this, damage, environment, minFlinch, maxFlinch);
            damageComponent.IgnoreEntity(attackingCharacter);
            this.AttachComponent(damageComponent);

            SelfDestructOnImpactComponent selfDestructImpact = new SelfDestructOnImpactComponent(this, environment, true);
            selfDestructImpact.IgnoreEntity(attackingCharacter);
            this.AttachComponent(selfDestructImpact);

            SelfDestructOnJumpComponent selfDestructJump = new SelfDestructOnJumpComponent(this, environment, attackingCharacter);
            this.AttachComponent(selfDestructJump);
        }

       

    }
}
