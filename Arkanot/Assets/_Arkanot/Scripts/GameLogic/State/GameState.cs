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
        private GameController gameController;

        //SerializeField is set for inspector visibility. Not strictly neccessary
        [SerializeField]
        private State _currentState;
        #endregion
        
        #region properties
        public int Lives { get; set; }
        public SwipeUpDetector SwipeUpDetector { get; private set; }
        public PowerUpController PowerUpController { get; private set; }
        public Ball CurrentBall { get; private set; }
        
        public State CurrentState { get { return _currentState; } }
        public Paddle Paddle { get { return gameController.Paddle; } }
        public UI.LaunchHint LaunchHint { get { return gameController.LaunchHint; } }
        #endregion

        public GameState(GameController gameController)
        {
            this.gameController = gameController;
            SwipeUpDetector = new SwipeUpDetector(100, 0.25f);
            PowerUpController = new PowerUpController(gameController);
            _currentState = State.Intro;
        }

        #region private methods


        private void AssignBall(GameObject goBall)
        {
            CurrentBall = goBall.GetComponent<Ball>();
            CurrentBall.SetGameController(gameController);
        }

        #endregion

        #region public methods
        
        public void ChangeState(State state)
        {
            _currentState = state;
            
            //On State Enter
            switch (state)
            {
                case State.Intro:
                    break;
                case State.Launch:

                    SwipeUpDetector.Reset();
                    if(CurrentBall!=null)
                        GameObject.Destroy(CurrentBall.gameObject);

                    AssignBall(gameController.SpawnBall());
                    LaunchHint.ShowHint();

                    break;
                case State.Playing:

                    CurrentBall.Launch();
                    LaunchHint.HideHint();

                    break;
                case State.Win:

                    CurrentBall.StopAndImplode();

                    break;
                case State.Lost:

                    Paddle.Implode();

                    break;
                default:
                    break;
            }
        }

        public void Update()
        {
            switch (_currentState)
            {
                case State.Intro:
                    break;
                case State.Launch:

                    CurrentBall.FollowPaddle();

                    LaunchHint.FollowPaddle(Paddle.transform);

                    if (SwipeUpDetector.ListenForSwipeUp())
                        ChangeState(State.Playing);


                    break;
                case State.Playing:

                    PowerUpController.Update();
                    CurrentBall.NudgeBallIfStuck();

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
