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

        private List<Entity> ignoredEntities = new List<Entity>();

        private List<Entity> tempIgnoredEntities = new List<Entity>();

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
            EntityCollidableCollection.Enumerator enumerator = overlappedCollideables.GetEnumerator();
            List<BEPUphysics.Entities.Entity> collidingEntities = new List<BEPUphysics.Entities.Entity>();
            while (enumerator.MoveNext())
            {
                collidingEntities.Add(enumerator.Current);
            }

            // Destroy the item if it collided with anything.

            // Check if arena elements are colliding
            List<Platform> platforms = environment.Arena.Platforms;
            for (int i = 0; i < platforms.Count; i++){
                bool collidingWithPlatform = collidingEntities.Contains(platforms[i].GetBox());

                if (collidingWithPlatform && !ignoredEntities.Contains(platforms[i])){
                    if (destructOnArenaImpact)
                    {
                        environment.ItemManager.Remove((Item)Parent);
                        return;
                    }
                    else
                    {
                        // Don't disappear after having landed back on the ground.
                        Parent.DetachComponent(this);
                    }
                } else if (!collidingWithPlatform && tempIgnoredEntities.Contains(platforms[i])) {
                    ignoredEntities.Remove(platforms[i]);
                    tempIgnoredEntities.Remove(platforms[i]);
                }
            }
            

            // Check if characters are colliding
            List<Character> characters = environment.Characters;
            for (int i = 0; i < characters.Count; i++){
                bool collidingWithCharacter = collidingEntities.Contains(characters[i].GetBox());

                if (collidingWithCharacter && !ignoredEntities.Contains(characters[i])){
                    environment.ItemManager.Remove((Item)Parent);
                    return;
                } else if (!collidingWithCharacter && tempIgnoredEntities.Contains(characters[i])) {
                    ignoredEntities.Remove(characters[i]);
                    tempIgnoredEntities.Remove(characters[i]);
                }
            }

            // Check if items are colliding
            List<Character> items = environment.Characters;
            for (int i = 0; i < items.Count; i++){
                bool collidingWithItem = collidingEntities.Contains(items[i].GetBox());

                if (collidingWithItem && !ignoredEntities.Contains(items[i])){
                    environment.ItemManager.Remove((Item)Parent);
                    return;
                } else if (!collidingWithItem && tempIgnoredEntities.Contains(items[i])) {
                    ignoredEntities.Remove(items[i]);
                    tempIgnoredEntities.Remove(items[i]);
                }
            }

           

            /*

                // Check if the entity collides with any entities in the arena.
                if (destructOnArenaImpact)
                {
                    List<Platform> platforms = environment.Arena.Platforms;
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        if (platforms[i].GetBox() == collidingEntity && !ignoredEntities.Contains(platforms[i]))
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
                    if (collidingEntity == characters[i].GetBox() && !ignoredEntities.Contains(characters[i]))
                    {
                        environment.ItemManager.Remove((Item)Parent);
                        return;
                    }
                }

                // Check if the entity collides with any other items
                List<Item> items = environment.ItemManager.GetItems();
                for (int i = 0; i < items.Count; i++)
                {
                    if (collidingEntity == items[i].GetBox() && !ignoredEntities.Contains(items[i]))
                    {
                        environment.ItemManager.Remove((Item)Parent);
                        return;
                    }
                }
            }



            */




            ////////////////////////////////////

            /*

            // Get the entities this item is colliding with. 
            EntityCollidableCollection overlappedCollideables = bepuPhysicsComponent.Box.CollisionInformation.OverlappedEntities;
            
            // Destroy the item if it collided with anything.
            EntityCollidableCollection.Enumerator enumerator = overlappedCollideables.GetEnumerator();
            List<BEPUphysics.Entities.Entity> collidingEntities = new List<BEPUphysics.Entities.Entity>();
            while (enumerator.MoveNext())
            {
                BEPUphysics.Entities.Entity collidingEntity = enumerator.Current;
                collidingEntities.Add(collidingEntity);

                // Check if the entity collides with any entities in the arena.
                if (destructOnArenaImpact)
                {
                    List<Platform> platforms = environment.Arena.Platforms;
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        if (platforms[i].GetBox() == collidingEntity && !ignoredEntities.Contains(platforms[i]))
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
                    if (collidingEntity == characters[i].GetBox() && !ignoredEntities.Contains(characters[i]))
                    {
                        environment.ItemManager.Remove((Item)Parent);
                        return;
                    }
                }

                // Check if the entity collides with any other items
                List<Item> items = environment.ItemManager.GetItems();
                for (int i = 0; i < items.Count; i++)
                {
                    if (collidingEntity == items[i].GetBox() && !ignoredEntities.Contains(items[i]))
                    {
                        environment.ItemManager.Remove((Item)Parent);
                        return;
                    }
                }
            }
            */
            // Update temp if no longer colliding

            /*
            if (enumerator.MoveNext())
            {
                environment.ItemManager.Remove((Item)Parent);
                return;
            }*/

        }

        /// <summary>
        /// Permanently ignores an entity for collision. Useful for making 
        /// exceptions to collision rules.
        /// </summary>
        /// <param name="entity">The entity to ignore.</param>
        public void IgnoreEntity(Entity entity)
        {
            ignoredEntities.Add(entity);
        }

        /// <summary>
        /// Temporarily ignores an entity for collision. Entity is ignored 
        /// until the item is no longer colliding with the given entity.
        /// Useful for making exceptions to collision rules.
        /// </summary>
        /// <param name="entity">The entity to ignore.</param>
        public void IgnoreEntityTemporarily(Entity entity)
        {
            tempIgnoredEntities.Add(entity);
            ignoredEntities.Add(entity);
        }
    }
}
