using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private int _cafetieresKilled;
    private int _teaLeavesHarvested;
    private int _plantationsBuilt;
    private float _steamUsed;

    void Start()
    {
        ResetStats();
    }

    private void ResetStats()
    {
        _cafetieresKilled = 0;
        _teaLeavesHarvested = 0;
        _steamUsed = 0;
        _plantationsBuilt = 0;
    }


    public void CafetieresKilled(int amount)
    {
        _cafetieresKilled += amount;
    }

    public void TeaLeavesHarvested(int amount)
    {
        _teaLeavesHarvested += amount;
    }

    public void PlantationsBuilt(int amount)
    {
        _plantationsBuilt += amount;
    }

    public void SteamUsed(float amount)
    {
        _steamUsed += amount;
    }



    private bool gameIsEnding = false;
    public void EndGame(bool win)
    {
        if (!gameIsEnding)
        {
            gameIsEnding = true;
            PlayerPrefsWrapper.CafetieresKilled = _cafetieresKilled;
            PlayerPrefsWrapper.TeaLeavesHarvested = _teaLeavesHarvested;
            PlayerPrefsWrapper.SteamUsed = _steamUsed;
            PlayerPrefsWrapper.PlantationsBuilt = _plantationsBuilt;
            PlayerPrefsWrapper.Win = win ? 1 : 0;
            SceneManager.LoadScene(1);
        }
    }


    public void ReturnToGame()
    {
        gameIsEnding = false;
        SceneManager.LoadScene(0);
    }
}
