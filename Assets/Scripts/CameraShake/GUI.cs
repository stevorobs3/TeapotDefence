using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour {

	public Text buttonText;

	public CameraShake cameraShake;
	public CameraPerlinShake cameraPerlinShake;

	private bool shakeEnabled = false;

	void Awake () {
		SetButtonText ();
	}

	public void ToggleButtonClick () {
		shakeEnabled = !shakeEnabled;
		if (shakeEnabled) {
			cameraShake.Enable ();
			cameraPerlinShake.Enable ();
		} else {
			cameraShake.Disable ();
			cameraPerlinShake.Disable ();
		}
		SetButtonText ();
	}

	private void SetButtonText () {
		if (shakeEnabled)
			buttonText.text = "Shake: ON";
		else
			buttonText.text = "Shake: OFF";
	}
}
