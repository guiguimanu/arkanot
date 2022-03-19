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
        private LaunchHint launchHint;
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
        public LaunchHint LaunchHint { get { return launchHint; } }
        public Input.SwipeUpDetector SwipeUpDetector { get { return gameState.swipeDetector; } }
        public GameObject BallPrefab { get { return ballPrefab; } }
        #endregion

        #region private methods

        private void Awake()
        {
            gameState.SetGameController(this);
        }

        private void Start()
        {
            gameState.Lives = 3;
            gameState.ChangeState(State.Launch);
        }

        
        private void Update()
        {
            gameState.Update();
        }

        #endregion

        #region public methods
        
        public void OnBallDeath()
        {
            gameState.Lives--;
            lifeDisplay.RemoveHeartAt(gameState.Lives);

            if (gameState.Lives > 0)
            {
                gameState.ChangeState(State.Launch);
            }
            else
            {
                gameState.ChangeState(State.Lost);
                gameOverPanel.ShowLoss(this);
            }

        }

        public void OnAllBricksCleared()
        {
            gameState.ChangeState(State.Win);

            gameOverPanel.ShowWin(this);
            psCelebration.Play();

            if(_levelNumber>0)
                LevelProgressData.UpdateScore(_levelNumber, gameState.Lives);
        }

        public void AddPowerUp(PowerUpBrick powerUp)
        {
            gameState.powerUpCtrl.AddPowerUp(powerUp);
        }
    
        /// <summary>
        /// Moves the void up and make it a solid
        /// You are now a GOD
        /// Use it wiseley
        /// </summary>
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

