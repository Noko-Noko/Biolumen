using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] public class Oscilator : MonoBehaviour {

    [SerializeField] Vector3 movementFactor;
    [Range(0, 1)] [SerializeField] float movementVector;

    Vector3 startPosition;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = startPosition + movementVector * movementFactor;

    }
}
