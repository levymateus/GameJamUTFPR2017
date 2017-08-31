using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

	private Rigidbody2D rb;

	public float movementSpeed = 1;

	public float leftBoundary;

	public float rightBoundary;

	private bool side;

	public float moveWait;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		side = true;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move()
	{
		if (transform.position.x <= leftBoundary)
			side = false;

		if (transform.position.x >= rightBoundary)
			side = true;

		if(side)
			rb.MovePosition (transform.position - transform.right * movementSpeed * Time.deltaTime);
		else
			rb.MovePosition (transform.position + transform.right * movementSpeed * Time.deltaTime);
		
	}
}
