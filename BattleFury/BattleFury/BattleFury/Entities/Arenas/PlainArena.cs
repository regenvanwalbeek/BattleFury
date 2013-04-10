using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.Entities.Arenas
{
    public class PlainArena : Arena
    {
        private List<Vector3> spawnPositions;

        private int currentSpawnIndex = 0;


        public PlainArena()
        {
            spawnPositions = new List<Vector3>();
            spawnPositions.Add(new Vector3(-4, 4, 0));
            spawnPositions.Add(new Vector3(0, 16, 0));
            spawnPositions.Add(new Vector3(4, 16, 0));
            spawnPositions.Add(new Vector3(8, 4, 0));
        }

        public override Microsoft.Xna.Framework.Vector3 GetSpawnPosition()
        {
            Vector3 toReturn = spawnPositions[currentSpawnIndex];
            currentSpawnIndex++;
            if (currentSpawnIndex == spawnPositions.Count)
            {
                currentSpawnIndex = 0;
            }
            return toReturn;
        }
    }
}
