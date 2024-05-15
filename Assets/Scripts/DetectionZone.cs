using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noColiderRemain;
    public List<Collider2D> detectedCollider = new List<Collider2D>();
    Collider2D col;

    public void Awake()
    {
        col= GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCollider.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCollider.Remove(collision);

        if(detectedCollider.Count <= 0)
        {
            noColiderRemain.Invoke();
        }
    }
}
