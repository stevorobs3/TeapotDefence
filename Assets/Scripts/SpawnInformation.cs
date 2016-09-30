using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpawnInformation : MonoBehaviour {

    public GridLayoutGroup _entityGrid;
    public Text _text;


    public Sprite _cafetiere;
    public Sprite _italianStove;

    // Private
    Image[] _enitityBoxes;
    Dictionary<CoffeeMakerType, Sprite> _sprites;
    float _timeTilSpawn;
    void Awake()
    {
        //TODO: calculate number of children to make...! (plus width and height)
        // TODO: do this each new spawn cycle

        _enitityBoxes = new Image[_entityGrid.transform.childCount];
        for (int i = 0; i < _entityGrid.transform.childCount; i++)
        {
            _enitityBoxes[i] = _entityGrid.transform.GetChild(i).GetComponent<Image>();
        }

        _sprites = new Dictionary<CoffeeMakerType, Sprite>()
        {
            { CoffeeMakerType.Cafetiere, _cafetiere },
            { CoffeeMakerType.ItalianStove, _italianStove},
        };
    }



    void Update()
    {
        _timeTilSpawn -= Time.deltaTime;
        if (_timeTilSpawn > 0)
        {            
            _text.text = _timeTilSpawn.ToString("F2");
        }
        else if (_numSpawns < _wave.NumCafetieres + _wave.NumItalianStoves) {
            _timeTilSpawn = _wave.TimeBetweenSpawns;
            _text.text = _timeTilSpawn.ToString("F2");
        }
        else
        {
            _text.text = "";
        }
    }

    SpawnWave _wave;
    int _numSpawns;

    public void SetNextSpawn(SpawnWave wave)
    {
        _numSpawns = 0;
        _wave = wave;
        _timeTilSpawn = wave.TimeBeforeFirstSpawn;
        ClearEntityBoxes();
        ResizeEntityBoxes(wave.NumCafetieres + wave.NumItalianStoves);
        int index = 0;
        for (int i = 0; i < wave.NumCafetieres; i++)
        {
            _enitityBoxes[index].sprite = _sprites[CoffeeMakerType.Cafetiere];
            index++;
        }

        for (int j = 0; j < wave.NumItalianStoves; j++)
        {
            _enitityBoxes[index].sprite = _sprites[CoffeeMakerType.ItalianStove];
            index++;
        }
    }

    public void EntitySpawned()
    {
        // TODO animate this over time
        _enitityBoxes[_numSpawns].gameObject.SetActive(false);
        _numSpawns++;
    }

    void ResizeEntityBoxes(int numEntities)
    {
        int size = 1;
        while (size * size <= numEntities)
            size++;

        var rect = _entityGrid.GetComponent<RectTransform>();
        float width = rect.sizeDelta.x / size;
        float height = rect.sizeDelta.y / size;

        _entityGrid.cellSize = new Vector2(width, height);

        for (int i = 0; i < _enitityBoxes.Length; i++)
        {
            _enitityBoxes[i].gameObject.SetActive(i < numEntities);
        }
    }

    void ClearEntityBoxes()
    {
        for (int i = 0; i < _enitityBoxes.Length; i++)
        {
            _enitityBoxes[i].sprite = null;
        }
    }

}