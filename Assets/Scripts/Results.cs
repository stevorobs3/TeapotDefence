using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {

    public Text _cafetieresKilled;
    public Text _teaLeavesHarvested;
    public Text _teaPlantationsBuilt;
    public Text _steamUsed;

    void Start () {
        _cafetieresKilled.text = PlayerPrefsWrapper.CafetieresKilled.ToString();
        _teaLeavesHarvested.text = PlayerPrefsWrapper.TeaLeavesHarvested.ToString();
        _teaPlantationsBuilt.text = PlayerPrefsWrapper.PlantationsBuilt.ToString();
        _steamUsed.text = ((int)PlayerPrefsWrapper.SteamUsed).ToString();
    }
}