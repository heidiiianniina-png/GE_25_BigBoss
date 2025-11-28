using UnityEngine;
using System.Collections;

public class MusicZone : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioSource zoneMusic;
    public float fadeTime = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(FadeMusic(mainMusic, zoneMusic));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(FadeMusic(zoneMusic, mainMusic));
    }

    IEnumerator FadeMusic(AudioSource fadeOut, AudioSource fadeIn)
    {
        float t = 0f;

        fadeIn.Play();

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float lerp = t / fadeTime;

            fadeOut.volume = Mathf.Lerp(1f, 0f, lerp);
            fadeIn.volume = Mathf.Lerp(0f, 1f, lerp);

            yield return null;
        }

        fadeOut.Stop();
    }
}
