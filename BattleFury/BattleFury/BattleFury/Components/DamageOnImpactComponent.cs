
using BattleFury.Entities.Items;
using BattleFury.EntitySystem;
using BattleFury.Entities.Characters;
using BattleFury.Entities;
using System.Collections.Generic;
using BEPUphysics.Collidables;
using BattleFury.Components.Characters;
using BattleFury.Entities.Arenas;

namespace BattleFury.Components
{
    public class DamageOnImpactComponent : Component
    {

        private float damage;

        private Environment environment;

        private BepuPhysicsComponent bepuPhysicsComponent;

        private List<Character> ignoredEntities = new List<Character>();

        private List<Character> tempIgnoredEntities = new List<Character>();

        public DamageOnImpactComponent(Item parent, float damage, Environment environment)
            : base(parent, "DamageOnImpactComponent")
        {
            this.damage = damage;
            this.environment = environment;
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

            // Check if characters are colliding
            List<Character> characters = environment.Characters;
            for (int i = 0; i < characters.Count; i++)
            {
                bool collidingWithCharacter = collidingEntities.Contains(characters[i].GetBox());

                if (collidingWithCharacter && !ignoredEntities.Contains(characters[i]))
                {
                    // Damage the vitality component when thrown if the entity has vitality
                    VitalityComponent health = (VitalityComponent)characters[i].GetComponent("VitalityComponent");
                    if (health != null)
                    {
                        health.Damage(damage);
                        damageApplied = true;
                    }
                }
                else if (!collidingWithCharacter && tempIgnoredEntities.Contains(characters[i]))
                {
                    ignoredEntities.Remove(characters[i]);
                    tempIgnoredEntities.Remove(characters[i]);
                }
            }

            // Turn off damage if arena elements are colliding
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


            /*
            // Damage any characters it is colliding with.
            EntityCollidableCollection.Enumerator enumerator = overlappedCollideables.GetEnumerator();
            while (enumerator.MoveNext())
            {
                BEPUphysics.Entities.Entity collidingEntity = enumerator.Current;

                // Check if the entity collides with any characters
                for (int i = 0; i < characters.Count; i++)
                {
                    if (collidingEntity == characters[i].GetBox())
                    {
                        // Damage the vitality component when thrown if the entity has vitality
                        VitalityComponent health = (VitalityComponent)characters[i].GetComponent("VitalityComponent");
                        if (health != null)
                        {
                            health.Damage(damage);
                            damageApplied = true;
                        }
                    }
                }
            }*/

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
