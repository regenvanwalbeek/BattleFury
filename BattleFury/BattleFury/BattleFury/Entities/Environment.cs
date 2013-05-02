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

        public ItemManager ItemManager;

        /// <summary>
        /// Initialize the Game Environment
        /// </summary>
        /// <param name="arena">The arena to battle in.</param>
        /// <param name="spawner">The item spawner.</param>
        public Environment(Arena arena)
        {
            Characters = new List<Character>();
            this.Arena = arena;
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

        public void SetItemManager(ItemManager itemManager)
        {
            this.ItemManager = itemManager;
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

            foreach (Item item in ItemManager.GetItems()){
                IEntityComponent component = item.GetComponent(componentID);
                if (component != null){
                    components.Add( (T) component );
                }
            }
            return components;
        }

        public List<Entity> GetEntitiesOfType<T>()
        {
            throw new NotImplementedException(); // TODO this is just a warning. you can remove this line. but use at own risk.
            List<Entity> entities = new List<Entity>();

            for (int i = 0; i < Characters.Count; i++)
            {
                if ( typeof(T) == this.Characters[i].GetType()){
                    entities.Add(Characters[i]);
                }
            }

            List<Item> items = ItemManager.GetItems();
            for (int i = 0; i < items.Count; i++)
            {
                if (typeof(T) == items[i].GetType())
                {
                    entities.Add(items[i]);
                }
            }

            List<Platform> platforms = Arena.Platforms;
            for (int i = 0; i < platforms.Count; i++)
            {
                if (typeof(T) == platforms[i].GetType())
                {
                    entities.Add(platforms[i]);
                }
            }

            return entities;
        }
    }
}
