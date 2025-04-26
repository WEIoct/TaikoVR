using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitPlace : MonoBehaviour
{
    public KeyCode don;
    public KeyCode kat;

    public Color donColor;
    public Color katColor;
    public Color missColor;

    private SpriteRenderer spriteRenderer;
    private Coroutine colorCoroutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(don))
        {
            print("Don");
            StartColorChange(donColor);
        }
        else if (Input.GetKeyDown(kat))
        {
            print("Kat");
            StartColorChange(katColor);
        }
        else if (Input.anyKeyDown)
        {
            print("Miss");
            StartColorChange(missColor);
        }
    }

    void StartColorChange(Color targetColor)
    {
        if (colorCoroutine != null)
        {
            StopCoroutine(colorCoroutine);
        }
        colorCoroutine = StartCoroutine(ColorChangeRoutine(targetColor));
    }

    IEnumerator ColorChangeRoutine(Color targetColor)
    {
        spriteRenderer.color = targetColor;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }
}
