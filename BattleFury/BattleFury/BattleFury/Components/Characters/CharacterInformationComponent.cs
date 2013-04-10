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

        public PlayerIndex PlayerIndex;

        public CharacterInformationComponent(Entity parent, PlayerIndex playerIndex, int team) 
            : base(parent, "CharacterInformationComponent")
        {
            this.PlayerIndex = playerIndex;
            this.Team = team;
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
    }
}
