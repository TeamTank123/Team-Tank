using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiTank : MonoBehaviour {

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

    public AudioSource fireAudio;
    public AudioSource reloadAudio;

    private float fireCdTmp;
    private GameObject[] _enemy;
    private float _shootingIdleTime = 1f;



    public List<Vector3> coordinaten;
    public float speed = 2;
    private Rigidbody _rigidbody;
    private int moveCordinaten = 0;
    private int listIndecator = 1;   // 1 and -1





    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        _enemy = GameObject.FindGameObjectsWithTag(enemyTag);
        UpdateTarget();



        if (target == null)
        {
            _shootingIdleTime = 1f;
            Move();

            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        _rigidbody.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (target.tag.Equals("Tank"))
        {
            _shootingIdleTime -= Time.deltaTime;
            fireCdTmp -= Time.deltaTime;
            if (target != null && fireCdTmp <= 0 && target.GetComponent<Complete.TankHealth>().getCurrentHealth() > 0 && _shootingIdleTime <= 0)
            {
                Fire();
                fireCdTmp = fireCd;
            }
        }
    
    }

    private void Move()
    {
        if(IsNearPosition())
        {
            moveCordinaten += listIndecator;
            if (moveCordinaten >= coordinaten.Count-1 || moveCordinaten == 0)
            {
                listIndecator *= -1;
            }
        }

        Vector3 dir = coordinaten[moveCordinaten] - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        _rigidbody.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward  * speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    private bool IsNearPosition()
    {

        float dist = Vector3.Distance(coordinaten[moveCordinaten], transform.position);
        if (dist <= 1.0f)
        {
            return true;
        }

        return false;
    }

    void Start()
    {
        fireCdTmp = fireCd;
    }

    void UpdateTarget()
    {

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject e in _enemy)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, e.transform.position);

            Complete.TankShooting t = e.gameObject.GetComponent<Complete.TankShooting>();
            if (t != null)
            {
                if (distanceToEnemy < shortestDistance && playerNumber != e.GetComponent<Complete.TankShooting>().m_PlayerNumber)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = e;
                }
            }

        }


        if (nearestEnemy != null && shortestDistance <= range && target != nearestEnemy.transform)
        {
            target = nearestEnemy.transform;
            reloadAudio.Play();

        }
        else if (shortestDistance > range)
        {
            target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, coordinaten[moveCordinaten]);
        }

        for(int i = 0; i < coordinaten.Count-1; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(coordinaten[i], coordinaten[i+1]);
        }
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

        fireAudio.Play();

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 5.0f);
    }

}
