using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Components.Characters
{
    /// <summary>
    /// A class which tracks vitality information for a character.
    /// </summary>
    public class VitalityComponent : Component
    {
        public int LivesLeft;

        public float RageMeter;

        public bool IsAlive = true;

        public bool IsKO = false;

        public VitalityComponent(Entity parent, int lives) : 
            base(parent, "VitalityComponent")
        {
            this.LivesLeft = lives;
            this.RageMeter = 100.0f;
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

        public void Damage(float damageAmount)
        {
            RageMeter -= damageAmount;
            if (RageMeter < 0)
            {
                RageMeter = 0;
            }
        }


  
    }
}
