using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWallAnimation : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;

    [Range(0.1f, 5f)]
    [SerializeField] private float disolveDuration;

    [SerializeField] private AnimationCurve animationCurve;

    private MaterialPropertyBlock materialPropertyBlock;

    private void Awake()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(materialPropertyBlock);

        materialPropertyBlock.SetFloat("AlphaClip_", 0f);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }

    public void DisolveColorWall()
    {
        StartCoroutine(DisolveCoroutine());
    }

    private IEnumerator DisolveCoroutine()
    {
        float elapsedTime = 0f;

        float t;

        while (elapsedTime <= 1f)
        {
            t = animationCurve.Evaluate(elapsedTime / disolveDuration);

            materialPropertyBlock.SetFloat("AlphaClip_", t);

            renderer.SetPropertyBlock(materialPropertyBlock);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        materialPropertyBlock.SetFloat("AlphaClip_", 1.1f);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }
}
