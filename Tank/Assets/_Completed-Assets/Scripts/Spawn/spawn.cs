using System;
using System.Collections;
using System.Collections.Generic;
using Complete;
using UnityEngine;

public class spawn : MonoBehaviour {

    public GameObject objectPrefab;
    public Vector3 center;
    public Vector3 size;

    public float spawnCd;
    public int spawnLimit;

    private float spawnTimer;

    private void Start()
    {
        spawnTimer = spawnCd;
    }

    void Update () {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f && spawnLimit>0)
        {
            spawnTimer = spawnCd;
            spawnLimit -= 1;
            SpawnObject();
        }
    }

    internal void ObjectPicked(ISpawnable iSpawnable)
    {
        spawnTimer = spawnCd;
        spawnLimit++;
    }

    public void SpawnObject()
    {
        Vector3 pos = center + new Vector3(UnityEngine.Random.Range(-size.x / 2, size.x / 2),0, UnityEngine.Random.Range(-size.z / 2, size.z / 2));
        GameObject newSpawnObject = Instantiate(objectPrefab, pos,Quaternion.identity);
        var iSpawnableScript = newSpawnObject.GetComponent<ISpawnable>();
        if (iSpawnableScript != null)
        {
            iSpawnableScript.setSpawner(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.45f);
        Gizmos.DrawCube(center, size);
    }

}
