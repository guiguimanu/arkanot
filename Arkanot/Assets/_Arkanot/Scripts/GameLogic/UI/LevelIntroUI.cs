using UnityEngine;
using System;
using DG.Tweening;
using TMPro;
public class LevelIntroUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private TextMeshProUGUI txtLevel;
    [SerializeField]
    private Transform xfMaskOrigin;
    [SerializeField]
    private Transform xfMaskTarget;
    [SerializeField]
    private Transform xfMask;
    
    public void ShowIntro(int level, Action OnComplete)
    {
        canvasGroup.alpha = 1;


        txtLevel.text = $"Level {level}";

        xfMask.transform.position = xfMaskOrigin.position;

        xfMask.DOMove(xfMaskTarget.transform.position, 1.5f).SetEase(Ease.Linear)
            .SetDelay(0.1f).OnComplete(()=>
        {
            canvasGroup.DOFade(0, 0.5f).SetDelay(0.5f).OnComplete(()=>OnComplete());
        });

    }

    
}
