using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components.Characters;

namespace BattleFury.Components
{
    public class HealComponent : Component
    {

        private float totalHealing;

        private float timeLeft;

        private float totalTime;

        private VitalityComponent vitalityComponent;

        /// <summary>
        /// Heals an entity over time
        /// </summary>
        /// <param name="parent">The parent entity</param>
        /// <param name="healAmount">The amount to heal the parent entity</param>
        /// <param name="time">Number of ms to spend healing</param>
        public HealComponent(Entity parent, float healing, float time)
            : base(parent, "HealComponent")
        {
            this.totalHealing = healing;
            this.totalTime = time;
            this.timeLeft = time;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            vitalityComponent = (VitalityComponent) Parent.GetComponent("VitalityComponent");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float elapsed = gameTime.ElapsedGameTime.Milliseconds;
            this.timeLeft -= elapsed;
            if (timeLeft >= 0)
            {
                float healAmount = (totalHealing / totalTime) * elapsed;
                vitalityComponent.Heal(healAmount);
            }
            else
            {
                this.Parent.DetachComponent(this);
            }
        }
    }
}
