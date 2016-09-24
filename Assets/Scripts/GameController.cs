using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private int _cafetieresKilled;
    private int _teaLeavesHarvested;
    private int _teaPlantationsBuilt;
    private float _steamUsed;

    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        _cafetieresKilled = 0;
        _teaLeavesHarvested = 0;
        _steamUsed = 0;
    }


    public void CafetieresKilled(int amount)
    {
        _cafetieresKilled += amount;
    }

    public void TeaLeavesHarvested(int amount)
    {
        _teaLeavesHarvested += amount;
    }

    public void TeaPlantationBuilt(int amount)
    {
        _teaPlantationsBuilt += amount;
    }

    public void SteamUsed(float amount)
    {
        _steamUsed += amount;
    }



    private bool gameIsEnding = false;
    public void EndGame()
    {
        if (!gameIsEnding)
        {
            gameIsEnding = true;
            PlayerPrefsWrapper.CafetieresKilled = _cafetieresKilled;
            PlayerPrefsWrapper.TeaLeavesHarvested = _teaLeavesHarvested;
            PlayerPrefsWrapper.SteamUsed = _steamUsed;
            PlayerPrefsWrapper.TeaPlantationsBuilt = _teaPlantationsBuilt;
            SceneManager.LoadScene(1);
        }
    }


    public void ReturnToGame()
    {
        gameIsEnding = false;
        SceneManager.LoadScene(0);
    }
}
