using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour 
{
	public Character character;

	public float smoothTime = 0.4f;

	private Vector3 speed = Vector3.zero;

	void Update () 
	{
		Vector3 newPos = Vector3.SmoothDamp (transform.position, character.gameObject.transform.position, ref speed, smoothTime);
		newPos.z = -5f;
		transform.position = newPos;
	}
}
