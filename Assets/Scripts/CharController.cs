using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform arrowspawnpoint;
    public GameObject arrowPrefab;
    public float bulletSpeed = 10; //for arrow 


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
    public bool _isAttacking = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GatherInput();
        Look();

        // Eðer saldýrý yapýlýyorsa hareket etme
        if (_isAttacking)
            return;

        if (_input.magnitude > 0 && !_isDashing)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        // Saldýrý
        if (Input.GetMouseButtonDown(0) && !_isDashing)
        {
            Attack();
        }

        // Dash
        if (Input.GetMouseButtonDown(1) && _canDash)
        {
            animator.SetBool("IsDashing", true);
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!_isDashing && !_isAttacking)
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
        if (_input == Vector3.zero || _isAttacking) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
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
        animator.SetBool("IsDashing", false);

        // Dash sonrasý animasyon durumunu kontrol et
        if (_input.magnitude > 0) // Eðer hareket varsa
        {
            animator.SetBool("IsWalking", true);
        }
        else // Hareket yoksa
        {
            animator.SetBool("IsWalking", false);
        }

        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

    private void Attack()
    {
        _isAttacking = true;
        _input = Vector3.zero; // Saldýrý sýrasýnda hareket giriþlerini sýfýrla

        if (bow.activeSelf)
        {
            animator.SetBool("IsAttackingBow", true);
            BowAttack();
        }

        if (sword.activeSelf)
        {
            animator.SetBool("IsAttackingSword", true);
        }

        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Saldýrý animasyonunun süresi kadar bekle
        animator.SetBool("IsAttackingBow", false);
        animator.SetBool("IsAttackingSword", false);
        _isAttacking = false;
    }

    private void BowAttack() 
    {
        var arrow = Instantiate(arrowPrefab, arrowspawnpoint.position, arrowspawnpoint.rotation);
        arrow.GetComponent<Rigidbody>().velocity = arrowspawnpoint.forward * bulletSpeed;
        Destroy(arrow, 4f);
    }


}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
