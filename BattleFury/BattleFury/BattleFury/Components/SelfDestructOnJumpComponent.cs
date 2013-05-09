using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components.Movement;
using BattleFury.Entities;
using BattleFury.Entities.Items;
using BattleFury.Entities.Characters;

namespace BattleFury.Components
{
    public class SelfDestructOnJumpComponent : Component
    {

        private JumpComponent jumpComponent;

        private Environment environment;

        /// <summary>
        /// True if the item is marked for self destruct on the next frame.
        /// This is done so that if 2 items collide that want to self destruct,
        /// an items waits and gives another item a chance.
        /// 
        /// In other words, there's a 1 frame lag between colliding and removal.
        /// Hopefully this doesn't cause too many problems.
        /// </summary>
        private bool markedForRemoval;

        public SelfDestructOnJumpComponent(Entity parent, Environment environment, Character attackingCharacter)
            : base(parent, "SelfDestructOnJumpComponent")
        {
            this.markedForRemoval = false;
            this.environment = environment;
            this.jumpComponent = (JumpComponent) attackingCharacter.GetComponent("JumpComponent");
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (markedForRemoval)
            {
                environment.ItemManager.Remove((Item)Parent);
            }

            if (jumpComponent.JumpedThisFrame)
            {
                markedForRemoval = true;
            }
        }
    }
}
