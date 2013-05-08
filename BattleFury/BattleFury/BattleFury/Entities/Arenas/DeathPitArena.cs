using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BEPUphysics.Entities.Prefabs;
using BattleFury.EntitySystem;
using BattleFury.Entities.Physics;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Entities.Arenas
{
    /// <summary>
    /// A plain arena with no entities. Just a single base platform.
    /// </summary>
    public class DeathPitArena : Arena
    {

        /// <summary>
        /// List of spawn positions.
        /// </summary>
        private List<Vector3> spawnPositions;

        /// <summary>
        /// Next place to spawn.
        /// </summary>
        private int currentSpawnIndex = 0;

        /// <summary>
        /// The Bounding box. Falling outside of this will destroy the
        /// character or item.
        /// </summary>
        private BoundingBox boundingBox;

        /// <summary>
        /// Constructs the arena. Also constructs several platforms and physics elements
        /// which need to get added to the EntityManager and PhysicsSimulator.
        /// </summary>
        /// <param name="entityManager"></param>
        /// <param name="physicsSimulator"></param>
        public DeathPitArena(EntityManager entityManager, PhysicsSimulator physicsSimulator)
        {
            // Create the platforms.
            Platform ground = new Platform(new Vector3(0, 0, 0), 50.0f, 3.0f, 5.0f);
            Platforms.Add(ground);
            entityManager.AddEntity(ground);
            physicsSimulator.AddPhysicsEntity(ground.GetBox());

            // Create the Spawn Positions
            spawnPositions = new List<Vector3>();
            spawnPositions.Add(new Vector3(-15, 10, 0));
            spawnPositions.Add(new Vector3(-5, 15, 0));
            spawnPositions.Add(new Vector3(5, 15, 0));
            spawnPositions.Add(new Vector3(15, 10, 0));

            // Create the Bounding box.
            boundingBox = new BoundingBox(new Vector3(-70, -20, -20), new Vector3(70, 60, 20));
        }

        public override Vector3 GetCharacterSpawnPosition()
        {
            Vector3 toReturn = spawnPositions[currentSpawnIndex];
            currentSpawnIndex++;
            if (currentSpawnIndex == spawnPositions.Count)
            {
                currentSpawnIndex = 0;
            }
            return toReturn;
        }

        public override BoundingBox GetBoundingBox()
        {
            return boundingBox;
        }

        public override Vector3 GetItemSpawnPosition(Random random)
        {
            Vector3 spawnPosition = new Vector3(random.Next(49) - 24, random.Next(10) + 5, 0);
            return spawnPosition;
        }
    }
}
