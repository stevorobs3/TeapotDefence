using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public float speed = 2f;

	private Rigidbody body;

	void Awake () 
	{
		body = GetComponent <Rigidbody> ();
	}

	void FixedUpdate () 
	{
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		body.AddForce (speed * direction, ForceMode.VelocityChange);
	}
}
