using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Esse objeto geralmente será uma referencia para o jogador
	public Transform target;

	public float x = 0.5f;
	public float y = 1f;
	public float z = -1f;

	public float smoothTime = 0.3f;

	private Vector3 velocity = Vector3.zero;

	// Update is called once per frame
	void Update () {

		Vector3 targetPosition = target.TransformPoint (new Vector3 (x, y, z));
		this.transform.position = Vector3.SmoothDamp(this.transform.position, 
			targetPosition,
			ref velocity,
			smoothTime
		);
	}
		
}
