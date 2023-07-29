using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonMusicPlayer
{


    public static void play(AudioSource audio)
    {
        if (InitPrefsData.getInt(InitPrefsData.isAudioOn) == 1)
        {
            audio.Play();
        }
    }

    public static void playMusic(AudioSource audio)
    {
        if (InitPrefsData.getInt(InitPrefsData.isMusicOn) == 1)
        {
            audio.Play();
        }
    }



}
