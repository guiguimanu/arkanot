using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Arkanot.Data;

namespace Arkanot.LevelSelection
{
    public class LevelSelectionController : MonoBehaviour
    {
        private int levelsToShow = LevelProgressData.NumberOfLevels;

        [SerializeField]
        private GameObject goButtonPrefab;
        [SerializeField]
        private Transform xfContent;

        private void Start()
        {
            RefreshNumberOfLevelsToShow();
            InstantiateButtons();
        }

        private void RefreshNumberOfLevelsToShow()
        {
            for (int i = 1; i <= LevelProgressData.NumberOfLevels; i++)
            {
                if (LevelProgressData.GetScore(i) == 0)
                {
                    levelsToShow = i;
                    break;
                }
            }
        }

        private void InstantiateButtons()
        {
            for (int i = 1; i <= levelsToShow; i++)
            {
                LevelSelectionButton button = Instantiate(goButtonPrefab, xfContent).GetComponent<LevelSelectionButton>();
                button.Init(i);
            }
        }
    
        public void BackToHome()
        {
            SceneManager.LoadScene("Home");
        }
    }
}

