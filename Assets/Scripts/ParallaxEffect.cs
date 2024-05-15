using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Starting position for the parallax object
    Vector2 startingPosition;

    // Starting Z value of the parallax game object
    float startingZ;

    //Distance that the camera has moved from the startinf position of the parallax object
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zdistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zdistanceFromTarget > 0 ? cam.farClipPlane : -cam.nearClipPlane));

    // The futher the object from the player ,the faster the ParallaxEffect object will move. Drag it's Z Value closer to the target to make it mice slower
    float parallaxFactor => Mathf.Abs(zdistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // When target move , move the parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // The position changes based on target travel speed times the parallax factor, but Z stay consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
