using BattleFury.EntitySystem;
using BattleFury.Components;
using BattleFury.Entities.Physics;
using BattleFury.Entities.Arenas;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BattleFury.Entities.Items
{

    /// <summary>
    /// Spawns items in the arena at random positions and random times. 
    /// Also manages the manual spawning of any items.
    /// </summary>
    public class ItemManager : Entity
    {


        private ItemManagerComponent itemManager;

        public ItemManager(EntityManager entityManager, PhysicsSimulator physics, Arena arena, bool itemDropsAllowed)
        {
            if (itemDropsAllowed)
            {
                ItemSpawnComponent itemSpawner = new ItemSpawnComponent(this, arena);
                this.AttachComponent(itemSpawner);
            }

            this.itemManager = new ItemManagerComponent(this, entityManager, physics, arena);
            this.AttachComponent(itemManager);
        }
       
        public void AddItem(Item item)
        {
            itemManager.Add(item);
        }
        
        public List<Item> GetItems()
        {
            return itemManager.GetItems();
        }

    }
}
