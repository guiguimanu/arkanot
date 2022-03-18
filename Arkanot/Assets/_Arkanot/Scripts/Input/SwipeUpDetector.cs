using UnityEngine;
using UInput = UnityEngine.Input;
using System.Collections;

namespace Arkanot.Input
{
    public class SwipeUpDetector
    {
        private Vector2 v2FirstPressPos;
        private Vector2 v2SecondPressPos;
        private Vector2 v2CurrentSwipe;
        private float fAccumulator;
        private readonly float fMaginitudeRequirement;
        private readonly float fSwipeTimeLimit;

        /// <summary>
        /// Detect a swipe up based on screen height and time limit
        /// </summary>
        /// <param name="swipeToScreenHeightRatio"> The desired minimum swipe length as a ratio of the screen height </param>
        /// <param name="timeLimit"> The maximum time acceptable for a swipe detection in seconds</param>
        public SwipeUpDetector(float swipeToScreenHeightRatio, float timeLimit)
        {
            fMaginitudeRequirement = Screen.height * swipeToScreenHeightRatio;
            fSwipeTimeLimit = timeLimit;
            Reset();
        }

        #region private methods

        private void OnTouchBegin(Touch touch)
        {
            v2FirstPressPos = new Vector2(touch.position.x, touch.position.y);
            fAccumulator = 0;
        }

        private bool OnTouchMoved(Touch touch)
        {
            fAccumulator += Time.deltaTime;

            v2SecondPressPos = new Vector2(touch.position.x, touch.position.y);
            v2CurrentSwipe = new Vector3(v2SecondPressPos.x - v2FirstPressPos.x, v2SecondPressPos.y - v2FirstPressPos.y);

            if (v2CurrentSwipe.magnitude > fMaginitudeRequirement && fAccumulator < fSwipeTimeLimit)
            {
                v2CurrentSwipe.Normalize();

                if (v2CurrentSwipe.y > 0 && v2CurrentSwipe.x > -0.5f && v2CurrentSwipe.x < 0.5f)
                    return true;
            }

            return false;
        }

        #endregion

        #region public methods

        public void Reset()
        {
            v2FirstPressPos = new Vector2();
            v2SecondPressPos = new Vector2();
            v2CurrentSwipe = new Vector2();
            fAccumulator = 0;
        }
        
        /// <summary>
        /// Listen for the swipe up based on defined settings
        /// </summary>
        /// <returns></returns>
        public bool ListenForSwipeUp()
        {
            if (UInput.touchCount == 0)
                return false;

            bool swipeDetected = false;

            Touch touch = UInput.GetTouch(0);
        
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegin(touch);
                    break;
                case TouchPhase.Moved:
                    swipeDetected = OnTouchMoved(touch);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                case TouchPhase.Stationary:
                default:
                    Reset();
                    break;
            }

            return swipeDetected;
        }

        #endregion
    }

}
