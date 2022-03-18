using UnityEngine;
using Arkanot.Input;
using Arkanot.GameLogic.PowerUps;
using Arkanot.GameLogic.Actors;

namespace Arkanot.GameLogic
{
    [System.Serializable]
    public class GameState
    {
        #region private variables
        [SerializeField]
        private State _currentState;
        private GameController gameController;
        #endregion

        #region public variables
        public Paddle paddle;
        public BrickList brickList;
        public GameObject goVoid;
        public PowerUpController powerUpCtrl;
        public SwipeUpDetector swipeDetector;
        #endregion

        #region properties
        public Ball CurrentBall { get; private set; }
        public State CurrentState { get { return _currentState; } }
        #endregion

        #region private methods

        private void MoveBallWithPaddle()
        {
            if (CurrentBall == null)
                return;

            Vector3 pos = paddle.transform.position;
            pos.y += 0.4f;

            CurrentBall.transform.position = pos;
        }

        /// <summary>
        /// Edge case where the ball stays at 0 y velocity forever
        /// This happens when it hits the roof with a low y velocity
        /// Solution? Nudge it!
        /// </summary>
        private void NudgeBallIfStuck()
        {
            if (CurrentBall.IsStuck())
                CurrentBall.Nudge();
        }


        #endregion

        #region state changes
        public void ChangeToPlaying()
        {
            _currentState = State.Playing;
            CurrentBall.Launch();

            gameController.HideLaunchHint();
        }

        public void ChangeToLaunch()
        {
            _currentState = State.Launch;
            swipeDetector.Reset();

            GameObject.Destroy(CurrentBall.gameObject);
        }

        public void ChangeToLost()
        {
            _currentState = State.Lost;
        }

        public void ChangeToWin()
        {
            _currentState = State.Win;

        }

        #endregion

        #region public methods

        public void SetGameController(GameController gameController)
        {
            this.gameController = gameController;

            paddle.SetGameController(gameController);
            brickList.SetGameController(gameController);
            swipeDetector = new SwipeUpDetector(0.15f, 0.25f);
            powerUpCtrl = new PowerUpController(gameController);
            _currentState = State.Launch;
        }
        
        public void AssignBall(GameObject goBall)
        {
            CurrentBall = goBall.GetComponent<Ball>();
            CurrentBall.SetGameController(gameController);
        }

        public void Update()
        {
            switch (_currentState)
            {
                case State.Launch:

                    MoveBallWithPaddle();

                    if (swipeDetector.ListenForSwipeUp())
                        ChangeToPlaying();


                    break;
                case State.Playing:

                    powerUpCtrl.Update();
                    NudgeBallIfStuck();

                    break;
                case State.Win:
                case State.Lost:
                default:
                    break;
            }
        }
        #endregion
    }
}
