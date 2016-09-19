using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    public GameObject GrassTile;

    const int PIXELS_PER_TILE = 128;
    const int NUM_TILES_HIGH  = 6;
    const int NUM_TILES_WIDE = 8;

    const float X_OFFSET = -3f;
    const float Y_OFFSET = -2f;

    // Use this for initialization
    void Awake () {
        SpawnTiles();
	}

    private void SpawnTiles()
    {
        for (int i = 0; i < NUM_TILES_WIDE; i++)
        {
            for (int j = 0; j < NUM_TILES_HIGH; j++)
            {
                var position = new Vector3(i + X_OFFSET, j + Y_OFFSET, 0);
                var go = Instantiate(GrassTile, position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform, false);
            }
        }
    }
}
