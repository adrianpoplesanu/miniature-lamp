using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Randomly adjusts the alpha of a UI Graphic (Text, Image, etc.) to create a soft flicker effect.
/// Attach it to the GameObject that holds the text component or point it to another Graphic via the inspector.
/// </summary>
public class TextFlicker : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Graphic targetGraphic;

    [Header("Alpha Range")]
    [SerializeField, Range(0f, 1f)] private float minAlpha = 0.35f;
    [SerializeField, Range(0f, 1f)] private float maxAlpha = 1f;

    [Header("Timing (seconds)")]
    [SerializeField, Min(0.05f)] private float minInterval = 0.1f;
    [SerializeField, Min(0.05f)] private float maxInterval = 0.4f;

    [Header("Behaviour")]
    [SerializeField] private bool useUnscaledTime = true;

    private Coroutine flickerRoutine;
    private float originalAlpha = 1f;

    private void Awake()
    {
        if (!targetGraphic)
            targetGraphic = GetComponent<Graphic>();

        if (targetGraphic)
            originalAlpha = targetGraphic.color.a;
    }

    private void OnEnable()
    {
        if (targetGraphic == null)
            return;

        StartFlicker();
    }

    private void OnDisable()
    {
        StopFlicker();
        ResetAlpha();
    }

    public void StartFlicker()
    {
        if (flickerRoutine != null || targetGraphic == null)
            return;

        flickerRoutine = StartCoroutine(FlickerRoutine());
    }

    public void StopFlicker()
    {
        if (flickerRoutine == null)
            return;

        StopCoroutine(flickerRoutine);
        flickerRoutine = null;
    }

    private IEnumerator FlickerRoutine()
    {
        while (enabled && targetGraphic != null)
        {
            float duration = Random.Range(minInterval, maxInterval);
            float targetAlpha = Random.Range(minAlpha, maxAlpha);

            float elapsed = 0f;
            float startingAlpha = targetGraphic.color.a;

            while (elapsed < duration)
            {
                elapsed += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                float smoothed = Mathf.SmoothStep(0f, 1f, t);
                SetAlpha(Mathf.Lerp(startingAlpha, targetAlpha, smoothed));
                yield return null;
            }
        }

        flickerRoutine = null;
    }

    private void SetAlpha(float alpha)
    {
        if (targetGraphic == null)
            return;

        Color color = targetGraphic.color;
        color.a = Mathf.Clamp01(alpha);
        targetGraphic.color = color;
    }

    private void ResetAlpha()
    {
        SetAlpha(originalAlpha);
    }

    private void OnValidate()
    {
        if (maxAlpha < minAlpha)
            maxAlpha = minAlpha;
    }
}


