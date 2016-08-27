using UnityEngine;
using System.Collections;

public class PlayerControllerSphere : MonoBehaviour 
{
	public float speed;
	private int count;
	//public GUIText countText;
	//public GUIText winText;

	public float jumpHeight = 6.0f;
	public float superJumpHeight = 18.0f;
	//float gravity = -20;
	public float turnSpeed = 300.0f;
	
	public bool isMobile = false;
	
	public float torque;
	public Rigidbody rb;

	public int healthPoints;

	//Will this be enough of a change for github versioning?

	void Start()
	{
		count = 0;
		//SetCountText();
		//winText.text = "";


		rb = GetComponent<Rigidbody>();

		healthPoints = 100;

	}



	void FixedUpdate ()
	{
				float moveHorizontal = Input.GetAxis("Horizontal");
				float moveVertical = Input.GetAxis("Vertical");

				Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

				GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);

//*********************************************************************************
//*********************************************************************************


		//Jump Code with right amount of points
		if(count <= 2 && Input.GetKeyDown(KeyCode.Space)){
			//rb.velocity.y = jumpHeight;
			
			
			Vector3 temporaryVariable = rb.velocity;
			temporaryVariable.y = jumpHeight;
			rb.velocity = temporaryVariable;
			
		}
		//Double Jump
		else if(count >= 3 && Input.GetKeyDown(KeyCode.Space)){ //when set at 0, player can jump at anytime
			//rb.velocity.y = jumpHeight;
			
			Vector3 temporaryVariable = rb.velocity;
			temporaryVariable.y = superJumpHeight;
			rb.velocity = temporaryVariable;
			
		}

		//Basic health system - deducts 10 health points when player presses the h key
		if (Input.GetKeyDown (KeyCode.H)) {

			healthPoints = healthPoints - 10;

			Debug.Log(healthPoints);
		}
		//********************************************************************************
		//*********************************************************************************

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PickUp") 
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			//SetCountText();
		}
	}

	/*
	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
		if (count >= 9) 
		{
			winText.text = "YOU WIN!";
		}
	}
	*/
}
