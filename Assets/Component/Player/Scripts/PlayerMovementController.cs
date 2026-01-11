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

    [Header("Slide Parameter")]
    [SerializeField] private float _slideDuration = 0.5f;
    [SerializeField] private float _slideDownDuration = 0.6f;
    [SerializeField] private Transform[] _slideTargets;

    [Header("Component")]
    [SerializeField] private Animator _animator;
    [SerializeField] PlayerCollisionController collisionController;

    [Header("Debug")]
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _isSliding;
    [SerializeField] private bool _isSlidingDown;
    [SerializeField] private int _currentLaneIndex =1;

    private const string Jump_Parameter = "IsJumping";
    private const string Slide_Down_Parameter = "IsSlidingDown";
    private const string Grounded_Parameter = "Grounded";

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(_isSliding)
            {
                return;
            }
            
            if(_currentLaneIndex == 0)
            {
                return;
            }
            
            _currentLaneIndex--;

            StartCoroutine(SlideCoroutine(_slideTargets[_currentLaneIndex]));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_isSliding)
            {
                return;
            }

            if (_currentLaneIndex == 2)
            {
                return;
            }

            _currentLaneIndex++;

            StartCoroutine(SlideCoroutine(_slideTargets[_currentLaneIndex]));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_isSlidingDown || _isJumping)
            {
                return;
            }

            StartCoroutine(SlideDownCoroutine());
        }
    }

    private void HandleJump()
    {
        if(_isJumping)
        {
            return;
        }

        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        _isJumping = true;

        _animator.SetBool(Jump_Parameter, true);

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

        _animator.SetBool(Jump_Parameter, false);

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

        _animator.SetTrigger(Grounded_Parameter);

        _isJumping = false;
    }

    private IEnumerator SlideCoroutine(Transform target)
    {
        _isSliding = true;
        var slideTimer = 0f;

        while(slideTimer < _slideDuration)
        {
            slideTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(slideTimer / _slideDuration);
            var targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, normalizedTime);

            // Wait for the next frame
            yield return null;
        }
        _isSliding = false;
    }

    private IEnumerator SlideDownCoroutine()
    {
        var slideTimer = 0f;

        _isSlidingDown = true;
        _animator.SetBool(Slide_Down_Parameter, true);
        collisionController.ShrinkCollider(true);

        while(slideTimer < _slideDownDuration)
        {
            slideTimer += Time.deltaTime;
            yield return null;
        }

        collisionController.ShrinkCollider(false);
        _animator.SetBool(Slide_Down_Parameter, false);
        _isSlidingDown = false;
    }
}
