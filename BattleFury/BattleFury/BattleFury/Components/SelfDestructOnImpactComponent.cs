using BattleFury.EntitySystem;
using BattleFury.Entities.Items;
using BEPUphysics.Collidables;
using BattleFury.Entities;
using System.Collections.Generic;
using BattleFury.Entities.Arenas;
using BattleFury.Entities.Characters;

namespace BattleFury.Components
{
    /// <summary>
    /// Self destructs an Item on impact with another entity
    /// </summary>
    public class SelfDestructOnImpactComponent : Component
    {

        private BepuPhysicsComponent bepuPhysicsComponent;

        private Environment environment;

        private bool destructOnArenaImpact;

        public SelfDestructOnImpactComponent(Item parent, Environment environment, bool destructOnArenaImpact)
            : base(parent, "DestroyItemOnCharacterImpactComponent")
        {
            this.environment = environment;
            this.destructOnArenaImpact = destructOnArenaImpact;
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
           
            
            // Destroy the item if it collided with anything.
            EntityCollidableCollection.Enumerator enumerator = overlappedCollideables.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                BEPUphysics.Entities.Entity collidingEntity = enumerator.Current;

                // Check if the entity collides with the arena
                if (destructOnArenaImpact)
                {
                    List<Platform> platforms = environment.Arena.Platforms;
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        if (platforms[i].GetBox() == collidingEntity)
                        {
                            environment.ItemManager.Remove((Item)Parent);
                            return;
                        }
                    }
                }

                // Check if the entity collides with any characters
                List<Character> characters = environment.Characters;
                for (int i = 0; i < characters.Count; i++)
                {
                    if (collidingEntity == characters[i].GetBox())
                    {
                        environment.ItemManager.Remove((Item)Parent);
                        return;
                    }
                }

                // Check if the entity collides with any other items
                List<Item> items = environment.ItemManager.GetItems();
                for (int i = 0; i < items.Count; i++)
                {
                    if (collidingEntity == items[i].GetBox())
                    {
                        environment.ItemManager.Remove((Item)Parent);
                        return;
                    }
                }
            }
            
            /*
            if (enumerator.MoveNext())
            {
                environment.ItemManager.Remove((Item)Parent);
                return;
            }*/

        }
    }
}
