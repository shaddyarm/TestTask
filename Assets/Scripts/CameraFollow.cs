// ��� �������� �� �������� ������ �� �������� ��������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] private float _angularSpeed = 1f;  // �������� �������� ������
    [SerializeField] private Transform _target;  // ������, �� ������� ����� ������� ������
    private float _angleY;  // ���� �������� ������ �� Y-���

    void Start()
    {
        _angleY = transform.rotation.y;  // ���������� ������� ���� ��������
    }

    void Update()
    {
        // ��������� ������� ������ Z � X
        if (Input.GetKey(KeyCode.Z))
            _angleY -= _angularSpeed;  // ��������� ���� �������� �� Y-���
        if (Input.GetKey(KeyCode.X))
            _angleY += _angularSpeed;  // ����������� ���� �������� �� Y-���

        // ������������� ������� ������ ������ ������� ����
        transform.position = _target.transform.position;
        // ������������� ���� �������� ������ �� ��� Y
        transform.rotation = Quaternion.Euler(0, _angleY, 0);
    }
}