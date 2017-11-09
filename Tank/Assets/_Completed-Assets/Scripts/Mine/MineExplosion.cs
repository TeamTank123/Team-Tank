using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class MineExplosion : MonoBehaviour
    {
		public Light blinken;
        public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
        public LayerMask m_MineMask;
        public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
        public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
        public float m_MaxDamage = 5f;                    // The amount of damage done if the explosion is centred on a tank.
        public float m_ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
        public float m_ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.

        public AudioSource minePeep;
        private bool exploded = false;                   // a variable to avoid a endlsess loop
        private float timer = 10;
		private bool aktiv = false;
		private float timeTillAkitv = 3.0f;

        private void Update()
        {
			if (aktiv == false) {
				blinken.gameObject.SetActive (false);
				timeTillAkitv = timeTillAkitv - Time.deltaTime;
				if (timeTillAkitv <= 0) {
					aktiv = true;
					blinken.gameObject.SetActive (true);
                    minePeep.Play();

                }
			}



            if(exploded == true)
            {
                timer--;
                if (timer == 0)
                {
                    OnTriggerEnter(GetComponent<Collider>());
                }
            }

        }


        public void OnTriggerEnter(Collider other)
        {
			if (aktiv == true) {
				
				exploded = true;
				//DestroyImmediate(gameObject);
				//transform.localScale = new Vector3(transform.localScale.x + 5, transform.localScale.y + 5, transform.localScale.z + 5);
				// Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.

				Collider[] collidersTank = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask);
				Collider[] collidersMine = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_MineMask);

				for (int i = 0; i < collidersMine.Length; i++) {
					MineExplosion mine = collidersMine [i].GetComponent<MineExplosion> ();
					if (mine.exploded != true) {
						mine.exploded = true;
					}
				}

				// Go through all the colliders...
				for (int i = 0; i < collidersTank.Length; i++) {
					// ... and find their rigidbody.
					Rigidbody targetRigidbody = collidersTank [i].GetComponent<Rigidbody> ();

					// If they don't have a rigidbody, go on to the next collider.
					if (!targetRigidbody)
						continue;

					// Add an explosion force.
					targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

					// Find the TankHealth script associated with the rigidbody.
					TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth> ();

					// If there is no TankHealth script attached to the gameobject, go on to the next collider.
					if (!targetHealth)
						continue;

					// Calculate the amount of damage the target should take based on it's distance from the shell.
					float damage = CalculateDamage (targetRigidbody.position);

					// Deal this damage to the tank.
					targetHealth.TakeDamage (damage);

				}

				// Unparent the particles from the shell.
				m_ExplosionParticles.transform.parent = null;

				// Play the particle system.
				m_ExplosionParticles.Play ();

				// Play the explosion sound effect.
				m_ExplosionAudio.Play ();

				// Once the particles have finished, destroy the gameobject they are on.
				Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

				// Destroy the mine.
				Destroy (gameObject);
			}

        }


        private float CalculateDamage(Vector3 targetPosition)
        {
            // Create a vector from the shell to the target.
            Vector3 explosionToTarget = targetPosition - transform.position;

            // Calculate the distance from the shell to the target.
            float explosionDistance = explosionToTarget.magnitude;

            // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
            float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

            // Calculate damage as this proportion of the maximum possible damage.
            float damage = relativeDistance * m_MaxDamage;

            // Make sure that the minimum damage is always 0.
            damage = Mathf.Max(0f, damage);

            return damage;
        }
    }



}
// other.gameObject.SetActive(false);
//TankHealth targetHealth = m_Rigidbody.GetComponent<TankHealth>();
//targetHealth.TakeDamage(50f);