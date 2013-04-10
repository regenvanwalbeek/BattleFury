using System;
using BattleFury.Components.Characters;
using BattleFury.EntitySystem;
using BattleFury.Components;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;

namespace BattleFury.Entities.Characters
{
    /// <summary>
    /// Game character used for battling.
    /// </summary>
    public abstract class Character : Entity
    {

        protected VitalityComponent vitalityComponent;

        protected BepuPhysicsComponent bepuPhysicsComponent;

        protected CharacterInformationComponent characterInformationComponent;   

        public Character(string id, int lives, Box box, PlayerIndex controllingPlayer, int team) : base(id){
            vitalityComponent = new VitalityComponent(this, lives);
            this.AttachComponent(vitalityComponent);

            characterInformationComponent = new CharacterInformationComponent(this, controllingPlayer, team);
            this.AttachComponent(characterInformationComponent);

            // Create the physics Component. 
            // This will actually probably just translate into one box for jumps/gravity/trajectories
            // and collision between characters.
            bepuPhysicsComponent = new BepuPhysicsComponent(this, box);
            this.AttachComponent(bepuPhysicsComponent);
        }

        public Box GetBox()
        {
            return bepuPhysicsComponent.Box;
        }

    }
}
