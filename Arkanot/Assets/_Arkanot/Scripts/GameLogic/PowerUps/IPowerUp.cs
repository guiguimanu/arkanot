namespace Arkanot.GameLogic.PowerUps
{
    public interface IPowerUp
    {
        void SetGameController(GameController gameController);
        void StartPowerUp();
        void EndPowerUp();
        float Duration();
    }
}
