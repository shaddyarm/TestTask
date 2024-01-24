using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private GameObject skillApplied; // ������ �� ������, ������� ����� ����������� ��� ��������� ������

    void Start()
    {
        _transform = GetComponent<Transform>(); // �������� ������ �� ��������� Transform �������� �������
        StartCoroutine(ActivateSkill()); // ��������� �������� ��������� ������
    }

    IEnumerator ActivateSkill(float duration = 1f)
    {
        float time = 0; // �����, ��������� � ������ ��������� ������
        while (time < duration) // ���� �� ������� ������������ ��������� ������
        {
            time += Time.deltaTime; // ����������� ����� �� ��������� � ������� ���������� �����

            float t = time / duration; // ������� �������, ��������� �� ������ ��������� ������
            float scale = Mathf.Lerp(0, 15, t); // ������������� ����� ��������� � �������� ��������� �������� ������
            _transform.localScale = new Vector3(scale, scale, scale); // ��������� ����� ������� ������

            yield return null; // ��������� �� ��������� ����
        }

        time = 0; // ���������� ����� ���������
        while (time < duration) // ���� �� ������� ������������ ��������� ������
        {
            time += Time.deltaTime; // ����������� ����� �� ��������� � ������� ���������� �����

            float t = time / duration; // ������� �������, ��������� �� ������ ��������� ������
            float scale = Mathf.Lerp(15, 0, t); // ������������� ����� ��������� � �������� �������� �������� ������
            _transform.localScale = new Vector3(scale, scale, scale); // ��������� ����� ������� ������

            yield return null; // ��������� �� ��������� ����
        }

        Destroy(this.gameObject); // ���������� ������ ������
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") // ���� ����������� � ������
        {
            other.GetComponent<CapsuleCollider>().radius *= 2f; // ����������� ������ ���������� �����
            GameObject skillIsApplied = Instantiate(skillApplied, other.gameObject.transform); // ������� ������, ������� ����� �������� � ����� ��� ���������
            other.gameObject.GetComponent<Enemy>().ApplySkill(other.gameObject, skillIsApplied); // ��������� ����� � �����
        }
    }
}