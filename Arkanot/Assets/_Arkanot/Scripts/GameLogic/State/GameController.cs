using UnityEngine;
using DG.Tweening;
using Arkanot.Data;
using Arkanot.GameLogic.PowerUps;
using Arkanot.GameLogic.Actors;
using Arkanot.GameLogic.UI;
namespace Arkanot.GameLogic
{
    public class GameController : MonoBehaviour
    {
        #region private fields
        [Tooltip("This will be used for data saving, please make sure you have the correct level configured here")]
        [SerializeField]
        private int _levelNumber = -100;

        [SerializeField]
        private GameState gameState;
        
        [Header("UI References")]
        [SerializeField]
        private CanvasGroup cgLaunchHint;
        [SerializeField]
        private GameOverPanel gameOverPanel;
        [SerializeField]
        private LifeDisplay lifeDisplay;

        [Header("Other References")]
        [SerializeField]
        private GameObject ballPrefab;
        [SerializeField]
        private ParticleSystem psCelebration;
        
        #endregion

        #region properties

        public int LevelNumber { get { return _levelNumber; } }
        public Ball CurrentBall { get { return gameState.CurrentBall; } }
        public Paddle Paddle { get { return gameState.paddle; } }
        public GameObject GoVoid { get { return gameState.goVoid; } }
        public State CurrentState { get { return gameState.CurrentState; } }
        public int Lives { get; set; }
        
        #endregion

        #region private methods

        private void Awake()
        {
            gameState.SetGameController(this);
        }

        private void Start()
        {
            Lives = 3;
            SpawnBall();
            
            cgLaunchHint.DOFade(1.0f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        private void SpawnBall()
        {
            gameState.AssignBall(Instantiate(ballPrefab));
        }
        
        private void Update()
        {
            gameState.Update();
        }

        #endregion

        #region public methods

        public void HideLaunchHint()
        {
            cgLaunchHint.DOKill();
            cgLaunchHint.DOFade(0, 0.2f);
        }

        public void OnBallDeath()
        {
            lifeDisplay.RemoveHeartAt(Lives-1);
            Lives--;

            if (Lives > 0)
            {
                gameState.ChangeToLaunch();
                Destroy(CurrentBall.gameObject);
                SpawnBall();
                
                cgLaunchHint.DOFade(1.0f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                gameState.ChangeToLost();
                gameOverPanel.ShowLoss(this);
            }

        }

        public void OnAllBricksCleared()
        {
            gameState.ChangeToWin();
            CurrentBall.StopAndImplode();
            gameOverPanel.ShowWin(this);
            psCelebration.Play();

            if(_levelNumber>0)
                LevelProgressData.UpdateScore(_levelNumber, Lives);
        }

        public void AddPowerUp(PowerUpBrick powerUp)
        {
            gameState.powerUpCtrl.AddPowerUp(powerUp);
        }
    
        [ContextMenu("God Mode")]
        public void GodMode()
        {
            Vector2 v2Pos = gameState.goVoid.transform.position;
            v2Pos.y = -6.5f;
            gameState.goVoid.transform.position = v2Pos;

            gameState.goVoid.GetComponent<Collider2D>().isTrigger = false;
        }

        #endregion
        
    }
}

