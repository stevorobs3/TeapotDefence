using UnityEngine;

public class SteamShredder : MonoBehaviour {

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D col) {
        var steam = col.GetComponent<Steam>();
        if (steam != null)
            Destroy(steam.gameObject);
    }
}
