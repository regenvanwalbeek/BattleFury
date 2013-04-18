using System;
using BattleFury.Entities.Arenas;
using BattleFury.Entities.Items;
using BattleFury.Entities.Physics;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BattleFury.Components
{
    public class ItemSpawnComponent : Component
    {
        private Random random;

        private EntityManager entityManager;

        private PhysicsSimulator physics;

        /// <summary>
        /// Minimum amount of time in which items should be spawned.
        /// </summary>
        private const int MIN_FREQUENCY = 5000;

        /// <summary>
        /// Maximum amount of time in which items should be spawned.
        /// </summary>
        private const int MAX_FREQUENCY = 10000;

        /// <summary>
        /// Number of milliseconds until an item spawns.
        /// </summary>
        private int timeTillSpawn;

        /// <summary>
        /// Limit on the number of items that can be randomly spawned. Note that items can still be added manually after random spawning
        /// </summary>
        private const int ITEM_LIMIT = 1;

        private Arena arena;

        /// <summary>
        /// Items that have been spawned.
        /// </summary>
        private List<Item> Items;

        private bool itemDropsAllowed;

        public ItemSpawnComponent(Entity parent, EntityManager entityManager, PhysicsSimulator physics, Arena arena, bool itemDropsAllowed) : base(parent, "ItemSpawnComponent")
        {
            this.random = new Random();
            this.entityManager = entityManager;
            this.timeTillSpawn = random.Next(MAX_FREQUENCY - MIN_FREQUENCY) + MIN_FREQUENCY;
            this.physics = physics;
            this.arena = arena;
            this.Items = new List<Item>();
            this.itemDropsAllowed = itemDropsAllowed;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {

            // Spawn items at random times.
            timeTillSpawn -= gameTime.ElapsedGameTime.Milliseconds;
            if (itemDropsAllowed && timeTillSpawn <= 0 && Items.Count < ITEM_LIMIT)
            {
                // Get a spawn position
                Vector3 spawnPosition = arena.GetItemSpawnPosition(random);

                // Spawn a random item
                Item item = new Rock(spawnPosition);
                item.Initialize();
                Items.Add(item);

                // Add the item to the world.
                entityManager.AddEntity(item);
                physics.AddPhysicsEntity(item.GetBox());

                timeTillSpawn = random.Next(MAX_FREQUENCY - MIN_FREQUENCY) + MIN_FREQUENCY;
            }

            // Remove any items outside of the arena
            for (int i = 0; i < Items.Count; i++)
            {
                Item item = Items[i];
                if (arena.GetBoundingBox().Contains(item.GetBox().Position) == ContainmentType.Disjoint)
                {
                    // Remove the item from the physics, the entity manager, and the items list.
                    Items.Remove(item);
                    entityManager.RemoveEntity(item);
                    physics.RemovePhysicsEntity(item.GetBox());
                    i--;
                }
            }
        }

        public void Add(Item item)
        {
            Items.Add(item);
            entityManager.AddEntity(item);
            physics.AddPhysicsEntity(item.GetBox());
        }

        public List<Item> GetItems()
        {
            return this.Items;
        }


    }
}
