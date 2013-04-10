using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BattleFury.Components.Characters;
using BattleFury.Settings;

namespace BattleFury.Components.Movement
{
    public class JumpComponent : Component
    {
        private int JumpHeight;

        private PlayerIndex controllingPlayer;

        private BepuPhysicsComponent bepuPhysicsComponent;

        public JumpComponent(Entity parent, int jumpHeight)
            : base(parent, "JumpComponent")
        {
            this.JumpHeight = jumpHeight;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            controllingPlayer = ((CharacterInformationComponent) Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            bepuPhysicsComponent = ((BepuPhysicsComponent) Parent.GetComponent("BepuPhysicsComponent"));
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            if (GameplayBindings.IsJump(controllingPlayer))
            {
                bepuPhysicsComponent.Box.LinearVelocity = new Vector3(0, JumpHeight, 0);
            }
        }
    }
}
