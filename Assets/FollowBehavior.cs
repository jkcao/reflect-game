using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : MonoBehaviour {

	[SerializeField]
	protected Transform trackingTarget;

	[SerializeField]
	float xOffset;

	[SerializeField]
	float yOffset;

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(trackingTarget.position.x + xOffset, 
			trackingTarget.position.y + yOffset, transform.position.z);
	}
}
