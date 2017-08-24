using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D playerRigidBody;

	public LayerMask whatIsGround;

	private bool grounded;
	private int jumpCount = 0;

	public int maxJumps;

    // movement
    public float speed = 3;

    // jump
    public float jumpForce;
	public float secondJumpForce = 100;
    private bool isJump;

	private float power;
	public float powerInc = 0.5f;
	public float maxPower = 60;

    void Start()
    {
        isJump = false;
		grounded = true;
		power = 0;
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

		if (grounded) {
			Debug.Log ("grounded");
			isJump = false;
			if(jumpCount <= maxJumps)
				jumpCount = 0;
		}
		
		transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * Time.deltaTime;
		grounded = Physics2D.OverlapCircle (transform.position, 0.2f, whatIsGround);

		if (Input.GetButton ("Jump") && power <= maxPower) {
			power += powerInc;
		}

		if (Input.GetButtonUp("Jump") && grounded && !isJump)
		{
			float aux = jumpForce;
			jumpForce += power;
			Debug.Log (jumpForce);
			playerRigidBody.AddForce (transform.up * jumpForce);
			jumpForce = aux;
			power = 0;
			isJump = true;
			jumpCount += 1;
		}else if (jumpCount <= maxJumps && isJump && Input.GetButtonDown("Jump")) {
			Debug.Log ("isJump");
			playerRigidBody.AddForce (transform.up * secondJumpForce);
			jumpCount += 1;
		}
		
			
    }

}
