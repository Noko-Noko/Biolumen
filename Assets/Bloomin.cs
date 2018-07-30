using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bloomin : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;
    new Light light;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    [SerializeField] float scaleModifier = 0.0025F;
    [SerializeField] float upforce = 12f;
    [SerializeField] float sideforce = 8f;
    [SerializeField] float hp = 3f;

    [SerializeField] AudioClip inflate;
    [SerializeField] AudioClip swim;
    [SerializeField] AudioClip start;
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip death;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        light = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
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
            //if (!audioSource.isPlaying)
            //{
            //    audioSource.PlayOneShot(swim);
            //}
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
            playMoveSound();
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(Vector3.right * sideforce);
            playMoveSound();
        }
    }

    private void playMoveSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(swim);
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
                audioSource.PlayOneShot(win);
                //loadNextScene(); --> We Invoke it instead as a corutine
                Invoke( "LoadNextScene", 1f);
                break;
            default:
                if (hp > 0f)
                {
                    if (hp ==2f)
                    {
                        light.color = new Color32(151, 82, 233, 1);
                    } else if(hp ==1f)
                    {
                        light.color = new Color32(224, 29, 77, 1);
                    }
                    hp -= 1f;
                    audioSource.PlayOneShot(crash);
                }
                else
                {
                    print("Dead!");
                    audioSource.PlayOneShot(death);
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
