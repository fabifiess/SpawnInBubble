using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Dragable : MonoBehaviour
{
	public GameObject mgmt;
	DragManager dragManager;
	private Color realCol;

	void Start ()
	{
		dragManager = mgmt.GetComponent<DragManager>();
		realCol = GetComponent<Renderer>().material.color;
	}

	private void OnMouseDown()
	{
		dragManager.ActivateDragObject(gameObject);
	}

	private void OnMouseEnter()
	{
		if (dragManager.curDragObj != null && dragManager.curDragObj.tag == dragManager.tagEffect)
		{
			GetComponent<Renderer>().material.color = Color.blue;
		}
	}

	private void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = realCol;
	}
}
