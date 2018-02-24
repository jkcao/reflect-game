using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlatform : MonoBehaviour {

	private float toGround;
	private GameObject allocated;
	
	// Use this for initialization
	void Start () {
		toGround = Physics2D.Raycast (this.gameObject.transform.position, Vector2.down).distance;
		Debug.Log (toGround);
		allocated = null;
	}

	protected void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Player") {
			Player p = col.gameObject.GetComponent<Player>();
			Vector3 mirrored = new Vector3(-(this.transform.position.x - p.transform.position.x) + p.mirror.transform.position.x,
											p.mirror.getGroundPosition() + toGround, this.transform.position.z);
			allocated = Instantiate (this.gameObject);
			Destroy(allocated.GetComponent<MirrorPlatform>());
			allocated.transform.position = mirrored;
		}
	}

	protected void OnCollisionExit2D (Collision2D col){
		if (col.gameObject.tag == "Player") {
			Destroy (allocated);
			allocated = null;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
