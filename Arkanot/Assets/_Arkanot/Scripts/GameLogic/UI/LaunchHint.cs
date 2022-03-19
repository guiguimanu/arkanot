using UnityEngine;
using DG.Tweening;

namespace Arkanot.GameLogic.UI
{
    public class LaunchHint : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        private Animator anim;

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
