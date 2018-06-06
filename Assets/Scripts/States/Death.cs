using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

	protected void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

	}
}
