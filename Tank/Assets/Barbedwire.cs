using UnityEngine;
using System.Collections;

namespace Complete
{


    public class Barbedwire : MonoBehaviour
    {

        public float slow;
        public float dmg;




        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Tank"))
            {
                TankMovement tankMovement = other.GetComponent<TankMovement>();
                tankMovement.m_Speed = 5;
            }

        }
        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Tank"))
            {
                TankMovement tankMovement = other.GetComponent<TankMovement>();
                tankMovement.m_Speed = 15;
            }

        }
    }
}
