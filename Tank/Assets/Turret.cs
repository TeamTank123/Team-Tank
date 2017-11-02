using UnityEngine;
using System.Collections;

namespace Complete
{


    public class Turret : MonoBehaviour
    {
        public LayerMask m_TankMask;
        public float range = 15f;
        public float smooth = 2.0F;
        public float tiltAngle = 30.0F;
        // Use this for initialization
        float maxDegreesPerSecond = 30.0f;
        Quaternion qTo;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * 4 );
            Collider[] collidersTank = Physics.OverlapSphere(transform.position, range, m_TankMask);
                
            for (int i = 0; i < collidersTank.Length; i++)
            {
                var v3T = collidersTank[i].transform.position - transform.position;
                v3T.y = transform.position.y;
                qTo = Quaternion.LookRotation(v3T, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, maxDegreesPerSecond * Time.deltaTime);
            }
        }
    }
}