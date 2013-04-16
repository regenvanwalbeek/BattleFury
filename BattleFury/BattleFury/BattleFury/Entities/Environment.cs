using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.Entities.Characters;
using BattleFury.Entities.Arenas;
using BattleFury.Entities.Items;
using BattleFury.EntitySystem;

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

        public ItemSpawner Spawner;

        /// <summary>
        /// Initialize the Game Environment
        /// </summary>
        /// <param name="arena">The arena to battle in.</param>
        /// <param name="spawner">The item spawner.</param>
        public Environment(Arena arena, ItemSpawner spawner)
        {
            Characters = new List<Character>();
            this.Arena = arena;
            this.Spawner = spawner;
        }

        /// <summary>
        /// Used for adding initializing the characters in the environment.
        /// Each character should be added to the environment with this method.
        /// </summary>
        /// <param name="character">The character to add.</param>
        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }

        /// <summary>
        /// Gets all entities which contain a component with the specified ID.
        /// </summary>
        /// <param name="componentID">The id of the component to search for.</param>
        public List<T> GetEntitiesWithComponent<T>(String componentID)
        {
            List <T> components = new List<T>();

            foreach (Character character in Characters){
                IEntityComponent component = character.GetComponent(componentID);
                if (component != null){
                    components.Add( (T) component );
                }
            }

            foreach (Item item in Spawner.GetItems()){
                IEntityComponent component = item.GetComponent(componentID);
                if (component != null){
                    components.Add( (T) component );
                }
            }
            return components;
        }
    }
}
