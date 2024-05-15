using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{

    public int healthRestore = 20;
    [SerializeField] private float maxY = 0.5f; 
    [SerializeField] private float minY = 0.0f;

    AudioSource pickupSource;

    private bool movingUp = true; 
    private float startY;

    private void Awake()
    {
        pickupSource= GetComponent<AudioSource>();
    }
    void Start()
    {
        startY = transform.localPosition.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable)
        {
            bool wasHealth = damageable.Heal(healthRestore);

            if (wasHealth)
                if (pickupSource)
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);

                Destroy(gameObject);
        }
    }
    private void Update()
    {
        
        float stepFloat = movingUp ? 0.5f : -0.5f;
        float newY = startY + stepFloat * maxY;
        transform.localPosition += transform.up * stepFloat * Time.deltaTime;


        if (transform.localPosition.y >= startY + maxY)
        {
            movingUp = false;
        }
        else if (transform.localPosition.y <= startY - minY)
        {
            movingUp = true;
        }
    }
}
