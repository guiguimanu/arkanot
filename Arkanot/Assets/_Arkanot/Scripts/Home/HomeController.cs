using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Arkanot.Home
{
    public class HomeController : MonoBehaviour
    {
        [SerializeField]
        private InfoPanel infoPanel;


        public void ExitApp()
        {
            Application.Quit();
        }

        public void GoToLevelSelect()
        {
            SceneManager.LoadScene("Level Selector");
        }

        public void Info()
        {
            infoPanel.ShowPanel();
        }
    
    }
}
