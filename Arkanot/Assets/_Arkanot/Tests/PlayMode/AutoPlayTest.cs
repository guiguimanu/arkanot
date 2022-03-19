using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Arkanot.GameLogic;
using Arkanot.Input;

public class AutoPlayTest
{

    [UnityTest]
    public IEnumerator Should_Lose_All_Levels_With_Avoidant_AI()
    {
        int LevelToPlay = 1;
        while (LevelToPlay <= Arkanot.Data.LevelProgressData.NumberOfLevels)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level " + LevelToPlay);

            yield return null;

            Time.timeScale = 10;

            // get all our references
            GameController gameController = GameObject.FindObjectOfType<GameController>();
            SwipeUpDetector swipeUpDetector = gameController.SwipeUpDetector;
            TouchToWorld touchInput = gameController.Paddle.TouchInput;


            while (gameController.CurrentState != State.Lost)
            {
                //lets play
                swipeUpDetector.OverrideInput = true;

                while (gameController.CurrentState == State.Launch)
                    yield return null;



                while (gameController.CurrentState == State.Playing)
                {
                    if (gameController.CurrentBall != null)
                    {
                        //this AI avoids the ball at all costs
                        Vector2 ballPosition = gameController.CurrentBall.transform.position;

                        //move paddle at the opposite fare end of the ball
                        if (ballPosition.x > 0)
                        {
                            touchInput.OverideInput(new Vector2(-3,0));

                        }
                        else
                        {
                            touchInput.OverideInput(new Vector2(3, 0));

                        }
                    
                    
                    }
                    yield return null;
                }
                

                if (gameController.CurrentState == State.Win)
                    Assert.Fail();


            }

            LevelToPlay++;
        }


        Assert.Pass();
    }

    [UnityTest]
    public IEnumerator Should_Win_All_Levels_With_Smart_AI()
    {
        int LevelToPlay = 1;
        while(LevelToPlay <= Arkanot.Data.LevelProgressData.NumberOfLevels)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level "+LevelToPlay);
            
            yield return null;
            
            Time.timeScale = 20;

            // get all our references
            GameController gameController = GameObject.FindObjectOfType<GameController>();
            SwipeUpDetector swipeUpDetector = gameController.SwipeUpDetector;
            TouchToWorld touchInput = gameController.Paddle.TouchInput;

            int attempts = 0;

            while(gameController.CurrentState!=State.Win)
            {
                attempts++;

                swipeUpDetector.OverrideInput = true;

                //wait for launch to set off
                while (gameController.CurrentState == State.Launch)
                    yield return null;

                swipeUpDetector.OverrideInput = false;

                while (gameController.CurrentState == State.Playing)
                {
                    if (gameController.CurrentBall != null)
                    {
                        //very rudementary AI that puts a touch input wherever the ball is 
                        Vector2 idealPosition = gameController.CurrentBall.transform.position;
                        
                        //with a little bit of x noise so the ball doesnt always go straight up and down forever
                        Vector2 noise = new Vector2(Random.Range(-0.2f,0.2f), 0);

                        //lower the noise in case we have the short paddle
                        noise *= gameController.Paddle.CurrentWidth;

                        //finally lets give the AI an advantage (we want him to win everytime after all)
                        gameController.Paddle.OverrideSpeed(100);

                        touchInput.OverideInput(idealPosition + noise);
                    }
                    yield return null;
                }

                if (gameController.CurrentState == State.Lost)
                    Assert.Fail();

            }

            Debug.Log($"Completed level {LevelToPlay} with {attempts} attempts!");

            LevelToPlay++;
        }


        Assert.Pass();
    }
}
