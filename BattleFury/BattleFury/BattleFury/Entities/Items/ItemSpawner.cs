using BattleFury.EntitySystem;
using BattleFury.Components;
using BattleFury.Entities.Physics;
using BattleFury.Entities.Arenas;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Entities.Items
{

    /// <summary>
    /// Spawns items in the arena at random positions and random times.
    /// </summary>
    public class ItemSpawner : Entity
    {

        private ItemSpawnComponent itemSpawner;

        public ItemSpawner(EntityManager entityManager, PhysicsSimulator physics, Arena arena)
        {
            this.itemSpawner = new ItemSpawnComponent(this, entityManager, physics, arena);
            this.AttachComponent(itemSpawner);
        }


    }
}
