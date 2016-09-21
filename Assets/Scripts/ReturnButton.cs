using UnityEngine;
using System.Collections;

public class ReturnButton : MonoBehaviour {

    private GameController _gameController;

    public void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    public void ReturnToGame()
    {
        _gameController.ReturnToGame();
    }
}
