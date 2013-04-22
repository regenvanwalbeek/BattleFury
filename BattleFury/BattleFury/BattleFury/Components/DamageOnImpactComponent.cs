
using BattleFury.Entities.Items;
using BattleFury.EntitySystem;
using BattleFury.Entities.Characters;
using BattleFury.Entities;
using System.Collections.Generic;
using BEPUphysics.Collidables;
using BattleFury.Components.Characters;

namespace BattleFury.Components
{
    public class DamageOnImpactComponent : Component
    {

        private int damage;

        private List<Character> characters;

        private BepuPhysicsComponent bepuPhysicsComponent;

        public DamageOnImpactComponent(Item parent, int damage, Environment environment)
            : base(parent, "DamageOnImpactComponent")
        {
            this.damage = damage;
            this.characters = environment.Characters;
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
            // Get the entities this item is colliding with. 
            EntityCollidableCollection overlappedCollideables = bepuPhysicsComponent.Box.CollisionInformation.OverlappedEntities;
            
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
                        }
                    }
                }
            }
        }


    }
}
