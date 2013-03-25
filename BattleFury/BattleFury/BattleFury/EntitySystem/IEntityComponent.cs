using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFury.EntitySystem
{
    /// <summary>
    /// Interface which all components will need to implement.
    /// </summary>
    public interface IEntityComponent
    {
        /// <summary>
        /// The component's ID. Used when getting this component from inside the parent entity.
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Called after initialize. Gathers references to other components.
        /// </summary>
        void Start();
    }
}
