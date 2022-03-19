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
        //SerializeField is set for inspector visibility. Not strictly neccessary
        [SerializeField]
        private State _currentState;
        private GameController gameController;
        #endregion

        #region public variables
        public PowerUpController powerUpCtrl;
        public SwipeUpDetector swipeDetector;

        [Header("Actors")]
        public Paddle paddle;
        public BrickList brickList;
        public GameObject goVoid;
        #endregion

        #region properties
        public Ball CurrentBall { get; private set; }
        public State CurrentState { get { return _currentState; } }
        public int Lives { get; set; }
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

        private void AssignBall(GameObject goBall)
        {
            CurrentBall = goBall.GetComponent<Ball>();
            CurrentBall.SetGameController(gameController);
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

        public void ChangeState(State state)
        {
            _currentState = state;
            
            //On State Enter
            switch (state)
            {
                case State.Launch:

                    swipeDetector.Reset();
                    if(CurrentBall!=null)
                        GameObject.Destroy(CurrentBall.gameObject);

                    AssignBall(GameObject.Instantiate(gameController.BallPrefab));
                    gameController.LaunchHint.ShowHint();

                    break;
                case State.Playing:

                    CurrentBall.Launch();
                    gameController.LaunchHint.HideHint();

                    break;
                case State.Win:

                    CurrentBall.StopAndImplode();

                    break;
                case State.Lost:

                    paddle.Implode();

                    break;
                default:
                    break;
            }
        }

        public void Update()
        {
            switch (_currentState)
            {
                case State.Launch:

                    MoveBallWithPaddle();

                    if (swipeDetector.ListenForSwipeUp())
                        ChangeState(State.Playing);


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
