using UnityEngine;
using DG.Tweening;

namespace Arkanot.GameLogic.UI
{
    public class LaunchHint : MonoBehaviour
    {
        private Animator anim;

        [SerializeField]
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            anim = GetComponent<Animator>();

        }

        public void ShowHint()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1, 0.2f);
            anim.enabled = true;
        }

        public void FollowPaddle(Transform xfPaddle)
        {
            Vector2 pos = transform.position;
            pos.x = xfPaddle.position.x;
            transform.position = pos;
        }

        public void HideHint()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0, 0.2f).OnComplete(()=>
            {
                anim.enabled = false;
            });
        }
    }

}
