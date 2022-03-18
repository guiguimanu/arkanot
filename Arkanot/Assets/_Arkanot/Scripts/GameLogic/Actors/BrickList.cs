using UnityEngine;
using System.Collections.Generic;

namespace Arkanot.GameLogic.Actors
{
    public class BrickList : MonoBehaviour
    {
        private List<Brick> bricks;
        private GameController gameController;
    
        private void Awake()
        {
            bricks = new List<Brick>(GetComponentsInChildren<Brick>());
        }

        public void SetGameController(GameController gameController)
        {
            this.gameController = gameController;
        }

        public void RemoveBrick(Brick brick)
        {
            bricks.Remove(brick);

            if (bricks.Count == 0)
                gameController.OnAllBricksCleared();
        }
    }
}

