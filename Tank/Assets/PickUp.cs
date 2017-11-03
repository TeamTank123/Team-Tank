using UnityEngine;
using System.Collections;
namespace Complete
{
    public class PickUp : MonoBehaviour
    {
        public float healthAmount = 50f;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Tank"))
            {
                gameObject.SetActive(false);
                Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
                TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
                targetHealth.Heal(healthAmount);
            }

        }
    }
}
