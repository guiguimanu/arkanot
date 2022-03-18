using UnityEngine;
using DG.Tweening;
namespace Arkanot.GameLogic.PowerUps
{
    public class PowerUpBrick : MonoBehaviour
    {
        [SerializeField]
        private PowerUpEnum _powerUpType;

        [SerializeField]
        private Transform xfIconParents;

        public PowerUpEnum PowerUpType { get { return _powerUpType; } }


        public void OnGet()
        {
            xfIconParents.transform.DOScale(1.5f, 0.4f);

            xfIconParents.DOMoveY(transform.position.y + 2, 0.5f).OnComplete(()=>
            {
                xfIconParents.DOScale(0, 0.5f).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });
        }
    }
}

