using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{
	public Transform target;
	public float relativeHeigth = 80.0f;
	public float zDistance = 50.0f;
	public float dampSpeed = 2;
	
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 newPos = target.position + new Vector3 (0, relativeHeigth, -zDistance);
		transform.position = Vector3.Lerp (transform.position, newPos, Time.deltaTime * dampSpeed);
	}
}
