using UnityEngine;
using UInput = UnityEngine.Input;
using System.Collections;

namespace Arkanot.Input
{
    public class TouchToWorld 
    {
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
            if (UInput.touchCount == 0)
            {
                IsTouching = false;
                return;
            }

            LastKnownPosition = camera.ScreenToWorldPoint(UInput.GetTouch(0).position);
            IsTouching = true;
        }
    
    }

}
