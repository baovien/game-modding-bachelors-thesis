using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListener : MonoBehaviour
{
	private bool craftState;
	private CanvasGroup cg;	

	private void Start()
	{
		cg = GameObject.FindGameObjectWithTag("CraftingWindow").GetComponent<CanvasGroup>();
		
		cg.interactable= false;
		cg.alpha = 0;
		
	}

	// Update is called once per frame
	void Update () {
		// a crafting test
		if (Input.GetKeyDown(KeyCode.C))
		{
			craftState = !craftState;

			if (craftState)
			{
				cg.interactable = true;
				cg.alpha = 1;
			}
			else
			{
				cg.interactable= false;
				cg.alpha = 0;
			}
		}
	}
}
