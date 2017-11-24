using UnityEngine;
using System.Collections;
namespace Complete
{
    public class PickUp : MonoBehaviour,ISpawnable
    {
        public float healthAmount = 50f;
        private spawn _spawn;
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Tank"))
            {
                gameObject.SetActive(false);
                Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
                TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
                targetHealth.Heal(healthAmount);
                _spawn.ObjectPicked(this);
            }

        }
        public void setSpawner(spawn spawn)
        {
            _spawn = spawn;
        }
    }
}
