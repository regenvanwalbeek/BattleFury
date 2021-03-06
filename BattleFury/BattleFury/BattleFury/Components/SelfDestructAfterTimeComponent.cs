﻿using BattleFury.Entities.Items;
using BattleFury.EntitySystem;
using BattleFury.Entities;
using Microsoft.Xna.Framework;

namespace BattleFury.Components
{
    /// <summary>
    /// Self destructs an item after a given amount of time.
    /// </summary>
    public class SelfDestructAfterTimeComponent : Component
    {
        /// <summary>
        /// Number of milliseconds until the entity should self destruct.
        /// </summary>
        private int msTillSelfDestruct;

        private Environment environment;

        public System.EventHandler OnDestroy;

        public SelfDestructAfterTimeComponent(Item parent, Environment environment, int msTillSelfDestruct) 
            : base(parent, "SelfDestructAfterTimeComponent")
        {
            this.environment = environment;
            this.msTillSelfDestruct = msTillSelfDestruct;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
            // Remove the item after the specified number of milliseconds
            msTillSelfDestruct -= gameTime.ElapsedGameTime.Milliseconds;
            if (msTillSelfDestruct <= 0)
            {
                try
                {
                    environment.ItemManager.Remove((Item)Parent);
                    System.EventHandler handler = OnDestroy;
                    if (handler != null)
                    {
                        handler(this, null);
                    }
                    
                }
                catch (System.ArgumentException)
                {
                    // Ignore. Item has already been removed by another self destruct component.
                }
            }
        }
    }
}
