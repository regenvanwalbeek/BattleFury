using BattleFury.Entities.Arenas;
using BattleFury.Entities.Items;
using BattleFury.Entities.Physics;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using BattleFury.Entities;

namespace BattleFury.Components
{
    public class ItemSpawnComponent : Component
    {
        private System.Random random;

        /// <summary>
        /// Minimum amount of time in which items should be spawned.
        /// </summary>
        private const int MIN_FREQUENCY = 15000;

        /// <summary>
        /// Maximum amount of time in which items should be spawned.
        /// </summary>
        private const int MAX_FREQUENCY = 30000;

        /// <summary>
        /// Number of milliseconds until an item spawns.
        /// </summary>
        private int timeTillSpawn;

        /// <summary>
        /// Limit on the number of items that can be randomly spawned. Note that items can still be added manually after random spawning
        /// </summary>
        private const int ITEM_LIMIT = 1;

        private Environment environment;

        private ItemManagerComponent itemManagerComponent;

        public ItemSpawnComponent(ItemManager parent, Environment environment) : base(parent, "ItemSpawnComponent")
        {
            this.random = new System.Random();
            this.timeTillSpawn = random.Next(MAX_FREQUENCY - MIN_FREQUENCY) + MIN_FREQUENCY;
            this.environment = environment;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            // Get the Item Manager.
            this.itemManagerComponent = (ItemManagerComponent) Parent.GetComponent("ItemManagerComponent");
        }

        public override void Update(GameTime gameTime)
        {

            // Spawn items at random times.
            timeTillSpawn -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeTillSpawn <= 0 && itemManagerComponent.NumItems() < ITEM_LIMIT)
            {
                // Get a spawn position
                Vector3 spawnPosition = environment.Arena.GetItemSpawnPosition(random);

                // Spawn a random item
                Item item = new Rock(spawnPosition, environment);
                item.Initialize();
                itemManagerComponent.Add(item);

                timeTillSpawn = random.Next(MAX_FREQUENCY - MIN_FREQUENCY) + MIN_FREQUENCY;
            }

            
        }


    }
}
