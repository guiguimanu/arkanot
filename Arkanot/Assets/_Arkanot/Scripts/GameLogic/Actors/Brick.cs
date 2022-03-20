using UnityEngine;
using DG.Tweening;
using Arkanot.GameLogic.PowerUps;

namespace Arkanot.GameLogic.Actors
{
    public class Brick : MonoBehaviour
    {
        #region private fields
        private BrickList brickList;

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private SpriteRenderer spriteRendererShadow;
        [SerializeField]
        private GameObject prefabOnHit;
        [SerializeField]
        private ParticleSystem psOnDie;
        [SerializeField]
        private Color[] lifeColors;
        [SerializeField]
        private int lives;

        #endregion
        
        #region properties

        public PowerUpBrick PowerUp { get; private set; }

        #endregion

        #region private methods

        private void Awake()
        {
            RefreshColor();

            brickList = GetComponentInParent<BrickList>();

            PowerUp = GetComponent<PowerUpBrick>();
        }

        private void RefreshColor()
        {
            spriteRenderer.DOKill();
            spriteRenderer.color = lifeColors[lives-1];
        }

        private void TweenColor()
        {
            spriteRenderer.DOKill();
            spriteRenderer.DOColor(lifeColors[lives - 1],0.5f);
        }

        private void TweenHit(Vector2 v2Dir)
        {
            spriteRenderer.transform.DOKill();
            spriteRenderer.transform.DOLocalMove(-v2Dir * 0.1f, 0.1f).OnComplete(() =>
            {
                spriteRenderer.transform.localPosition = Vector2.zero;
            });
        }

        private void SetParticleColor()
        {
            ParticleSystem.MainModule psMain = psOnDie.main;

            psMain.startColor = spriteRenderer.color;
        
        }

        private void KillBrick()
        {
            brickList.RemoveBrick(this);

            GetComponent<Collider2D>().enabled = false;
            SetParticleColor();
            psOnDie.Play();

            spriteRenderer.DOKill();
            spriteRendererShadow.DOKill();

            spriteRendererShadow.DOFade(0, 0.2f).SetDelay(0.1f);
            spriteRenderer.DOFade(0, 0.25f).SetDelay(0.1f).OnComplete(() =>
            {
                //1.5 to let the particle play
                Destroy(gameObject,1.5f);
            });
        }
    
        private void OnHitParticles(Vector2 v2Dir, Vector2 collisionPos)
        {
            float angleOffset = -45;
            float angle = Mathf.Atan2(v2Dir.y, v2Dir.x) * Mathf.Rad2Deg;
        
            Quaternion rotation = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);
            ParticleSystem ps = Instantiate(prefabOnHit, collisionPos, rotation).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psMain = ps.main;
            psMain.startColor = spriteRenderer.color;

            Destroy(ps.gameObject, 1.5f);//Particle system life is 1.5
        }

        private bool IsDead()
        {
            return lives <= 0;
        }

        #endregion

        #region public methods

        public void GetHit(Vector2 v2Dir,Vector2 collisionPos)
        {
            if (IsDead())
                return;

            OnHitParticles(v2Dir, collisionPos);
            TweenHit(v2Dir);
            lives--;
            
            if(IsDead())
            {
                KillBrick();
            }
            else
            {
                TweenColor();
            }
        }

        #endregion
    }

}

