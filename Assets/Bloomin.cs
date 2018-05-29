using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloomin : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float scaleModifier = 0.0025F;
    [SerializeField] float upforce = 12f;
    [SerializeField] float sideforce = 8f;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        up();
        move();
        deflate();
	}

    private void up()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(Vector3.up * upforce);
            transform.localScale += new Vector3(scaleModifier, scaleModifier, scaleModifier);
            audioSource.Play();
        }
    }

    private void deflate()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl))
        {
            rigidBody.AddForce(Vector3.down * upforce);
            rigidBody.AddForce(Vector3.left * sideforce);
            if (transform.localScale.sqrMagnitude > 2 * 0.5)
            {
                transform.localScale -= new Vector3(scaleModifier * 5, scaleModifier * 5, scaleModifier * 5);
            }
            //audioSource.Play();
        }
    }

    private void move()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(Vector3.left * sideforce);
            if(transform.localScale.sqrMagnitude > 2 * 0.5)
            {
                transform.localScale -= new Vector3(scaleModifier, scaleModifier, scaleModifier);
            }

        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(Vector3.right * sideforce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "Friendly":
                print("Freindly Collission detected");
                break;
            default:
                print("Dead!");
                break;
        }
    }
}
