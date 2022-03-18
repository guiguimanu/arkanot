namespace Arkanot.GameLogic.PowerUps.Types
{
    public class ShortPaddle :IPowerUp
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
            gameController.Paddle.SetCurrentWidth(0.25f);
        }

        public void EndPowerUp()
        {
            gameController.Paddle.SetCurrentWidth(1);
        }
    }
}
