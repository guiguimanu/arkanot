using UnityEngine;
using TMPro;
using DG.Tweening;
namespace Arkanot.GameLogic.UI
{
    public class GameOverPanel : MonoBehaviour
    {
        #region private variables
        [SerializeField]
        private Transform xfTarget;
        [SerializeField]
        private Transform xfOrigin;
        [SerializeField]
        private TextMeshProUGUI txtText;
        [SerializeField]
        private Color colorWin;
        [SerializeField]
        private Color colorLose;
        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private EndGamePanel endGamePanel;
        #endregion

        #region private methods
        private void TweenText(GameController gameController)
        {
            bool win = (gameController.CurrentState == State.Win);


            txtText.transform.position = xfOrigin.position;

            txtText.transform.DOMove(xfTarget.position, 4.0f);

            if (win)
            {
                txtText.color = colorWin;
            }
            else
            {
                txtText.color = colorLose;
            }

            canvasGroup.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                endGamePanel.ShowPanel(gameController);
            });

        }
        #endregion

        #region public methods
        public void ShowWin(GameController gameController)
        {
            txtText.text = "You Win!";
            TweenText(gameController);
        }

        public void ShowLoss(GameController gameController)
        {
            txtText.text = "Game Over";
            TweenText(gameController);
        }
        #endregion
    }
}
