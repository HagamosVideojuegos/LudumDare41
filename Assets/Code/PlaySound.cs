using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    public void Play(AudioClip audioClip)
    {
        source.clip = audioClip;
        source.pitch = Random.Range(.8f, 1.2f);
        source.panStereo = Mathf.Lerp(0, 1, Camera.main.WorldToViewportPoint(transform.position).x) * 2 - 1;
        source.Play();
    }

}
