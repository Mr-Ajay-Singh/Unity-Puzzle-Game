using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InitPrefsData
{
    public static string coins = "coins";
    public static string level = "level";
    public static string isMusicOn = "isMusicOn";
    public static string isAudioOn = "isAudioOn";
    public static void initPrefs()
    {
        if (PlayerPrefs.GetInt(coins, -1) == -1)
        {
            PlayerPrefs.SetInt(coins, 20);
        }
        if (PlayerPrefs.GetInt(level, -1) == -1)
        {
            PlayerPrefs.SetInt(level, 20);
        }
        if (PlayerPrefs.GetInt(isMusicOn, -1) == -1)
        {
            PlayerPrefs.SetInt(isMusicOn, 1);
        }
        if (PlayerPrefs.GetInt(isAudioOn, -1) == -1)
        {
            PlayerPrefs.SetInt(isAudioOn, 1);
        }

    }

    public static int getInt(string txt,int def =1)
    {
        return PlayerPrefs.GetInt(txt, def);
    }
    public static void setInt(string txt,int val)
    {
        PlayerPrefs.SetInt(txt, val);
    }

}
