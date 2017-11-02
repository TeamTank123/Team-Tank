using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

    public float growthRate = 0.01f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        transform.localScale = new Vector3(transform.localScale.x + growthRate, transform.localScale.y + growthRate, transform.localScale.z + growthRate);
        if (transform.localScale.x >= 1f)
        {
            growthRate = -0.01f;
        }
        else if (transform.localScale.x <= 0.5f    )
        {
            growthRate = 0.01f;
        }
    }

  

}
