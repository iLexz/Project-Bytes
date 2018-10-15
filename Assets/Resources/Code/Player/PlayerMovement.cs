using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

	private CharacterController character;
	[SerializeField] private GameObject playerCam;

	//Movement variables
	private float moveFB, moveLR;
	[SerializeField] private float speedFB, speedLR;
	
	//Mouse variables
	private float rotX, rotY;
	public float mouseSensitivity = 3;

	//Physics/Jump/Sprinting/Crouching variables
	[SerializeField] private float jumpForce = 4;
	[SerializeField] private bool hasJumped;
	[SerializeField] private bool canJump;

	[SerializeField] private float sprintingSpeedFB = 6;
	[SerializeField] private float sprintingSpeedLR = 5;
	[SerializeField] private bool isSprinting;

	[SerializeField] private float crouchedSpeedFB = 4;
	[SerializeField] private float crouchedSpeedLR = 2.5f;
	[SerializeField] private bool isCrouched;
	[SerializeField] private int characterHeightWhenCrouched = 1;

	/*The speed at which the player falls down. It's normally set to 9.81f by the Physics.Gravity.y function
	 * in line 100*/
	[SerializeField] private float verticalVelocity;


	// Use this for initialization
	private void Start ()
	{
		//Setting V-Sync to 0
		QualitySettings.vSyncCount = 0;

		//Variables
		speedFB = 5;
		speedLR = 3.5f;

		character = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	private void Update ()
	{
		Movement();
		ApplyGravity();
		Crouch();
		Sprint();
	}

	private void Movement()
	{
		moveFB = Input.GetAxis("Vertical") * speedFB;
		moveLR = Input.GetAxis("Horizontal") * speedLR;

        rotY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
		rotX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;

		Vector3 movement = new Vector3(moveLR, verticalVelocity, moveFB);

		transform.Rotate(0, rotX, 0);
		//Only god knows what happens here. It works, so I won't touch it. Let's just not bring it up. Ever again.
		playerCam.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
		rotY = Mathf.Clamp(rotY, -60f, 60f);
		movement = transform.rotation * movement;

		character.Move(movement * Time.deltaTime);


		if (Input.GetButtonDown("Jump"))
		{
			Jump();
		}
	}

	private void Jump()
	{
		hasJumped = true;
	}

	private void ApplyGravity()
	{
		if (character.isGrounded)
		{
			if(hasJumped == false)
			{
				verticalVelocity = Physics.gravity.y;
			}
			else
			{
				verticalVelocity = jumpForce;
			}
		}
		else
		{
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
			//clamping the falling speed
			verticalVelocity = Mathf.Clamp(verticalVelocity, -20, jumpForce);
			hasJumped = false;
		}
	}

	private void Sprint()
	{
		if (Input.GetButton("Sprint"))
		{
			isSprinting = true;
			isCrouched = false;
			if (isSprinting)
			{
				speedFB = sprintingSpeedFB;
				speedLR = sprintingSpeedLR;
			}
		}
		else
		{
			isSprinting = false;
			if(isSprinting == false)
			{
				speedFB = 5;
				speedLR = 3.5f;
			}

		}
	}

	private void Crouch()
	{
		if (Input.GetButton("Crouch"))
		{
			isCrouched = true;
			if (isCrouched)
			{
				character.height = characterHeightWhenCrouched;
				speedFB = crouchedSpeedFB;
				speedLR = crouchedSpeedLR;

                if (isSprinting)
				{
					character.height = 2;
					speedFB = sprintingSpeedFB;
					speedLR = sprintingSpeedLR;
				}
			}
		}
		else
		{
			isCrouched = false;
			if(isCrouched == false)
			{
				if (isSprinting)
				{
					character.height = 2;
					speedFB = sprintingSpeedFB;
					speedLR = sprintingSpeedLR;
				}
				else
				{
					character.height = 2;
					speedFB = 5;
					speedLR = 3.5f;
				}
			}
		}
	}
}
