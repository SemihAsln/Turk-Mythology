using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private float _dashSpeed = 15;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1.0f;
    [SerializeField] private GameObject bow; // Ýlk obje
    [SerializeField] private GameObject sword; // Ýkinci obje

    private Vector3 _input;
    private bool _isDashing = false;
    private bool _canDash = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GatherInput();
        Look();

        // Sadece hareketsizken saldýrý
        if (_input.magnitude == 0 && !_isDashing && Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(1) && _canDash)
        {
            animator.SetBool("IsDashing", true);
            StartCoroutine(Dash());
        }

       

        // Hareket animasyonu
        if (_input.magnitude > 0 && !_isDashing) // Eðer hareket varsa
        {
            animator.SetBool("IsWalking", true);
        }
        else // Hareket yoksa
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void FixedUpdate()
    {
        if (!_isDashing)
        {
            Move();
        }
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsDashing", false);
        _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        _canDash = false;

        Vector3 dashDirection = transform.forward;
        float dashEndTime = Time.time + _dashDuration;

        while (Time.time < dashEndTime)
        {
            _rb.MovePosition(transform.position + dashDirection * _dashSpeed * Time.deltaTime);
            yield return null;
        }

        _isDashing = false;

        // Cooldown before next dash
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

    private void Attack()
    {
        if (bow.activeSelf)
        {
            animator.SetBool("IsAttackingBow", true);
        }

        if (sword.activeSelf)
        {
            animator.SetBool("IsAttackingSword", true);
        }

        // Saldýrýyý bitir ve animasyonu sýfýrla
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.1f); // Saldýrý animasyonunun süresine göre ayarlanabilir
        animator.SetBool("IsAttackingBow", false);
        animator.SetBool("IsAttackingSword", false);
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
