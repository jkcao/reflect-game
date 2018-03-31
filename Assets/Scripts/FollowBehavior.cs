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

	float cameraSpeed = 0.5f;

	/* Pre-Level Start: Camera moves up slowly until the topmost platform is visible, and slowly moves down
	 * 
	 * Camera Properties: 
	 * - Maximum height to go up for showing the level
	 * - Speed of camera moving up/down before
	 * - Perhaps some sound/music to indicate that this is level-showing time
	 * 
	 * Then level starts normally
	 * 
	 */

	/* Panning left + right: use "-" and "=" key respectively
	* 
	* When input key press = "-", move the cameras to the left
	* When input key press = "+", move the cameras to the right
	* Jump key would reset the camera position
	* 
	*/

	bool gameStart;

	void Start() {

		// Inverse for right camera
		if (gameObject.name == "CCamera") {
			print ("CCamera");
		} else if (gameObject.name == "RCamera") {
			cameraSpeed = -1 * cameraSpeed;
			print ("RCamera");
		}

		// Initial Setup
		float ychange = transform.position.y;
		if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;
		transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);

		// GameStart false at the start
		gameStart = false;

		// After Camera Panning, resume game
		gameStart = true;
		print("Start loop");
	}

	// Update is called once per frame
	void Update()
	{

		float ychange = transform.position.y;

		// ----- Camera Panning ------ //

		// Pan Right
		if(Input.GetKey(KeyCode.Semicolon))
		{
			gameStart = false;
			transform.position = new Vector3(transform.position.x + cameraSpeed, transform.position.y, transform.position.z);
			print ("Camera: Semicolon key pressed");
		}

		// Pan Left
		if(Input.GetKey(KeyCode.Quote))
		{
			gameStart = false;
			transform.position = new Vector3(transform.position.x - cameraSpeed, transform.position.y, transform.position.z);
			print ("Camera: Quote key pressed");
		}

		// ---- Zooming in and out ---- //

		// Zoom Out
		if(Input.GetKey(KeyCode.UpArrow))
		{
			// Camera 1
			if (Camera.allCameras[1].fieldOfView<=125)
				Camera.allCameras[1].fieldOfView +=2;
			if (Camera.allCameras[1].orthographicSize<=20)
				Camera.allCameras[1].orthographicSize +=0.5f;

			// Camera 1
			if (Camera.allCameras[0].fieldOfView<=125)
				Camera.allCameras[0].fieldOfView +=2;
			if (Camera.allCameras[0].orthographicSize<=20)
				Camera.allCameras[0].orthographicSize +=0.5f;
		}

		// Zoom In
		if(Input.GetKey(KeyCode.DownArrow))
		{
			// Camera 2
			if (Camera.allCameras[1].fieldOfView>2)
				Camera.allCameras[1].fieldOfView -=2;
			if (Camera.allCameras[1].orthographicSize>=1)
				Camera.allCameras[1].orthographicSize -=0.5f;

			// Camera 1
			if (Camera.allCameras[0].fieldOfView>2)
				Camera.allCameras[0].fieldOfView -=2;
			if (Camera.allCameras[0].orthographicSize>=1)
				Camera.allCameras[0].orthographicSize -=0.5f;
		}

		// ----- Snaps Camera back to Player ------ //

		// Left or Right Arrow
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
		{
			reattachCamera (ychange);
			print ("Left key pressed - Camera attached");
		}

		// a, w or d key
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
		{
			reattachCamera (ychange);
			print ("Right key pressed - Camera attached");
			print ("Camera attached");
		}

		// space, j or l key
		if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
		{
			reattachCamera (ychange);
			print ("Camera attached");
		}

		// left or right mouse key
		if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
		{
			reattachCamera (ychange);
			print ("Camera attached");
		}

		// ----- GameStart - ReAttach Camera ----- //
		if (gameStart == true) {
			reattachCamera (ychange);
		} else {
			//print ("Camera is in level-exploring mode");
		}
	}



	// -- Helpers --

	// Reattaches camera and starts game
	private void reattachCamera(float ychange) {
		ychange = transform.position.y;
		if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;
		transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);
		gameStart = true;
	}


}
