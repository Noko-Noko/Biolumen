using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloomin : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInputs();
	}

    private void ProcessInputs()
    {
        float scale = 0.0025F;

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(Vector3.up);
            transform.localScale += new Vector3(scale, scale, scale);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(Vector3.left);
            if(transform.localScale.sqrMagnitude > 2 * 0.5)
            {
                transform.localScale -= new Vector3(scale * 2, scale * 2, scale * 2);
            }

        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            print("right pressed");
            rigidBody.AddForce(Vector3.right);
        }

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    switch (Input.inputString)
        //    {
        //        case "a":
        //            Debug.Log("a pressed");
        //            break;
        //        case "d":
        //            Debug.Log("d pressed");
        //            break;
        //        case "w":
        //            Debug.Log("w pressed");
        //            break;
        //        case "Space":
        //            Debug.Log("Space pressed");
        //            break;
        //        default:
        //            Debug.Log("this is not a valid key");
        //            break;
        //    }
        //}
    }
}
