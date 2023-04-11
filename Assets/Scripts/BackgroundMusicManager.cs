using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    static GameObject bgMusic;

    void Start()
    {
        if (bgMusic == null)
        {
            bgMusic = gameObject;
            DontDestroyOnLoad(bgMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
