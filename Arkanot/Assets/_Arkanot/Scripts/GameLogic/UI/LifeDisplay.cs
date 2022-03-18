using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Arkanot.GameLogic.UI
{
    public class LifeDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image[] imgLives;

        [SerializeField]
        private Color colorLost;

        public void RemoveHeartAt(int LostIndex)
        {
            imgLives[LostIndex].transform.DOShakePosition(1.0f, 15.0f);
            imgLives[LostIndex].color = colorLost;
            imgLives[LostIndex].DOFade(0, 1.0f);
        
        }
    }
}
