using BattleFury.EntitySystem;
using BattleFury.Entities.Items;
using BEPUphysics.Collidables;
using BattleFury.Entities;
using System.Collections.Generic;
using BattleFury.Entities.Arenas;
using BattleFury.Entities.Characters;
using Microsoft.Xna.Framework;

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

        /// <summary>
        /// True if the item is marked for self destruct on the next frame.
        /// This is done so that if 2 items collide that want to self destruct,
        /// an items waits and gives another item a chance.
        /// 
        /// In other words, there's a 1 frame lag between colliding and removal.
        /// Hopefully this doesn't cause too many problems.
        /// </summary>
        private bool markedForRemoval;

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

        public override void Update(GameTime gameTime)
        {
            // Remove the item 1 frame after it has collided.
            if (markedForRemoval)
            {
                try
                {
                    environment.ItemManager.Remove((Item)Parent);
                } 
                catch (System.ArgumentException)
                {
                    // Ignore. Item has already been removed by another self destruct component.
                }
            }


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
                        markedForRemoval = true;
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
                    markedForRemoval = true;
                    return;
                } else if (!collidingWithCharacter && tempIgnoredEntities.Contains(characters[i])) {
                    ignoredEntities.Remove(characters[i]);
                    tempIgnoredEntities.Remove(characters[i]);
                }
            }

            // Check if items are colliding
            List<Item> items = environment.ItemManager.GetItems();
            for (int i = 0; i < items.Count; i++){
                bool collidingWithItem = collidingEntities.Contains(items[i].GetBox());

                if (collidingWithItem && !ignoredEntities.Contains(items[i])){
                    markedForRemoval = true;
                    return;
                } else if (!collidingWithItem && tempIgnoredEntities.Contains(items[i])) {
                    ignoredEntities.Remove(items[i]);
                    tempIgnoredEntities.Remove(items[i]);
                }
            }

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
