using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelDefination
{


    public static string getLevel(int value)
    {
        if (value <= 10)
        {
            return "Beginner";
        }
        else if (value <= 20)
        {
            return "Intermediate";
        }
        else if (value <= 30)
        {
            return "Advanced";
        }
        else if (value <= 40)
        {
            return "Expert";
        }
        else if (value <= 50)
        {
            return "Master";
        }

        return "Beginner";
    }


}
