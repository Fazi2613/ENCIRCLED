using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public AudioClip audioClip;
    public float minDelay = 1f;
    public float maxDelay = 4f;
    public float interval = 5f;
    private float timer = 0f;

    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            Invoke("PlayAudio", Random.Range(minDelay, maxDelay));
            timer = 0f;
        }
    }

    void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}
