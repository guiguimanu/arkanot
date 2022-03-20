using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Arkanot.Data;
public class DataTests
{
    static int[] scores = new int[] { 1, 2, 3 };

    [Test]
    public void Should_Have_No_Data_After_Clear_Data()
    {
        LevelProgressData.ClearData();

        Assert.False(LevelProgressData.HasData());
    }

    [Test]
    public void Should_Set_All_Score_Variants_On_All_Levels([ValueSource("scores")] int score)
    {
        LevelProgressData.ClearData();

        int currentLevel = 1;

        while(currentLevel <= LevelProgressData.NumberOfLevels)
        {
            LevelProgressData.UpdateScore(currentLevel, score);

            if(LevelProgressData.GetScore(currentLevel)!=score)
            {
                Debug.Log($"Updating score for level {currentLevel} with a score of {score} failed!");
                Assert.Fail();
                return;
            }
            currentLevel++;
        }

        Assert.Pass();
    }

    [Test]
    public void Should_Have_Next_Level()
    {
        Assert.True(LevelProgressData.HasNextLevel(1));
    }

    [Test]
    public void Should_Not_Have_Next_Level()
    {
        Assert.False(LevelProgressData.HasNextLevel(LevelProgressData.NumberOfLevels));
    }

}
