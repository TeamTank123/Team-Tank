using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public int playerNumber = 0;
    public Transform target;
    public float range = 15f;
    public string enemyTag = "Player";

    public Transform partToRotate;
    public float turnSpeed = 7f;

    private GameObject[] _enemy;
	// Use this for initialization
	void Start () {
        _enemy = GameObject.FindGameObjectsWithTag(enemyTag);
        InvokeRepeating("UpdateTarget", 0f, .5f);

        
       
    }

    // Update is called once per frame

    void UpdateTarget () {
       
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject e in _enemy)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, e.transform.position);

            TankShooting t = e.gameObject.GetComponent<TankShooting>();
            print(t.m_PlayerNumber);
                if (distanceToEnemy < shortestDistance  && playerNumber != e.GetComponent<TankShooting>().m_PlayerNumber)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = e;

                }
            
        }


        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;

        }else { target = null;

        }
    }
	void Update () {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed ).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
