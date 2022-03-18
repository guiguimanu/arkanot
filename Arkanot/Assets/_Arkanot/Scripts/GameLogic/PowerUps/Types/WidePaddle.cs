
namespace Arkanot.GameLogic.PowerUps.Types
{
    public class WidePaddle : IPowerUp
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
            gameController.Paddle.SetCurrentWidth(1.5f);
        }

        public void EndPowerUp()
        {
            gameController.Paddle.SetCurrentWidth(1);
        }
    }

}

