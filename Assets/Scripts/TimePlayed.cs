using System;
using System.Globalization;
using UnityEngine;

public static class TimePlayed
{
    private static DateTime lastUpdatedTime;
    public static bool Initialized;
    private const string DefaultTime = "1970-01-01 00:00:00";

    public static bool Initialize()
    {
        Initialized = true;
        lastUpdatedTime = DateTime.UtcNow;
        var temp = PlayerPrefs.GetInt("GameDestroyTime", 0);
        var totalPlayTime = PlayerPrefs.GetInt("TotalPlayTime", 0);
        FormatPlayTime(ConvertFromInt(totalPlayTime));
        if (temp != 0)
        {
            DateTime destroyedTime = ConvertFromInt(temp);
            Debug.Log(lastUpdatedTime.ToString(CultureInfo.InvariantCulture) + " LastUpdatedTime");
            Debug.Log(destroyedTime.ToString(CultureInfo.InvariantCulture) + " DestroyedTime");
            var difference = (lastUpdatedTime - destroyedTime).TotalMinutes;
            Debug.Log(difference + " Difference");
            return true;
        }

        return false;
        }

        public static string GetTimePlayed()
        {
            return FormatPlayTime(UpdateTimePlayed());
        }

        private static int ConvertToInt(DateTime dateTime)
        {
            var newTime = dateTime - DateTime.Parse(DefaultTime, CultureInfo.InvariantCulture);
            return (int) newTime.TotalSeconds;
        }

        private static DateTime ConvertFromInt(int dateTime)
        {
            var newTime = DateTime.Parse(DefaultTime, CultureInfo.InvariantCulture);
            var result = newTime.AddSeconds(dateTime);
            return result;
        }

        private static DateTime UpdateTimePlayed()   
        {
            var currentTime = DateTime.UtcNow;
            var totalPlayTime = ConvertFromInt(PlayerPrefs.GetInt("TotalPlayTime" , 0));
            var difference = currentTime - lastUpdatedTime;
            var newTime = totalPlayTime.Add(difference);
            SaveTimePlayed();
            lastUpdatedTime = currentTime;
            return newTime;
        }
    
        static string FormatPlayTime(DateTime playTime)
        {
            var result = "";
            var years = playTime.Year - 1970;
            var days = playTime.Day - 1;
            var hours = playTime.Hour;
            var minutes = playTime.Minute;
            if (years != 0)
            {
                result += $"{years}";
                if (years > 1)
                    result += " Years ";
                else 
                    result += " Year ";
            }
            if (days != 0)
            {
                result += $"{days}";
                if (days > 1)
                    result += " Days ";
                else 
                    result += " Day ";
            }
            if (hours != 0)
            {
                result += $"{hours}";
                if (hours > 1)
                    result += " Hours ";
                else
                    result += " Hour ";
            }
            result += $"{minutes}";
            if (minutes > 1 || minutes == 0)
                result += " Minutes ";
            else
                result += " Minute ";
            return result;
        }

        public static void SaveDestroyedTime()
        {
            PlayerPrefs.SetInt("GameDestroyTime", ConvertToInt(DateTime.UtcNow));
        }
        public static void SaveTimePlayed()
        {
            var timePlayed = ConvertFromInt(PlayerPrefs.GetInt("TotalPlayTime", 0)) + (DateTime.UtcNow - lastUpdatedTime);
            PlayerPrefs.SetInt("TotalPlayTime", ConvertToInt(timePlayed)); 
        }
}