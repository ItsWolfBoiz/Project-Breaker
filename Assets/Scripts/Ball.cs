using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 10f;
    public bool gameOver;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    private void Update()
    {
        if (gameOver)
        {
            SetRandomTrajectory();
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
