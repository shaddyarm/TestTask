using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������������ ��������� ��� ������ ������
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera; // ��������� ������ ��� ���������� �������� ������
    [SerializeField] private MovementCharacteristics _characteristics; // �������������� �������� ������
    [SerializeField] private Transform bulletSpawner; // �����, ������ ����� �������� ����
    [SerializeField] private GameObject bullet; // ������ ����
    [SerializeField] private GameObject skill; // ������ ������ 
    [SerializeField] private Transform skillspawner; // �����, ������ ����� ����������� �����
    private float _vertical, _horizontal; // �������� ��� ������������� � ��������������� �����
    private const float _distanceOffsetCamera = 5f; // ���������� �� ������, ��������������� �� ������
    private CharacterController _controller; // ������ �� ��������� CharacterController
    private Animator _animator; // ������ �� ��������� Animator
    private Vector3 _direction; // ����������� �������� ������
    private Quaternion _look; // ����������� ������� ������
    // ����������� ���������� ��� ����������� ����� �������� ������
    private Vector3 TargetRotate => _camera.forward * _distanceOffsetCamera;
    private bool Idle => _horizontal == 0.0f && _vertical == 0.0f; // ��������, ��������� �� ����� � ��������� �����
    private bool isAttacking = false; // ���������� ��� ��������, ������� �� ����� � ������ ������
    private bool skillOnCooldown = false; // ���������� ��� �����������, ��������� �� ����� � ������ ��������������

    // ������������� ����������� ��� �������
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // ���������� �� ������ ����� ��� ���������� ������
    private void FixedUpdate()
    {
        Movement(); // ����� ������ ��� ���������� ��������� ������
        Rotate(); // ����� ������ ��� ���������� ��������� ������
        StartCoroutine(Attack()); // ����� ������ ��� ���������� �����
        StartCoroutine(UseSkill()); // ����� ������ ��� ������������� ������
    }

    // ����� ��� ���������� ��������� ������
    private void Movement()
    {
        // ��������, ��������� �� ����� �� �����
        if (_controller.isGrounded)
        {
            _horizontal = Input.GetAxis("Horizontal"); // ��������� ����� ��� ��������������� ��������
            _vertical = Input.GetAxis("Vertical"); // ��������� ����� ��� ������������� ��������

            _direction = transform.TransformDirection(_horizontal, 0, _vertical); // ����������� ����������� �������� ������

            PlayAnimation(); // ����� ������ ��� ���������� ��������� ������
            Jump(); // ����� ������ ��� ���������� ������
        }
        _direction.y -= _characteristics.Gravity * Time.fixedDeltaTime; // ���������� ���������� � �������� ������

        Vector3 dir = _direction * _characteristics.MovementSpeed * Time.fixedDeltaTime; // ������� ����������� � �������� �������� ������
        _controller.Move(dir); // ���������� �������� � ������
    }

    // ����� ��� ���������� ��������� ������
    private void Rotate()
    {
        if (Idle) return; // ���� ����� � ��������� �����, �� ����� �� ������
        Vector3 target = TargetRotate; // ����������� �������� ����� �������� ������
        target.y = 0;

        _look = Quaternion.LookRotation(target); // ����������� ����������� ������� ������

        float speed = _characteristics.AngularSpeed * Time.fixedDeltaTime; // ����������� �������� �������� ������
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _look, speed); // ���������� �������� � ������
    }

    // ����� ��� ���������� ��������� ������
    private void PlayAnimation()
    {
        _animator.SetFloat("Horizontal", _horizontal);
        _animator.SetFloat("Vertical", _vertical);
    }

    // ����� ��� ���������� ������
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
            _direction.y += 2f;
        }
    }

    // ����� ��� ���������� �����
    private IEnumerator Attack()
    {
        if (isAttacking)
            yield break; // ���� ����� ��� �������, �� ����� �� ������
        else if (Input.GetMouseButton(0) && !isAttacking)
        {
            isAttacking = true; // ��������� ����� �����
            Instantiate(bullet, bulletSpawner); // �������� ����
            _animator.SetTrigger("Attack"); // ����� �������� �����
            yield return new WaitForSeconds(1f); // ��������� �������� ����� �������
            isAttacking = false; // ����� ����� �����
        }
    }

    // ����� ��� ������������� ������
    private IEnumerator UseSkill()
    {
        if (skillOnCooldown)
            yield break; // ���� ����� ��������� � ������ ��������������, �� ����� �� ������
        else if (Input.GetKey(KeyCode.F))
        {
            skillOnCooldown = true; // ��������� ����� �������������� ������
            Instantiate(skill, skillspawner); // ����� ������������� ������
            _animator.SetTrigger("Skill"); // ����� �������� ������������� ������
            yield return new WaitForSeconds(8f); // ����� �������������� ������
            skillOnCooldown = false; // ����� ����� �������������� ������
        }
    }
}