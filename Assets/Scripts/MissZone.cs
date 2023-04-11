using UnityEngine;

public class MissZone : MonoBehaviour
{
    public AudioClip missClip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            FindObjectOfType<GameManager>().Miss();
            FindObjectOfType<GameManager>().UpdateLives(0);
            AudioSource.PlayClipAtPoint(missClip, transform.position);
        }
    }
}
