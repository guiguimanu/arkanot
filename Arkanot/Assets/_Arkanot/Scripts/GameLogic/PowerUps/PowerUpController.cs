using UnityEngine;
using System.Collections;
using Arkanot.GameLogic.PowerUps.Types;

namespace Arkanot.GameLogic.PowerUps
{
    public class PowerUpController 
    {
        #region private variables
        private GameController gameController;
        private IPowerUp currentPowerUp;
        private float fPowerUpCountDown;
        #endregion

        #region public methods

        public PowerUpController(GameController gameController)
        {
            this.gameController = gameController;
        }

        public void Update()
        {
            if (currentPowerUp == null)
                return;

            fPowerUpCountDown -= Time.deltaTime;

            if (fPowerUpCountDown < 0)
            {
                currentPowerUp.EndPowerUp();
                currentPowerUp = null;
            }
        }

        public void AddPowerUp(PowerUpBrick powerUp)
        {
            powerUp.OnGet();

            //only one power up at the time
            if (currentPowerUp != null)
                currentPowerUp.EndPowerUp();

            currentPowerUp = PowerUpTypes.Instantiate(powerUp.PowerUpType);

            if (currentPowerUp != null)
            {
                currentPowerUp.SetGameController(gameController);
                fPowerUpCountDown = currentPowerUp.Duration();
                currentPowerUp.StartPowerUp();
            }
        }

        #endregion
    }
}

