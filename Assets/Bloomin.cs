using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bloomin : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;
    Light light;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    [SerializeField] float scaleModifier = 0.0025F;
    [SerializeField] float upforce = 12f;
    [SerializeField] float sideforce = 8f;
    [SerializeField] float hp = 3f;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        light = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        if(state == State.Alive)
        {
            up();
            move();
            deflate();
        }
        
	}

    private void up()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(Vector3.up * upforce);
            transform.localScale += new Vector3(scaleModifier, scaleModifier, scaleModifier);
            light.intensity += 0.01f;
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
                light.intensity -= 0.1f;
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
        if (state != State.Alive) return;

        switch (collision.gameObject.tag) {
            case "Friendly":
                print("Freindly Collission detected");
                break;
            case "Goal":
                print("Goal reached");
                state = State.Transcending;
                //loadNextScene(); --> We Invoke it instead as a corutine
                Invoke( "LoadNextScene", 1f);
                break;
            default:
                if (hp > 0f)
                {
                    hp -= 1f;
                    if(hp ==2f)
                    {
                        light.color = new Color32(151, 82, 233, 1);
                    } else if(hp ==1f)
                    {
                        light.color = new Color32(224, 29, 77, 1);
                    }
                }
                else
                {
                    print("Dead!");
                    state = State.Dying;
                    Invoke("Retry", 1f);
                }
                break;
        }
    }

    private void LoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex+1);
    }

    private void Retry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

}
