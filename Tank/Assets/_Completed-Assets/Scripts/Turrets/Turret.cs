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

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireCd;
    public float bulletSpeed;

    private float fireCdTmp;
    private GameObject[] _enemy;

    void Start()
    {
        fireCdTmp = fireCd;
    }

    void UpdateTarget () {
       
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject e in _enemy)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, e.transform.position);

            Complete.TankShooting t = e.gameObject.GetComponent<Complete.TankShooting>();
          
                if (distanceToEnemy < shortestDistance  && playerNumber != e.GetComponent<Complete.TankShooting>().m_PlayerNumber)
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
        _enemy = GameObject.FindGameObjectsWithTag(enemyTag);
        UpdateTarget();



        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed ).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        fireCdTmp -= Time.deltaTime;
        if (target != null&& fireCdTmp <= 0)
        {
            Fire();
            fireCdTmp = fireCd;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 5.0f);
    }
}
