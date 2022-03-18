using UnityEngine;
using DG.Tweening;
using Arkanot.Input;

namespace Arkanot.GameLogic.Actors
{
    public class Paddle : MonoBehaviour
    {
        #region private fields
        private GameController gameController;
        private Camera mainCamera;
        private Rigidbody2D rigidBody2D;
        private Vector2 targetPosition;
        private float speed=20;
        private float currentWidth=1;
        private TouchToWorld touchInput;
        #endregion
        
        #region private methods
        private void Awake()
        {
            mainCamera = Camera.main;
            rigidBody2D = GetComponent<Rigidbody2D>();
            touchInput = new TouchToWorld(mainCamera);
        }

        private void Update()
        {
            touchInput.ProcessInput();
        }

        private void FixedUpdate()
        {
            if (touchInput.IsTouching)
                targetPosition = touchInput.LastKnownPosition;

            //dont move panel on the y axis of course
            targetPosition.y = rigidBody2D.position.y;

            rigidBody2D.MovePosition(Vector2.MoveTowards(rigidBody2D.position, targetPosition, Time.fixedDeltaTime * speed));
        }

        private void RefreshWidth()
        {
            transform.DOScaleX(currentWidth,0.25f);
        }
        #endregion

        #region public methods
        public void SetGameController(GameController gameController)
        {
            this.gameController = gameController;
        }

        public float ReflectionFactor(Vector2 ballPosition)
        {
            return (ballPosition.x - rigidBody2D.position.x) / currentWidth;
        }

        public void SetCurrentWidth(float width)
        {
            currentWidth = width;
            RefreshWidth();
        }
        #endregion
    }
}

