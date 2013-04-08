using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BattleFury.Components.CameraComponents;

namespace BattleFury.Components.Animated
{
    public abstract class BasicModelComponent : DrawableComponent
    {
        private Model model;

        private ViewProjectionComponent cameraComponent;

        public BasicModelComponent(Entity parent, Model model)
            : base(parent, "BasicModelComponent")
        {
            this.model = model;
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = cameraComponent.Projection;
                    effect.View = cameraComponent.View;
                    effect.World = GetWorld() * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }

        public override void Start()
        {
            // Gather reference to Camera Drawing this entity
            cameraComponent = (ViewProjectionComponent) ((DrawableByCameraComponent) Parent.GetComponent("DrawableByCameraComponent")).Camera.GetComponent("ViewProjectionComponent");
        }

        public override void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Gets the model's world matrix
        /// </summary>
        /// <returns>The model's world</returns>
        public virtual Matrix GetWorld()
        {
            return Matrix.Identity;
        }

    }
}
