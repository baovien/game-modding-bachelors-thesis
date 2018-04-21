using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListener : MonoBehaviour
{
	private bool craftState;

	public GameObject craftWindow;
	
	// Update is called once per frame
	void Update () {
		// a crafting test
		if (Input.GetKeyDown(KeyCode.C))
		{
			craftState = !craftState;
			craftWindow.SetActive(craftState);
		}
	}
}
