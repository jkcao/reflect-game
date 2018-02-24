using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

	protected void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
