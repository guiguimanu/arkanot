using UnityEngine;
using DG.Tweening;

namespace Arkanot.Home
{
    public class InfoPanel : MonoBehaviour
    {
        private RectTransform xfInfo;
        private Vector2 v2TargetInfoPos;

        [SerializeField]
        private CanvasGroup canvasGroup;

        private void Start()
        {
            xfInfo = canvasGroup.GetComponent<RectTransform>();
            v2TargetInfoPos = xfInfo.anchoredPosition;
        }

        public void ShowPanel()
        {
            xfInfo.DOKill();

            Vector2 offset = v2TargetInfoPos;
            offset.y -= 100;
            xfInfo.anchoredPosition = offset;
            xfInfo.DOAnchorPos(v2TargetInfoPos, 1.0f);

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, 0.25f);
            canvasGroup.transform.DOScale(1, 0.75f);
        }

        public void HidePanel()
        {
            Vector2 offset = v2TargetInfoPos;
            offset.y += 100;
            xfInfo.DOAnchorPos(offset, 1.0f);

            canvasGroup.DOFade(0, 0.25f).OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }
    }

}
