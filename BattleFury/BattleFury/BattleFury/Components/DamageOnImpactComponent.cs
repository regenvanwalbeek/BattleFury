
using BattleFury.Entities.Items;
using BattleFury.EntitySystem;
using BattleFury.Entities.Characters;
using BattleFury.Entities;
using System.Collections.Generic;
using BEPUphysics.Collidables;
using BattleFury.Components.Characters;
using BattleFury.Entities.Arenas;
using Microsoft.Xna.Framework;
using BattleFury.Components.Movement;

namespace BattleFury.Components
{
    public class DamageOnImpactComponent : Component
    {

        private float damage;

        private float minFlinch;

        private float maxFlinch;

        private Environment environment;

        private BepuPhysicsComponent bepuPhysicsComponent;

        private List<Character> ignoredEntities = new List<Character>();

        private List<Character> tempIgnoredEntities = new List<Character>();

        public DamageOnImpactComponent(Item parent, float damage, Environment environment, float minFlinch, float maxFlinch)
            : base(parent, "DamageOnImpactComponent")
        {
            this.damage = damage;
            this.environment = environment;
            this.minFlinch = minFlinch;
            this.maxFlinch = maxFlinch;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            this.bepuPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");   
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            bool damageApplied = false; // so that damage can be applied to multiple entities
      
            // Get the entities this item is colliding with. 
            EntityCollidableCollection overlappedCollideables = bepuPhysicsComponent.Box.CollisionInformation.OverlappedEntities;
            EntityCollidableCollection.Enumerator enumerator = overlappedCollideables.GetEnumerator();
            List<BEPUphysics.Entities.Entity> collidingEntities = new List<BEPUphysics.Entities.Entity>();
            while (enumerator.MoveNext())
            {
                collidingEntities.Add(enumerator.Current);
            }

            // Check if colliding with a character
            List<Character> characters = environment.Characters;
            for (int i = 0; i < characters.Count; i++)
            {
                bool collidingWithCharacter = collidingEntities.Contains(characters[i].GetBox());

                if (collidingWithCharacter && !ignoredEntities.Contains(characters[i]))
                {
                    // Damage the vitality component when thrown if the entity has vitality
                    // Also apply flinch based on vitality
                    VitalityComponent health = (VitalityComponent)characters[i].GetComponent("VitalityComponent");
                    if (health != null)
                    {
                        // Damage
                        health.Damage(damage);
                        damageApplied = true;

                        // Flinch
                        float flinch = minFlinch + ((maxFlinch - minFlinch) / 100) * (100 - health.RageMeter);
                        if (health.RageMeter == 0)
                        {
                            // RAGE MODE
                            flinch *= 4;
                        }
                        // Apply flinch if the character is not being grabbed.
                        if (!((GrabbableComponent)characters[i].GetComponent("GrabbableComponent")).IsGrabbed)
                        {
                            Vector3 characterPosition = ((BepuPhysicsComponent)characters[i].GetComponent("BepuPhysicsComponent")).Box.Position;
                            Vector3 flinchDirection = characterPosition - this.bepuPhysicsComponent.Box.Position;
                            flinchDirection = Vector3.Normalize(flinchDirection);

                            Vector3 flinchVector = flinchDirection * flinch;
                            characters[i].GetBox().ApplyLinearImpulse(ref flinchVector);
                        }
                    }
                }
                else if (!collidingWithCharacter && tempIgnoredEntities.Contains(characters[i]))
                {
                    // No longer ignore this entity.
                    ignoredEntities.Remove(characters[i]);
                    tempIgnoredEntities.Remove(characters[i]);
                }
            }

            // Turn off damage if arena elements are colliding (ex. rock thrown, lands on ground and bounces, don't want it to damage anymore)
            List<Platform> platforms = environment.Arena.Platforms;
            for (int i = 0; i < platforms.Count; i++)
            {
                bool collidingWithPlatform = collidingEntities.Contains(platforms[i].GetBox());

                if (collidingWithPlatform)
                {
                    // Don't disappear after having landed back on the ground.
                    Parent.DetachComponent(this);
                    
                }
            }

            // Stop damaging if the item has dealt damage.
            if (damageApplied)
            {
                Parent.DetachComponent(this);
            }
        }

        /// <summary>
        /// Permanently ignores an entity for damage. Useful for making 
        /// exceptions to damage rules.
        /// </summary>
        /// <param name="entity">The entity to ignore.</param>
        public void IgnoreEntity(Character entity)
        {
            ignoredEntities.Add(entity);
        }

        /// <summary>
        /// Temporarily ignores a character for collision. Entity is ignored 
        /// until the item is no longer colliding with the given entity.
        /// Useful for making exceptions to collision rules.
        /// </summary>
        /// <param name="entity">The entity to ignore.</param>
        public void IgnoreEntityTemporarily(Character entity)
        {
            tempIgnoredEntities.Add(entity);
            ignoredEntities.Add(entity);
        }


    }
}
