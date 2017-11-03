using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

    public float growthRate = 0.01f;
    public float maxGrowth = 0.05f;
    public string rotateAxis = "x";
    public float rotateSpeed = 10;

    private float _maxSize;
    void Start()
    {
        _maxSize = transform.localScale.x;
    }
    void Update () {
        if (rotateAxis.Equals("x")){
            transform.Rotate(new Vector3(rotateSpeed, 0, 0) * Time.deltaTime);
        }
        else if (rotateAxis.Equals("y")){
            transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
        }
        else if (rotateAxis.Equals("z")){
            transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
        }
   


        transform.localScale = new Vector3(transform.localScale.x + growthRate, transform.localScale.y + growthRate, transform.localScale.z + growthRate);
        if (transform.localScale.x >= _maxSize + maxGrowth/2)
        {
            growthRate = growthRate* -1;
        }
        else if (transform.localScale.x <= _maxSize - maxGrowth / 2)
        {
            growthRate = growthRate * -1;
        }
    }

  

}
