using System.Collections;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

/// <summary>
/// Handle player movement by listening to inputs.
/// </summary>

public class PlayerMovementController : MonoBehaviour
{
    [Header("Jump Parameters")]
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AnimationCurve _fallCurve;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }
    }

    private void HandleJump()
    {
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        float halfJumpDuration = _jumpDuration / 2f;
        float jumpTimer = 0f;

        // Jump logic
        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;
            float normalizedTime = jumpTimer / halfJumpDuration;

            //float targetHeight = Mathf.Lerp(0, _jumpHeight, normalizedTime);

            var targetHeight = _jumpCurve.Evaluate(normalizedTime) * _jumpHeight;

            var taegetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
            transform.position = taegetPosition;

            // Wait for the next frame
            yield return null;
        }

        // Fall logic
        jumpTimer = 0f;

        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(jumpTimer / halfJumpDuration);

            //float targetHeight = Mathf.Lerp(_jumpHeight, 0, normalizedTime);

            var targetHeight = _fallCurve.Evaluate(normalizedTime) * _jumpHeight; 

            var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
            transform.position = targetPosition;

            yield return null;
        }
    }
}
