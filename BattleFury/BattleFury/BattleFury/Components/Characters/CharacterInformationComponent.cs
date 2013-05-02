using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Components.Characters
{


    public class CharacterInformationComponent : Component
    {

        public int Team;

        /// <summary>
        /// Place the character finished in. -1 if character is still alive.
        /// </summary>
        public int Place = -1;

        public PlayerIndex PlayerIndex;

        public Color Color;

        public CharacterInformationComponent(Entity parent, PlayerIndex playerIndex, int team, Color color) 
            : base(parent, "CharacterInformationComponent")
        {
            this.PlayerIndex = playerIndex;
            this.Team = team;
            this.Color = color;
        }



        public override void Initialize()
        {
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void setPlace(int place)
        {
            this.Place = place;
        }
    }
}
