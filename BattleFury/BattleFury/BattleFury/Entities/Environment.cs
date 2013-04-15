using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.Entities.Characters;
using BattleFury.Entities.Arenas;

namespace BattleFury.Entities
{
    /// <summary>
    /// Data structure used for passing around information about the world which 
    /// other entities may need references to.
    /// </summary>
    public class Environment
    {
        public List<Character> Characters { get; private set; }

        public Arena Arena { get; private set; }

        public Environment(Arena arena)
        {
            Characters = new List<Character>();
            this.Arena = arena;
        }

        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }
    }
}
