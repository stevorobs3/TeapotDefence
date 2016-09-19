using UnityEngine;
using System.Collections;

public static class PlayerPrefsWrapper {

    const string TEA_LEAVES_HARVESTED = "TeaLeavesHarvested";
    const string TEA_PLANTATIONS_BUILT = "TeaPlantationsBuilt";
    const string CAFETIERES_KILLED = "CafetieresKilled";
    const string STEAM_USED = "SteamUsed";

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

    public static int TeaPlantationsBuilt
    {
        get
        {
            return PlayerPrefs.GetInt(TEA_PLANTATIONS_BUILT);
        }
        set
        {
            PlayerPrefs.SetInt(TEA_PLANTATIONS_BUILT, value);
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
