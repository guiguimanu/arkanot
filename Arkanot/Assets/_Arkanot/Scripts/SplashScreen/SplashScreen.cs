using UnityEngine;
using UnityEngine.SceneManagement;
namespace Arkanot.SplashScreen
{
    public class SplashScreen : MonoBehaviour
    {
        public void GoToHome()
        {
            SceneManager.LoadScene("Home");
        }
        
    }

}

