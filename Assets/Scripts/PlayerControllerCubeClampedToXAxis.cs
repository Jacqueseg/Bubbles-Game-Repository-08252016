using UnityEngine;
using System.Collections;

public class PlayerControllerCubeClampedToXAxis: MonoBehaviour 
{
	public float speed;
	private int count;
	public GUIText countText;
	public GUIText winText;

	public int jumpCount;
	public int maxJumpCount = 1;

	public float jumpHeight = 6.0f;
	public float superJumpHeight = 18.0f;
	//float gravity = -20;
	public float turnSpeed = 300.0f;

	public bool isMobile = false;
	public bool jumping;
	public float jumpTimer = 0.0f;

	public float torque;
	public Rigidbody rb;


	void Start()
	{
		count = 0;
		SetCountText();
		winText.text = "";

		rb = GetComponent<Rigidbody>();
		rb.maxAngularVelocity = 5.5f;		//Whenever the player picks up collectibles, the maxAngularVelocity will be adjusted as a variable for each level/powerup

	}

	void FixedUpdate ()
	{

		if(rb.IsSleeping()){
			rb.WakeUp();
			//debug.Log(awake);
		}

		if(jumping == true){
			jumpTimer -= Time.deltaTime;

			if (jumpTimer <= 0.0f)
				jumping = false;
			
		}


		if(Input.GetKeyDown(KeyCode.Space))
		{

			if(jumpCount < maxJumpCount)
			{
				jumpCount++;
				PlayerJump();

			}

			else
			{
				jumping = true;
				jumpTimer = .3f;
			}
		}


	


/*
	//Jump Code with right amount of points
		if(count == 1 && Input.GetKeyDown(KeyCode.Space)){
			//rb.velocity.y = jumpHeight;
			
			
			Vector3 temporaryVariable = rb.velocity;
			temporaryVariable.y = jumpHeight;
			rb.velocity = temporaryVariable;


		//2 Second Stick To walls and Rock forward
		else if(count >= 3 && Input.GetKeyDown(KeyCode.Space)){
			//rb.velocity.y = jumpHeight;

			Vector3 temporaryVariable = rb.velocity;
			temporaryVariable.y = superJumpHeight;
			rb.velocity = temporaryVariable;
			
		}
*/				

		//The player is unable to turn/rotate left and right
		//while moving/rocking forward and back along the x-axis

			float rockForward = Input.GetAxis("Horizontal");
			//rb.AddTorque(-transform.forward * torque * rockForward);
			rb.AddTorque(-Vector3.forward * torque * rockForward);
			//print(rockForward);
			//isMobile = true;



		//The player is unable to turn/rotate left and right
		//while moving/rocking forward and back along the x-axis

		float rockUpAndDown = Input.GetAxis("Vertical");
		//rb.AddTorque(-transform.forward * torque * rockForward);
		rb.AddTorque(Vector3.right * torque * rockUpAndDown);
		//print(rockForward);
		//isMobile = true;



		/*
		//If player is not mobile (stationary), treat its bottom as the "base" and 
		//allow the player to rotate around it's y-axis

			if (Input.GetKey (KeyCode.LeftArrow))
					transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
		
			if (Input.GetKey (KeyCode.RightArrow))
					transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);

		*/

	}
			

	void OnCollisionStay(Collision stay)
	{
		if (jumping == true){
			PlayerJump();
			
		}
			jumpCount = 0;
			//Debug.Log(jumping);

	}


	void PlayerJump()
	{

		//Jumping Code Works!

			Vector3 temporaryVariable = rb.velocity;
			temporaryVariable.y += jumpHeight;
			rb.velocity = temporaryVariable;
			jumping = false;

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PickUp") 
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
		if (count >= 5) 
		{
			winText.text = "YOU WIN!";
		}
	}
}
	


/*
 //Double Jump
		else if(count >= 0 && Input.GetKeyDown(KeyCode.Space)){ //when set at 0, player can jump at anytime
			//rb.velocity.y = jumpHeight;

			Vector3 temporaryVariable = rb.velocity;
			temporaryVariable.y = superJumpHeight;
			rb.velocity = temporaryVariable;
			
		}



		Spinning Octopus
		float turnSideways = Input.GetAxis("Horizontal");
		rb.AddTorque(transform.up * torque * turnSideways);

				float moveHorizontal = Input.GetAxis("Horizontal");
				float moveVertical = Input.GetAxis("Vertical");

			Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
			Vector3 direction = rigidbody.transform.position - transform.position;
			rigidbody.AddForceAtPosition(direction.normalized, transform.position);

*/
