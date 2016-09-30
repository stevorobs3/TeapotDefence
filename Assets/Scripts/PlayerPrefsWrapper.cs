using UnityEngine;
using System.Collections;

public static class PlayerPrefsWrapper {

    const string TEA_LEAVES_HARVESTED = "TeaLeavesHarvested";
    const string PLANTATIONS_BUILT = "PlantationsBuilt";
    const string CAFETIERES_KILLED = "CafetieresKilled";
    const string STEAM_USED = "SteamUsed";
    const string WIN = "Win";

    public static int TeaLeavesHarvested
    {
        get
        {
            return PlayerPrefs.GetInt(TEA_LEAVES_HARVESTED);
        }
        set
        {
            PlayerPrefs.SetInt(TEA_LEAVES_HARVESTED, value);
        }
    }

    public static int PlantationsBuilt
    {
        get
        {
            return PlayerPrefs.GetInt(PLANTATIONS_BUILT);
        }
        set
        {
            PlayerPrefs.SetInt(PLANTATIONS_BUILT, value);
        }
    }

    public static int Win
    {
        get
        {
            return PlayerPrefs.GetInt(WIN);
        }
        set
        {
            PlayerPrefs.SetInt(WIN, value);
        }
    }

    public static int CafetieresKilled
    {
        get
        {
            return PlayerPrefs.GetInt(CAFETIERES_KILLED);
        }
        set
        {
            PlayerPrefs.SetInt(CAFETIERES_KILLED, value);
        }
    }

    public static float SteamUsed
    {
        get
        {
            return PlayerPrefs.GetFloat(STEAM_USED);
        }
        set
        {
            PlayerPrefs.SetFloat(STEAM_USED, value);
        }
    }
}
