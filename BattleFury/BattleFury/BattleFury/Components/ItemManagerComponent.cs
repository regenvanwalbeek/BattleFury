using System.Collections.Generic;
using BattleFury.Entities.Arenas;
using BattleFury.Entities.Items;
using BattleFury.Entities.Physics;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Components
{
    /// <summary>
    /// Manages items in the game. Whenever an item is created, it should be 
    /// added to this component. All operations for adding and removing items 
    /// should go through this component.
    /// 
    /// Also takes care of removing any items that leave the arena.
    /// </summary>
    public class ItemManagerComponent : Component
    {

        private EntityManager entityManager;

        private PhysicsSimulator physics;

        private Arena arena;

        /// <summary>
        /// Items that have been spawned.
        /// </summary>
        private List<Item> Items;



        public ItemManagerComponent(Entity parent, EntityManager entityManager, PhysicsSimulator physics, Arena arena)
            : base(parent, "ItemManagerComponent")
        {
            this.entityManager = entityManager;
            this.physics = physics;
            this.arena = arena;
            this.Items = new List<Item>();
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Remove any items outside of the arena
            for (int i = 0; i < Items.Count; i++)
            {
                Item item = Items[i];
                if (arena.GetBoundingBox().Contains(item.GetBox().Position) == ContainmentType.Disjoint)
                {
                    // Remove the item from the physics, the entity manager, and the items list.
                    this.Remove(item);
                    i--;
                }
            }
        }


        /// <summary>
        /// Adds an item to the item manager. Takes care of setting the item up for the physics engine and game environment
        /// </summary>
        /// <param name="item"></param>
        public void Add(Item item)
        {
            Items.Add(item);
            entityManager.AddEntity(item);
            physics.AddPhysicsEntity(item.GetBox());
        }

        /// <summary>
        /// Gets the Items in the world. Modifying the returned list will have 
        /// no effect on items in the game world. Use the component method to 
        /// modify items in the game world.
        /// </summary>
        /// <returns>Items in the game world</returns>
        public List<Item> GetItems()
        {
            List<Item> itemsCopy = new List<Item>();
            for (int i = 0; i < Items.Count; i++)
            {
                itemsCopy.Add(Items[i]);
            }
            return itemsCopy;
        }

        /// <summary>
        /// Removes an item from the world. Also takes care of physics removal.
        /// </summary>
        /// <param name="item">The item to remove</param>
        public void Remove(Item item)
        {
            Items.Remove(item);
            entityManager.RemoveEntity(item);
            physics.RemovePhysicsEntity(item.GetBox());
        }

        /// <summary>
        /// Get the number of items currently in the game.
        /// </summary>
        /// <returns>The number of items in the game.</returns>
        public int NumItems()
        {
            return Items.Count;
        }
    
    }        
}
