using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private float lastXPoint;
	private float lastYpoint;
	private bool isJump;
	private float power;                         //!< forca adicional para o pulo
	private bool grounded;
	private bool isDead;
	private float lastHitTime;
	private int currentLife;
    private bool isImmune;

	public LayerMask whatIsGround;
    public float movementSpeed = 3;
    public float jumpForce;
	public float powerStep = 0.5f;              //!< valor que é incrementado p/ aumentar o 'power'
	public float maxPowerJump = 60;             //!< máximo valor de 'power'
	public int lives = 3;                       //!< vidas do jogador
	public float immuneTime;
    public float chock = 100;                    //!< Força que empurra o jogador quando chocar com o inimigo

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        isJump = false;
		isDead = false;
		grounded = true;
		power = 0;
		currentLife = lives;
        isImmune = false;
		lastXPoint = rb.transform.position.x;
		lastYpoint = rb.transform.position.y;
    }

    void Update()
    {
		if (grounded && isJump) {
			isJump = false;
		}

		if (!isDead) {
			transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * movementSpeed * Time.deltaTime;
			grounded = Physics2D.OverlapCircle (transform.position, 0.2f, whatIsGround);
		
			if (Input.GetButton ("Jump") && power <= maxPowerJump) {
				power += powerStep;
			}

			if (Input.GetButtonUp("Jump") && grounded && !isJump)
			{
				float aux = jumpForce;
				jumpForce += power;
				rb.AddForce (transform.up * jumpForce);
				jumpForce = aux;
				power = 0;
				isJump = true;
			}

            if (isImmune && (Time.time - lastHitTime) >= immuneTime )
            {
                Debug.Log(Time.time - lastHitTime);
                isImmune = false;
            }
			
		} else {
			if (Input.GetButton ("Submit")) {
				ResetPosition ();
				isDead = false;
				currentLife = lives;
			}
		}
		
    }

	private void Attack(){
		// TODO: Ataque do jogador . . .
	}

	public void ResetPosition(){
		rb.transform.position = new Vector3 (lastXPoint, lastYpoint, 1);
	}

	public void TookDamage(){
        if (!isImmune)
        {
            lastHitTime = Time.time;
            currentLife--;
            isImmune = true;
            Debug.Log("levou dano !");
        }

        if (currentLife <= 0)
        {
            isDead = true;
            ResetPosition();
        }
    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
            MoveEnemy enemy = other.GetComponent<MoveEnemy>();
            if(enemy != null)
            {
                if (enemy.side)
                {
                    rb.AddForce(transform.right * -chock);
                }
                else
                {
                    rb.AddForce(transform.right * chock);
                }
            }
            else
            {
                Debug.Log("Nao encontrou o script");
            }
            
            rb.AddForce(transform.up * jumpForce);
			TookDamage ();
		}
	}
		
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Boundary") {
			ResetPosition ();
		}
	}

}
