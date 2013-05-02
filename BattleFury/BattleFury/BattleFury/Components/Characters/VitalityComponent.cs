using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BattleFury.Input;

namespace BattleFury.Components.Characters
{
    /// <summary>
    /// A class which tracks vitality information for a character.
    /// </summary>
    public class VitalityComponent : Component
    {
        public int LivesLeft;

        public float RageMeter { get; private set; }

        public bool IsAlive = true;

        public bool IsKO = false;

        private const int RUMBLE_TIME = 100;
        int[] timeTillRumbleOff = new int[4];

        private CharacterInformationComponent characterInformationComponent;

        public VitalityComponent(Entity parent, int lives) : 
            base(parent, "VitalityComponent")
        {
            this.LivesLeft = lives - 1;
            this.RageMeter = 100.0f;
            for (int i = 0; i < 4; i++)
            {
                timeTillRumbleOff[i] = 0;
            }
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            characterInformationComponent = (CharacterInformationComponent) Parent.GetComponent("CharacterInformationComponent");
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < 4; i++)
            {
                if (timeTillRumbleOff[i] >= 0)
                {
                    timeTillRumbleOff[i] -= gameTime.ElapsedGameTime.Milliseconds;
                    if (timeTillRumbleOff[i] < 0)
                    {
                        InputState.Rumble(characterInformationComponent.PlayerIndex, 0, 0);
                    }
                }
            }
        }

        public void Damage(float damageAmount)
        {
            
            if (characterInformationComponent != null)
            {
                System.Console.WriteLine("Rumbling");
                InputState.Rumble(characterInformationComponent.PlayerIndex, 1, 1);
                timeTillRumbleOff[((int) characterInformationComponent.PlayerIndex) - 1] = RUMBLE_TIME;
            }
            
    
            RageMeter -= damageAmount;
            if (RageMeter < 0)
            {
                RageMeter = 0;
            }
        }

        public void ResetRageMeter()
        {
            this.RageMeter = 100.0f;
        }


  
    }
}
