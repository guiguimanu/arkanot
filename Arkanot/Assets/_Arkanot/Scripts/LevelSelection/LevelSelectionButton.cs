using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Arkanot.Data;

namespace Arkanot.LevelSelection
{
    public class LevelSelectionButton : MonoBehaviour
    {
        private int level;

        [SerializeField]
        private int score;
        [SerializeField]
        private Image[] stars;
        [SerializeField]
        private Color colorStarFill;
        [SerializeField]
        private TextMeshProUGUI txtLevelNum;

        private void FillStars()
        {
            if (score <= 0)
                return;

            for (int i = 1; i <= score; i++)
                stars[i - 1].color = colorStarFill;
        }

        public void Init(int level)
        {
            score = LevelProgressData.GetScore(level);

            txtLevelNum.text = level.ToString();

            this.level = level;

            FillStars();
        }

        public void OnPress()
        {
            SceneManager.LoadScene("Level " + level);
        }
    }

}

