using System.Collections.Generic;
using BattleFury.Components;
using BattleFury.Entities.Characters;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Entities
{
    public class HUD : Entity
    {
        private DrawableComponent drawableHUDComponent;

        public HUD(List<Character> characterList)
        {
            this.drawableHUDComponent = new DrawableHUDComponent(this, characterList);
            AttachComponent(drawableHUDComponent);
        }
    }
}
