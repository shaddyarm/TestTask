// �����, ���������� �� ���������� ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SphereCollider _sphereCollider; // ������ �� ��������� SphereCollider
    private Transform _spherePosition; // ������ �� ��������� Transform
    [SerializeField] float _bulletSpeed; // �������� ����

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>(); // �������� ��������� SphereCollider
        _spherePosition = GetComponent<Transform>(); // �������� ��������� Transform
    }

    private void FixedUpdate()
    {
        _spherePosition.Translate(new Vector3(0, 0, _bulletSpeed)); // ���������� ���� ������
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obsticle") // ���� ���� ����������� � ������������
            Destroy(this.gameObject); // ���������� ����
        else if (other.gameObject.tag == "Enemy") // ���� ���� ����������� � ������
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("GetHit"); // ������������� �������� ��������� ����� � �����
            Destroy(this.gameObject); // ���������� ����
            other.gameObject.GetComponent<Enemy>().GetHit(); // �������� ����� ��������� ����� � �����
        }
    }
}