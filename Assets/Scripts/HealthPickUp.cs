using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthRestored = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    void Start() { }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthRestored);
            if (wasHealed)
            {
                if (pickupSource != null)
                {
                    AudioSource.PlayClipAtPoint(
                        pickupSource.clip,
                        gameObject.transform.position,
                        pickupSource.volume
                    );
                }
                Destroy(gameObject);
            }
        }
    }
}
