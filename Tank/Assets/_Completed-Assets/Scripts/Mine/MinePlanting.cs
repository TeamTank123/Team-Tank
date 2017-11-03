using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlanting : MonoBehaviour {

	public int m_PlayerNumber = 1; 
	public Rigidbody mine; 
	public Transform tankTransform;

	private string _mineButton;


	private void Start ()
	{
		_mineButton = "Mine" + m_PlayerNumber;
	}

	void Update () {
		if (Input.GetButtonDown (_mineButton))
		{
			Plant ();
		}
	}

	public void Plant ()
	{
		mine.transform.position = new Vector3(tankTransform.position.x, 0.1f, tankTransform.position.z);
		Rigidbody mineInstance =
			Instantiate (mine) as Rigidbody;
	}
}
