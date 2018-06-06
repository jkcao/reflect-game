using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public GameObject platform;
	public float speed;
	public Transform currPt;
	public Transform[] pts;	
	public int ptSelection;
	public bool isActive;

	// Use this for initialization
	void Start () {
		currPt = pts [ptSelection];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isActive) {
			platform.transform.position = Vector3.MoveTowards (platform.transform.position, currPt.position, Time.deltaTime * speed);
			if (platform.transform.position == currPt.position) {
				ptSelection++;
				if (ptSelection == pts.Length) {
					ptSelection = 0;
				}
				currPt = pts [ptSelection];
			}
		}
	}
}
