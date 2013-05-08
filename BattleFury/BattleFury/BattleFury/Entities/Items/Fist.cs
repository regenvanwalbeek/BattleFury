using BattleFury.Entities.Characters;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.Components.Animated;
using BattleFury.Components;
using BattleFury.SoundManager;

namespace BattleFury.Entities.Items
{
    /// <summary>
    /// Represents a Fist object that does some serious punching
    /// </summary>
    public class Fist : Item
    {
        public Fist(Vector3 position, Character attackingCharacter, float damage, float minFlinch, float maxFlinch, Environment environment)
            : base(new Box(position, 1, 1, 1, .01f))
        {
            // It's not going to be alive very long anyhow.
            this.bepuPhysicsComponent.Box.IsAffectedByGravity = false;
            this.bepuPhysicsComponent.Box.LinearVelocity = Vector3.Zero;

            HoldOffsetComponent offsetComponent = new HoldOffsetComponent(this, (BepuPhysicsComponent) attackingCharacter.GetComponent("BepuPhysicsComponent"));
            this.AttachComponent(offsetComponent);

            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(1, 1, 1);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);

            DamageOnImpactComponent damageComponent = new DamageOnImpactComponent(this, damage, environment, minFlinch, maxFlinch);
            damageComponent.IgnoreEntity(attackingCharacter);
            this.AttachComponent(damageComponent);

            SelfDestructOnImpactComponent selfDestructImpact = new SelfDestructOnImpactComponent(this, environment, true);
            selfDestructImpact.IgnoreEntity(attackingCharacter);
            selfDestructImpact.OnDestroy += OnHit;
            this.AttachComponent(selfDestructImpact);

            SelfDestructAfterTimeComponent selfDestructTime = new SelfDestructAfterTimeComponent(this, environment, 100);
            selfDestructTime.OnDestroy += OnMiss;
            this.AttachComponent(selfDestructTime);
        }

        public void OnHit(object sender, System.EventArgs e)
        {
            AudioManager.PlayPunch();
        }

        public void OnMiss(object sender, System.EventArgs e)
        {
            AudioManager.PlayMissedPunch();
        }

    }
}
