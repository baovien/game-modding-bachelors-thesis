using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

	public Transform player;

	private new Camera camera;
	
	
	
	private void LateUpdate()
	{
		Vector3 newPos = player.position;
		newPos.z = transform.position.z;
		transform.position = newPos;
	}

	public void ZoomIn()
	{
		
	}
}
