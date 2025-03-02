using UnityEngine;

public class DashAnimation : MonoBehaviour
{
    public float dashDuration = 1f;
    public float squishAmount = 0.75f;
    public float stretchAmount = 1.25f;
    public float dashCooldown = 1f;

    private Vector3 _originalScale;
    private bool _isDashing = false;
    private float _dashTimer = 0f;
    private float _cooldownTimer = 0f;

    void Start()
    {
        _originalScale = transform.localScale;
    }

    void Update()
    {
        _cooldownTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !_isDashing && _cooldownTimer >= dashCooldown)
        {
            StartDash();
        }

        if (_isDashing)
        {
            DashEffect();
        }
    }

    void StartDash()
    {
        _isDashing = true;
        _dashTimer = 0f;
        _cooldownTimer = 0f;
    }

    void DashEffect()
    {
        _dashTimer += Time.deltaTime;
        float t = _dashTimer / dashDuration;

        float scaleX = Mathf.Lerp(_originalScale.x, _originalScale.x * squishAmount, Mathf.Sin(t * Mathf.PI));
        float scaleY = Mathf.Lerp(_originalScale.y, _originalScale.y * stretchAmount, Mathf.Sin(t * Mathf.PI));
        transform.localScale = new Vector3(scaleX, scaleY, _originalScale.z);

        if (_dashTimer >= dashDuration)
        {
            _isDashing = false;
            transform.localScale = _originalScale;
        }
    }
}
