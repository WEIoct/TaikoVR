using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;

    private bool isFlyingAway = false;

    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    void Update()
    {
        if (isFlyingAway) return; // Skip normal update if flying away

        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                Vector3.right * SongManager.Instance.noteSpawnX,
                Vector3.right * SongManager.Instance.noteDespawnX,
                t);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void FlyAway()
    {
        if (!isFlyingAway)
        {
            isFlyingAway = true;
            StartCoroutine(FlyAwayEffect());
        }
    }

    private IEnumerator FlyAwayEffect()
    {
        float duration = 0.6f;
        float elapsed = 0f;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(Random.Range(-1f, 1f), 2f, 0f); // Fly up and to the side

        float spinSpeed = 60f; // less spinning than before

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Arc using a parabola: y = 4t(1-t) for smooth up-down motion
            Vector3 curvedPos = Vector3.Lerp(startPos, endPos, t);
            curvedPos.y += 0.5f * Mathf.Sin(t * Mathf.PI); // slight arc

            transform.position = curvedPos;
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime); // slow spin

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
