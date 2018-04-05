using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Play using default keys
 * Camera Panning: Use ; and " keys to pan left and right
 * 
 */

public class FollowBehavior : MonoBehaviour {

	[SerializeField]
	protected Transform trackingTarget;

	[SerializeField]
	float xOffset;

	[SerializeField]
	float yOffset;

	// Panning Camera Speed (using ';' and '"' keys)
	public float cameraSpeed;

	// Camera: Level Walkthrough Properties
	public Transform initialPosition;
	public Transform finalPosition;
	public float speed;
	public Color backgroundColor;

	/* Pre-Level Start: Camera moves up slowly until the topmost platform is visible, and slowly moves down	*/

	private bool gameStart;
	private bool levelWalkthrough;

	void Start() {

		// Inverse for right camera
		if (gameObject.name == "RCamera") {
			cameraSpeed = -1 * cameraSpeed;
		}

		// Initial Setup
		float ychange = transform.position.y;
		reattachCamera (ychange);

		// GameStart false at the start
		gameStart = false;
		levelWalkthrough = false;

		this.backgroundColor = backgroundColor;

	}

	// Update is called once per frame
	void Update()
	{
		// Properties - Setup
		float ychange = transform.position.y;

		/*
		// ----- Level Walkthrough ---- //
		if (levelWalkthrough == true) {
			float step = speed * Time.deltaTime;
			Camera.main.transform.position = Vector3.MoveTowards (Camera.main.transform.position, finalPosition.position, step);
			Camera.allCameras [1].transform.position = Vector3.MoveTowards (Camera.allCameras [1].transform.position, finalPosition.position, step);
			if (Camera.main.transform.position == finalPosition.position || Camera.allCameras [1].transform.position == finalPosition.position) {
				reattachCamera (initialPosition.position.y);
			}
		}
		*/

		// ----- Camera Panning ------ //

		// Pan Right
		if(Input.GetKey(KeyCode.RightArrow))
		{
			gameStart = false;
			if((gameObject.name == "RCamera" && transform.position.x > 10)
				|| (gameObject.name == "CCamera" && transform.position.x < -10))
				transform.position = new Vector3(transform.position.x + cameraSpeed, transform.position.y, transform.position.z);
		}

		// Pan Left
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			gameStart = false;
			if((gameObject.name == "RCamera" && transform.position.x < 100)
				|| (gameObject.name == "CCamera" && transform.position.x > -100))
			transform.position = new Vector3(transform.position.x - cameraSpeed, transform.position.y, transform.position.z);
		}

		// ---- Zooming in and out ---- //

		// Zoom Out
		if(Input.GetKey(KeyCode.UpArrow))
		{
			// Camera 1
			if (Camera.allCameras[1].fieldOfView<=12)
				Camera.allCameras[1].fieldOfView +=2;
			if (Camera.allCameras[1].orthographicSize<=12)
				Camera.allCameras[1].orthographicSize +=0.5f;

			// Camera 1
			if (Camera.allCameras[0].fieldOfView<=12)
				Camera.allCameras[0].fieldOfView +=2;
			if (Camera.allCameras[0].orthographicSize<=12)
				Camera.allCameras[0].orthographicSize +=0.5f;
		}

		// Zoom In
		if(Input.GetKey(KeyCode.DownArrow))
		{
			// Camera 2
			if (Camera.allCameras[1].fieldOfView>7)
				Camera.allCameras[1].fieldOfView -=2;
			if (Camera.allCameras[1].orthographicSize>=7)
				Camera.allCameras[1].orthographicSize -=0.5f;

			// Camera 1
			if (Camera.allCameras[0].fieldOfView>7)
				Camera.allCameras[0].fieldOfView -=2;
			if (Camera.allCameras[0].orthographicSize>=7)
				Camera.allCameras[0].orthographicSize -=0.5f;
		}

		// ----- Snaps Camera back to Player ------ //

		// a, w or d key
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D)
		   || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)
		   || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
		{
			reattachCamera (ychange);
		}

		// ----- GameStart - ReAttach Camera ----- //

		if (gameStart == true) {
			reattachCamera (ychange);
		}
	}



	// -- Helpers --

	// Reattaches camera and starts game
	private void reattachCamera(float ychange) {
		levelWalkthrough = false;
		gameStart = true;
		if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;
		transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);
	}



}
