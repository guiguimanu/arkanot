namespace Arkanot.GameLogic.PowerUps.Types
{
    public class SlowBall : IPowerUp
    {
        private GameController gameController;

        public void SetGameController(GameController gameController)
        {
            this.gameController = gameController;
        }

        public float Duration()
        {
            return 5.0f;
        }

        public void StartPowerUp()
        {
            gameController.CurrentBall.SetSpeedMultiplier(0.5f);
        }


        public void EndPowerUp()
        {
            gameController.CurrentBall.SetSpeedMultiplier(1);
        }
    }

}
