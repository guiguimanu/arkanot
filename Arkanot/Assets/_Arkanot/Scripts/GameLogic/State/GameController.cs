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

        [Header("Actors")]
        [SerializeField]
        private Paddle _paddle;
        [SerializeField]
        private BrickList _brickList;
        [SerializeField]
        private GameObject _goVoid;

        [Header("UI References")]
        [SerializeField]
        private LevelIntroUI levelIntroUI;
        [SerializeField]
        private LaunchHint _launchHint;
        [SerializeField]
        private GameOverPanel gameOverPanel;
        [SerializeField]
        private LifeDisplay lifeDisplay;

        [Header("Prefabs")]
        [SerializeField]
        private GameObject _ballPrefab;

        [Header("Effects")]
        [SerializeField]
        private ParticleSystem psCelebration;
        
        #endregion

        #region properties

        public int LevelNumber { get { return _levelNumber; } }
        public Paddle Paddle { get { return _paddle; } }
        public GameObject GoVoid { get { return _goVoid; } }
        public LaunchHint LaunchHint { get { return _launchHint; } }
        public GameObject BallPrefab { get { return _ballPrefab; } }
        public Ball CurrentBall { get { return gameState.CurrentBall; } }
        public State CurrentState { get { return gameState.CurrentState; } }
        public Input.SwipeUpDetector SwipeUpDetector { get { return gameState.SwipeUpDetector; } }
        #endregion

        #region private methods

        private void Awake()
        {
            gameState = new GameState(this);
            _brickList.SetGameController(this);
        }

        private void Start()
        {
            gameState.Lives = 3;
            levelIntroUI.ShowIntro(_levelNumber, () => gameState.ChangeState(State.Launch));
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
            gameState.PowerUpController.AddPowerUp(powerUp);
        }
    
        public GameObject SpawnBall()
        {
            return Instantiate(BallPrefab);
        }

        /// <summary>
        /// Moves the void up and make it a solid
        /// You are now a GOD
        /// Use it wiseley
        /// </summary>
        [ContextMenu("God Mode")]
        public void GodMode()
        {
            Vector2 v2Pos = _goVoid.transform.position;
            v2Pos.y = -6.5f;
            _goVoid.transform.position = v2Pos;

            _goVoid.GetComponent<Collider2D>().isTrigger = false;
        }

        #endregion
        
    }
}

