using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Arkanot.Data;

namespace Arkanot.GameLogic.UI
{
    public class EndGamePanel : MonoBehaviour
    {
        #region private variables
        private int currentLevel;

        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private GameObject goHomeButton;
        [SerializeField]
        private GameObject goReplayButton;
        [SerializeField]
        private GameObject goNextLevelButton;

        #endregion

        #region public methods
        public void ShowPanel(GameController gameController)
        {
            bool win = (gameController.CurrentState == State.Win);

            currentLevel = gameController.LevelNumber;
        
            goNextLevelButton.SetActive(win && LevelProgressData.HasNextLevel(currentLevel));

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, 0.25f);
        }

        public void GoToLevelSelect()
        {
            SceneManager.LoadScene("Level Selector");
        }

        public void GoNextLevel()
        {
            if (currentLevel <= 0)
                return;

            int nextLevel = currentLevel + 1;

            SceneManager.LoadScene("Level "+ nextLevel);
        
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion
    }
}

