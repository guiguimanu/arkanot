using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Arkanot.GameLogic;

public class GameStateTests
{
    [Test]
    public void Can_Change_To_All_States()
    {
        GameState gameState = new GameState(null);



        for (int i = 0; i < System.Enum.GetNames(typeof(State)).Length; i++)
        {
            try
            {
                gameState.ChangeState((State)i);
            }
            catch
            {
                //we know there will be null references
                //we just want to make sure the state changes
            }

            if (gameState.CurrentState != (State)i)
            {
                Debug.Log($"Changing to {(State)i} FAILED");
                Assert.Fail();
            }
            else
            {
                Debug.Log($"Changing to {(State)i} Succeeded");
            }
        }

        Assert.Pass();
    }
    
}
