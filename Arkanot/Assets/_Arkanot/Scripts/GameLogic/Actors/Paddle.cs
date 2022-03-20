using UnityEngine;
using DG.Tweening;
using Arkanot.Input;

namespace Arkanot.GameLogic.Actors
{
    public class Paddle : MonoBehaviour
    {
        #region private fields
        private Camera mainCamera;
        private Rigidbody2D rigidBody2D;
        private Vector2 targetPosition;
        private float speed=20;

        [SerializeField]
        private ParticleSystem psOnDie;
        [SerializeField]
        private Transform xfSprites;

        #endregion

        #region properties
        public TouchToWorld TouchInput { get; private set; }
        public float CurrentWidth { get; private set; }

        #endregion


        #region private methods
        private void Awake()
        {
            mainCamera = Camera.main;
            rigidBody2D = GetComponent<Rigidbody2D>();
            TouchInput = new TouchToWorld(mainCamera);
            CurrentWidth = 1;
        }

        private void Update()
        {
            TouchInput.ProcessInput();
        }

        private void FixedUpdate()
        {
            if (TouchInput.IsTouching)
                targetPosition = TouchInput.LastKnownPosition;

            //dont move panel on the y axis of course
            targetPosition.y = rigidBody2D.position.y;

            rigidBody2D.MovePosition(Vector2.MoveTowards(rigidBody2D.position, targetPosition, Time.fixedDeltaTime * speed));
        }

        private void RefreshWidth()
        {
            transform.DOScaleX(CurrentWidth, 0.25f);
        }
        #endregion

        #region public methods

        public float ReflectionFactor(Vector2 ballPosition)
        {
            return (ballPosition.x - rigidBody2D.position.x) / CurrentWidth;
        }

        public void SetCurrentWidth(float width)
        {
            CurrentWidth = width;
            RefreshWidth();
        }

        public void OverrideSpeed(float speed)
        {
            this.speed = speed;
        }

        public void Implode()
        {
            xfSprites.DOScale(1.5f, 0.25f).SetDelay(1.0f).OnComplete(() =>
            {
                xfSprites.DOScale(0, 0.25f).OnComplete(() =>
                {
                    psOnDie.Play();
                });
            });
        }

        #endregion
    }
}

