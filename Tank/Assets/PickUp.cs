using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Complete
{
    public class PickUp : MonoBehaviour,ISpawnable
    {
        public float healthAmount = 50f;
        private spawn _spawn;

        void Awake()
        {
            //wenn es in den optionen ausgestellt wurde wird es sofort zerstört, besser wäre es wenn es garnicht gespawn wird....
            if (this.CompareTag("Canister") && PlayerPrefs.GetString("canister") == "False")
            {
                Destroy(gameObject);
            }
            else if (this.CompareTag("Shield") && PlayerPrefs.GetString("shield") == "False")
            {
                Destroy(gameObject);
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Tank"))
            {
                gameObject.SetActive(false);
                Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
                TankHealth tank = targetRigidbody.GetComponent<TankHealth>();
                if (this.CompareTag("Canister")){
                    tank.Heal(healthAmount);
                }
                else if (this.CompareTag("Shield")){
                    tank.aktivateShield();
                }
                    _spawn.ObjectPicked(this);
            }

        }
        public void setSpawner(spawn spawn)
        {
            _spawn = spawn;
        }
    }
}
