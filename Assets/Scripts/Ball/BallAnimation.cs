using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour, IResetable
{
    [SerializeField] private Animator animator;

    [SerializeField] private new Renderer renderer;

    [Range(0.1f, 5f)]
    [SerializeField] private float changeColorDuration;

    private MaterialPropertyBlock materialPropertyBlock;

    private Dictionary<BallColor, float> colorDictionary = new Dictionary<BallColor, float>()
    {
        {BallColor.Red, -3f },
        {BallColor.Blue, 3f }
    };

    private void Awake()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(materialPropertyBlock);
        ResetState();
    }

    private void Start()
    {
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    public void SpawnBallAnimation()
    {
        animator.SetTrigger(Constants.BallSpawn_AnimationTrigger);
    }

    public void BounceAnimation()
    {
        animator.SetTrigger(Constants.BlockCollision_AnimationTrigger);
    }

    public void ChangeColor(BallColor startBallColor, BallColor targetBallColor)
    {
        StartCoroutine(changeColorCoroutine(startBallColor, targetBallColor));
    }

    private IEnumerator changeColorCoroutine(BallColor startBallColor, BallColor targetBallColor)
    {
        float elapsedTime = 0f;

        float t;

        float startOrigin = colorDictionary[startBallColor];
        float targetOrigin= colorDictionary[targetBallColor];

        while (elapsedTime <= changeColorDuration)
        {
            t = Mathf.Lerp(startOrigin, targetOrigin, elapsedTime / changeColorDuration);

            materialPropertyBlock.SetFloat(Constants.BallColorGradientOrigin_ShaderProperties, t);

            renderer.SetPropertyBlock(materialPropertyBlock);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void ResetState()
    {
        materialPropertyBlock.SetFloat(Constants.BallColorGradientOrigin_ShaderProperties, colorDictionary[BallColor.Red]);

        renderer.SetPropertyBlock(materialPropertyBlock);
    }
}
