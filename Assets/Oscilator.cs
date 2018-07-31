using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] public class Oscilator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(2f, 2f, 2f);
    [SerializeField] float speed = 2f;

    Vector3 startPosition;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (speed < 0f) return;
        float cycles = Time.time / speed;
        const float tau = Mathf.PI * 2f; // get a tau, 2 times Pi
        float rawSinWave = Mathf.Sin(cycles * tau); // Return a loop of -1 to 1

        float movementFactor = rawSinWave / 2f + .5f; // to get a variant from 0 to 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;

    }
}
