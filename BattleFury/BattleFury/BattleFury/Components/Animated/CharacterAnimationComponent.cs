using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BattleFury.EntitySystem;
using BattleFury.AnimationEngine;

namespace BattleFury.Components.Animated
{
    /// <summary>
    /// This will be a specific implementation of an animation component
    /// </summary>
    public class CharacterAnimationComponent : AnimationComponent
    {

        public CharacterAnimationComponent(Entity parent)
            : base(parent, "CharacterAnimationComponent")
        {

        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void  Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            throw new NotImplementedException();
        }
    }
}
