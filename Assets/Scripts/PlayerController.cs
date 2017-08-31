using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;

	private float lastXPoint;

	private float lastYpoint;

	public LayerMask whatIsGround;

	private bool grounded;

	public int hearts = 3; //!< vidas do jogador

	private bool isDead;

	//private int jumpCount = 0;

	//public int maxJumps;

    // movement
    public float movementSpeed = 3;

    // jump
    public float jumpForce;
	//public float secondJumpForce = 100;
    private bool isJump;

	private float power; //!< forca adicional para o pulo
	public float powerStep = 0.5f; //!< valor que é incrementado p/ aumentar o 'power'
	public float maxPower = 60; //!< máximo valor de 'power'

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        isJump = false;
		isDead = false;
		grounded = true;
		power = 0;
		lastXPoint = rb.transform.position.x;
		lastYpoint = rb.transform.position.y;
    }

    void Update()
    {
		if (grounded && isJump) {
			isJump = false;
			//Debug.Log ("grounded");
			/*if(jumpCount <= maxJumps)
				jumpCount = 0;*/
		}

		if (!isDead) {
			Action ();
		} else {
			WaitingComands ();
		}
    }

	private void Attack(){
		if (Physics2D.OverlapCircle (transform.position, 0.2f, whatIsGround).attachedRigidbody.tag == "Enemy") 
		{
			Debug.Log("acertou o inimigo");
		}
	}

	public void ResetPosition(){
		rb.transform.position = new Vector3 (lastXPoint, lastYpoint, 1);
	}

	// Ações do jogador enquanto vivo
	private void Action()
	{
		transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * movementSpeed * Time.deltaTime;
		grounded = Physics2D.OverlapCircle (transform.position, 0.2f, whatIsGround);

		if (Input.GetButton ("Jump") && power <= maxPower) {
			power += powerStep;
		}

		if (Input.GetButtonUp("Jump") && grounded && !isJump)
		{
			float aux = jumpForce;
			jumpForce += power;
			Debug.Log (jumpForce);
			rb.AddForce (transform.up * jumpForce);
			jumpForce = aux;
			power = 0;
			isJump = true;
			//jumpCount += 1;
		}
		/*else if (jumpCount <= maxJumps && isJump && Input.GetButtonDown("Jump")) {
			Debug.Log ("isJump");
			playerRigidBody.AddForce (transform.up * secondJumpForce);
			jumpCount += 1;
		}*/
	}

	private void WaitingComands(){
		if (Input.GetButtonDown ("Submit")) {
			ResetPosition ();
		}
	}
		
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Boundary") 
		{
			Debug.Log ("saiu do limite . . .");
			isDead = true;
			Debug.Log ("Morto !!");
		}
	}

}
