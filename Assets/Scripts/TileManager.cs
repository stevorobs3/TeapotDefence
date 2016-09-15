using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    public GameObject GrassTile;

    const int PIXELS_PER_TILE = 128;
    const int NUM_TILES_WIDE = 25;

    // Use this for initialization
    void Awake () {
        SpawnTiles();
	}

    private void SpawnTiles()
    {
        for (int i = -NUM_TILES_WIDE / 2; i < NUM_TILES_WIDE / 2; i++)
        {
            for (int j = -NUM_TILES_WIDE; j < NUM_TILES_WIDE / 2; j++)
            {
                var position = new Vector3(i, j,0);
                var go = Instantiate(GrassTile, position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform, false);
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
