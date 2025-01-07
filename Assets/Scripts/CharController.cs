using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform arrowspawnpoint;
    public Transform swordHitPoint;
    public GameObject arrowPrefab;
    public GameObject slashEffect;
    public GameObject swordSlash;
    public float bulletSpeed = 10; //for arrow 


    private int comboStep = 0;
    private float comboTimer = 0f;
    public float comboResetTime = 1f; // combo counter variables

    [SerializeField] private AudioClip swordSound; // Kýlýç sesi için deðiþken
    private AudioSource audioSource; // AudioSource referansý


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
        audioSource = GetComponent<AudioSource>();

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

        if (comboStep > 0)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboResetTime)
            {
                ResetCombo();
            }
        }

        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            ResetCombo();
        }



    }

    private void FixedUpdate()
    {

        if (!_isDashing && !_isAttacking)
        {
            Move();
        }

        // Dash
        if (Input.GetMouseButtonDown(1) && _canDash)
        {
            animator.SetBool("IsDashing", true);
            StartCoroutine(Dash());
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

        // Collider'ý devre dýþý býrak
        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

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

        // Collider'ý tekrar etkinleþtir
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
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
            SwordAttack();
        }

        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.8f); // Saldýrý animasyonunun süresi kadar bekle
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

    private void SwordAttack()
    {
        comboStep = (comboStep % 4) + 1;
        
        comboTimer = 0f; // Zamanlayýcýyý sýfýrla


        if (comboStep == 1)
        {
            Debug.Log("1 kere vurdu.");
            
            animator.SetBool("IsAttackingSword",true);
            animator.SetBool("Combo1", false);
            animator.SetBool("Combo2", false);
            animator.SetBool("Combo3", false);
        }
        else if (comboStep == 2)
        {
            Debug.Log("2 kere vurdu.");
            animator.SetBool("Combo1",true);
            animator.SetBool("IsAttackingSword", false);
            animator.SetBool("Combo2", false);
            animator.SetBool("Combo3", false);
        }
        else if (comboStep == 3)
        {
            Debug.Log("3 kere vurdu.");
            animator.SetBool("IsAttackingSword", false);
            animator.SetBool("Combo1", false);
            animator.SetBool("Combo3", false);
            animator.SetBool("Combo2",true);
        }
        else if (comboStep == 4)
        {

           
            
            Debug.Log("4 kere vurdu.");
            animator.SetBool("IsAttackingSword", false);
            animator.SetBool("Combo1", false);
            animator.SetBool("Combo2", false);
            animator.SetBool("Combo3",true);

           
        }
        else if(comboStep >= 4)
        {
            ResetCombo(); // Son saldýrýdan sonra sýfýrla

        }


    }

    public void SwordSlashSound()
    {
        if (swordSound != null)
        {
            audioSource.PlayOneShot(swordSound);
        }
    }

    public void HitTheGround() 
    
    {
        var slash = Instantiate(slashEffect, swordHitPoint.position, swordHitPoint.rotation);
        slash.GetComponent<Rigidbody>().velocity = swordHitPoint.forward * 1f;
        Destroy(slash, 1f);

    }

    public void SlashEffect()
    {
       
      
    }
    void ResetCombo()
    {
        comboStep = 0;
        comboTimer = 0f;
        ResetAllComboBools();
    }

    void ResetAllComboBools()
    {
        animator.SetBool("IsAttackingSword", false);
        animator.SetBool("Combo1", false);
        animator.SetBool("Combo2", false);
        animator.SetBool("Combo3", false);

    }

    
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
