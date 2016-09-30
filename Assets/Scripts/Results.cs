using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {

    public Text _cafetieresKilled;
    public Text _teaLeavesHarvested;
    public Text _teaPlantationsBuilt;
    public Text _steamUsed;
    public Text _message;

    void Start () {
        _cafetieresKilled.text = PlayerPrefsWrapper.CafetieresKilled.ToString();
        _teaLeavesHarvested.text = PlayerPrefsWrapper.TeaLeavesHarvested.ToString();
        _teaPlantationsBuilt.text = PlayerPrefsWrapper.PlantationsBuilt.ToString();
        _steamUsed.text = ((int)PlayerPrefsWrapper.SteamUsed).ToString();

        if (PlayerPrefsWrapper.Win == 1)
        {
            _message.text = "You Won!";
        }
        else
        {
            _message.text = "The mighty coffee makers were too much for you!";
        }
    }
}