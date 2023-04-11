using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 10f;
    private GameManager gameManager;
    public AudioClip wallHitClip;
    public AudioClip brickHitClip;

    private AudioSource wallHitAudioSource;
    private AudioSource brickHitAudioSource;


    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
        wallHitAudioSource = gameObject.AddComponent<AudioSource>();
        wallHitAudioSource.clip = wallHitClip;

        brickHitAudioSource = gameObject.AddComponent<AudioSource>();
        brickHitAudioSource.clip = brickHitClip;
    }

    private void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallHitAudioSource.volume = 0.5f;
            wallHitAudioSource.pitch = 0.8f;
            wallHitAudioSource.PlayDelayed(-0.5f);
            wallHitAudioSource.Play();
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            brickHitAudioSource.volume = .8f;
            brickHitAudioSource.Play();
        }
    }

    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigidbody.velocity = Vector2.zero;
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = new Vector2();
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.speed);
    }

    //Ball slowing down fix
    private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity.normalized * speed;   
    }

}
