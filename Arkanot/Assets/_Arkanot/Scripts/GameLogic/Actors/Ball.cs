
using UnityEngine;
using DG.Tweening;

namespace Arkanot.GameLogic.Actors
{
    public class Ball : MonoBehaviour
    {
        #region private variables
        private const float BASE_SPEED = 8.0f;
        private GameController gameController;
        private Rigidbody2D rigidBody2D;
        private float speed = BASE_SPEED;

        [SerializeField]
        private ParticleSystem psOnDie;
        [SerializeField]
        private ParticleSystem psOnSpawn;
        [SerializeField]
        private GameObject goSprites;

        #endregion

        #region private methods

        private void Awake()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
            goSprites.transform.localScale = Vector3.zero;
            goSprites.transform.DOScale(1, 0.5f);
            psOnSpawn.Play();
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject== gameController.Paddle.gameObject)
            {
                Vector2 newDir = new Vector2(gameController.Paddle.ReflectionFactor(rigidBody2D.position), 1).normalized;

                rigidBody2D.velocity = newDir * speed;
            }
            else if (collision.gameObject.TryGetComponent(out Brick brick))
            {
                brick.GetHit(rigidBody2D.velocity.normalized, collision.GetContact(0).point);

                if (brick.PowerUp !=  null)
                    gameController.AddPowerUp(brick.PowerUp);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject==gameController.GoVoid)
            {
                gameController.OnBallDeath();
            }
        }

        private bool IsStuck()
        {
            return rigidBody2D.velocity.y == 0;
        }

        private void Nudge()
        {
            Vector2 newDir = new Vector2(rigidBody2D.velocity.normalized.x, -1);
            rigidBody2D.velocity = newDir * speed;

        }

        #endregion

        #region public methods

        public void SetGameController(GameController gameController)
        {
            this.gameController = gameController;
        }

        public void Launch()
        {
            rigidBody2D.isKinematic = false;
            rigidBody2D.velocity = Vector2.up * speed;
        }

        public void SetSpeedMultiplier(float mutliplier)
        {
            speed = mutliplier * BASE_SPEED;
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * speed;
        }

        public void FollowPaddle()
        {
            Vector3 pos = gameController.Paddle.transform.position;
            pos.y += 0.4f;

            transform.position = pos;
        }

        public void StopAndImplode()
        {
            rigidBody2D.velocity = Vector2.zero;

            goSprites.transform.DOScale(1.5f, 0.25f).SetDelay(1.0f).OnComplete(()=>
            {
                goSprites.transform.DOScale(0, 0.25f).OnComplete(() =>
                {
                    psOnDie.Play();
                });
            });
        }

        /// <summary>
        /// Edge case where the ball stays at 0 y velocity forever
        /// This happens when it hits the roof with a low y velocity
        /// Solution? Nudge it!
        /// </summary>
        public void NudgeBallIfStuck()
        {
            if (IsStuck())
                Nudge();
        }


        #endregion
    }
}

