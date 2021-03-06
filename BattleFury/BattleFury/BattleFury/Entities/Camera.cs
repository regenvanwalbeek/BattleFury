﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components.CameraComponents;
using Microsoft.Xna.Framework;


namespace BattleFury.Entities
{
    /// <summary>
    /// Camera class. Contains the View and Projection matrices necessary for drawing.
    /// 
    /// Just constructing this camera will result in a fixed camera entity. However,
    /// components which provide camera movement may be attached, such as a BattleCameraComponent
    /// or DebugCameraComponent.
    /// </summary>
    public class Camera : Entity
    {
        private ViewProjectionComponent viewProjection;

        private Vector3 startPosition;
        private Vector3 startTarget;
        private Vector3 startUp;

        public Camera(Vector3 position, Vector3 target, Vector3 up)
        {
            this.viewProjection = new ViewProjectionComponent(this, position, target, up);
            this.AttachComponent(viewProjection);

            startPosition = position;
            startTarget = target;
            startUp = up;
        }

        public Matrix GetView()
        {
            return viewProjection.View;
        }

        public Matrix GetProjection()
        {
            return viewProjection.Projection;
        }

        public void ResetCamera()
        {
            viewProjection.Reset();
        }
    }
}
