using System;
using BattleFury.EntitySystem;

namespace BattleFury.Components.CameraComponents
{
    /// <summary>
    /// Controls the camera movement for the camera focused on the battle arena.
    /// The camera will track entities in the arena and zoom in and out during
    /// the battle.
    /// 
    /// Requires access to a ViewProjectionComponent in the parent entity.
    /// </summary>
    public class BattleCameraComponent : Component
    {

        ViewProjectionComponent cameraViewProjection;

        public BattleCameraComponent(Entity parent) : base(parent, "BattleCaemraComponent")
        {
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            cameraViewProjection = (ViewProjectionComponent) Parent.GetComponent("ViewProjectionComponent");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // TODO update the view and projection of the parent camera based on the positons of entities in the playing field.
            throw new NotImplementedException();
        }
    }
}
