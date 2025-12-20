using UnityEngine;
using UnityEngine.UI;

public class TextGlitchDriver : MonoBehaviour
{
    [SerializeField] Graphic targetGraphic;
    [SerializeField] Material glitchMaterial;
    [SerializeField] AnimationCurve pulse = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] float interval = 2f;
	[SerializeField] bool constantOn = false;

    Material runtimeMat;
    float timer;

    void Awake()
    {
        if (targetGraphic == null)
            targetGraphic = GetComponent<Graphic>();

        if (glitchMaterial != null)
        {
            runtimeMat = new Material(glitchMaterial);
            targetGraphic.material = runtimeMat;
        }
        else if (targetGraphic.material != null)
        {
            runtimeMat = Instantiate(targetGraphic.material);
            targetGraphic.material = runtimeMat;
        }
    }

    void OnEnable()
    {
        if (runtimeMat != null && targetGraphic != null)
            targetGraphic.material = runtimeMat;
    }

    void Update()
    {
        if (runtimeMat == null) return;

        timer += Time.deltaTime;
		float t = Mathf.Repeat(timer, interval) / Mathf.Max(0.0001f, interval);
		float strength = constantOn ? 1f : pulse.Evaluate(t);
		runtimeMat.SetFloat("_GlitchStrength", strength);

		// Provide local rect bounds so shader can compute stripes across the whole text object
		var rt = targetGraphic.transform as RectTransform;
		if (rt != null)
		{
			// In local space, vertices use rect coordinates (xMin..xMax)
			var rect = rt.rect;
            //Debug.Log($"Rect: {rect.xMin}, {rect.xMax}, {rect.yMin}, {rect.yMax}");
			runtimeMat.SetFloat("_RectLocalXMin", rect.xMin);
			runtimeMat.SetFloat("_RectLocalWidth", Mathf.Max(0.0001f, rect.width));
			runtimeMat.SetFloat("_RectLocalYMin", rect.yMin);
			runtimeMat.SetFloat("_RectLocalHeight", Mathf.Max(0.0001f, rect.height));
		}
    }

    void OnDestroy()
    {
        if (runtimeMat != null)
            Destroy(runtimeMat);
    }
}