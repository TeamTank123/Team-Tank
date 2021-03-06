﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinePlanting : MonoBehaviour {


    public Slider slider;                        
    public Image fillImage;
    public int m_PlayerNumber = 1; 
	public Rigidbody mine; 
	public Transform tankTransform;
    public float mineCd = 3f;

    private float _mineCd;
	private string _mineButton;


	private void Start ()
	{
        _mineCd = mineCd;
		_mineButton = "Mine" + m_PlayerNumber;
	}

	void Update () {

        mineCd -= Time.deltaTime;
		if (Input.GetButtonDown (_mineButton) && mineCd <= 0f)
		{
			Plant ();
		}
        SetMineUI();
    }

	public void Plant ()
	{
		mine.transform.position = new Vector3(tankTransform.position.x, 0.1f, tankTransform.position.z);
		Rigidbody mineInstance =
			Instantiate (mine) as Rigidbody;

        mineCd = _mineCd;
	}

    private void SetMineUI()
    {
        slider.value = 100/(_mineCd/ mineCd);
        
    }
}
