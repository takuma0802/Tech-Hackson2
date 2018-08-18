using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D col)
	{
		SceneManager.LoadScene("Clear");	
	}
}
