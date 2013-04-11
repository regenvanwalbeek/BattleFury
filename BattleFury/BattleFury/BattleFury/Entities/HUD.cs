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

        public HUD(List<Character> characterList, SpriteFont rageFont, SpriteFont livesFont)
        {
            this.drawableHUDComponent = new DrawableHUDComponent(this, characterList, rageFont, livesFont);
            AttachComponent(drawableHUDComponent);
        }
    }
}
