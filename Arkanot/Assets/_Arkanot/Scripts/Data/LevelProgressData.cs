using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Arkanot.Data
{
    /// <summary>
    /// A simple player prefs based data class
    /// </summary>
    public static class LevelProgressData
    {
        private const string DATA_KEY = "level_progress_data";

        //Note, edit this when you add a level
        public static int NumberOfLevels = 12;
    
        private static string LevelDataKey(int level)
        {
            return $"{DATA_KEY}_{level}";
        }

        /// <summary>
        /// Check the score for a specified level
        ///  0 if no score, -1 if out of bounds, 1-3 if has score
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetScore(int level)
        {
            //no more levels
            if (level > NumberOfLevels)
                return -1;
            
            return PlayerPrefs.GetInt(LevelDataKey(level),0);
        }

        /// <summary>
        /// Update the score, score is number of lives at the end of a game
        /// </summary>
        /// <param name="level"></param>
        /// <param name="score"></param>
        public static void UpdateScore(int level, int score)
        {
            PlayerPrefs.SetInt(LevelDataKey(level), score);
        }

        /// <summary>
        /// Check to see if we have another level
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <returns></returns>
        public static bool HasNextLevel(int currentLevel)
        {
            if (currentLevel <= 0)
                return false;

            return currentLevel + 1 <= NumberOfLevels;
        }
    
        /// <summary>
        /// Delete All Data
        /// </summary>
        public static void ClearData()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        /// Check if there is any data at all
        /// Note: Used mainly for testing
        /// </summary>
        public static bool HasData()
        {
            for (int i = 1; i <= NumberOfLevels; i++)
            {
                //if any data at all return true
                if (PlayerPrefs.HasKey(LevelDataKey(i)))
                    return true;
            }

            return false;
        }
    }

}
