// ����� Enemy �������� �� ��������� ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // ������� ����������, ����� ���� �������� ����
    public void GetHit()
    {
        StartCoroutine(EnemyDeath());
    }

    // ��������, ������� ���������� ����� ����� 0.7 �������
    private IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }

    // �������, ����������� ����� � �������� �������
    public void ApplySkill(GameObject gameObject, GameObject skillIsApplied)
    {
        StartCoroutine(SkillAplied(gameObject, skillIsApplied));
    }

    // ��������, ������� ������� ����� � �������� ������� ����� 4 �������
    private IEnumerator SkillAplied(GameObject gameObject, GameObject skillIsApplied)
    {
        yield return new WaitForSeconds(4f);
        Destroy(skillIsApplied);
        gameObject.GetComponent<CapsuleCollider>().radius = gameObject.GetComponent<CapsuleCollider>().radius / 2f;
    }
}