using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Components
{
    /// <summary>
    /// Component which provides access to an Entity of type T. 
    /// This class will not update any data or gamestate. It merely provides
    /// easy data sharing between entities.
    /// </summary>
    /// <typeparam name="T">The entity type to share.</typeparam>
    public class EntityProvider<T>  where T : Component
    {
        /// <summary>
        /// The entity to share.
        /// </summary>
        public T Entity; 

        public EntityProvider(T entity) {
            this.Entity = entity;
        }

     
    }
}
