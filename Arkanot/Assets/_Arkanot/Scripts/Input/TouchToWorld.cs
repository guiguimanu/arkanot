using UnityEngine;
using UInput = UnityEngine.Input;
using System.Collections;

namespace Arkanot.Input
{
    public class TouchToWorld 
    {
        private bool overridingInput;

        private Camera camera;

        public Vector2 LastKnownPosition { get; private set; }

        public bool IsTouching { get; private set; }

        /// <summary>
        /// Keeps track of the touch position in world space, given a camera
        /// </summary>
        /// <param name="camera">The camera to convert to world space with</param>
        public TouchToWorld(Camera camera)
        {
            this.camera = camera;
        }
        
        public void ProcessInput()
        {
            if (overridingInput)
                return;

            if (UInput.touchCount == 0)
            {
                IsTouching = false;
                return;
            }

            LastKnownPosition = camera.ScreenToWorldPoint(UInput.GetTouch(0).position);
            IsTouching = true;
        }

        #region methods for testing
        public void StopOverride()
        {
            IsTouching = false;
            overridingInput = false;
        }
        public void OverideInput(Vector2 worldPosition)
        {
            IsTouching = true;
            overridingInput = true;
            LastKnownPosition = worldPosition;
        }
        #endregion
    }

}
