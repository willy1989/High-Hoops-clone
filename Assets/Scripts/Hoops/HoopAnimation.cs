using System.Collections;
using UnityEngine;

public class HoopAnimation : MonoBehaviour
{
    [SerializeField] private Animator hoopAnimator;
    [SerializeField] private Animator hoopAutoLetterAnimator;

    [SerializeField] private new Renderer renderer;

    [Range(0.1f, 5f)]
    [SerializeField] private float disolveDuration;

    [SerializeField] private AnimationCurve animationCurve;

    private MaterialPropertyBlock materialPropertyBlock;

    private WaitForSeconds animationWait;

    private void Awake()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(materialPropertyBlock);
        animationWait = new WaitForSeconds(0.3f);
    }

    public void DisolveHoopModel()
    {
        StartCoroutine(DisolveCoroutine());
    }

    private IEnumerator DisolveCoroutine()
    {
        float elapsedTime = 0f;

        float t;

        // We wait for the shake animation to end
        yield return animationWait;

        while(elapsedTime <= 1f)
        {
            t = animationCurve.Evaluate(elapsedTime / disolveDuration);

            materialPropertyBlock.SetFloat(Constants.HoopAlphaClip_ShaderProperties, t);

            renderer.SetPropertyBlock(materialPropertyBlock);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        materialPropertyBlock.SetFloat(Constants.HoopAlphaClip_ShaderProperties, 1f);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }

    public void SpinHoopAnimation()
    {
        hoopAnimator.SetTrigger(Constants.HoopSideCollision_AnimationTrigger);
    }

    public void ShakeHoopAnimation()
    {
        hoopAnimator.SetTrigger(Constants.HoopCenterCollision_AnimationTrigger);
    }

    public void RaiseAutoLetter()
    {
        hoopAutoLetterAnimator.SetTrigger(Constants.BallCollision_AnimationTrigger);
    }
}
