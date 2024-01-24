// Класс Enemy отвечает за поведение врагов
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Функция вызывается, когда враг получает урон
    public void GetHit()
    {
        StartCoroutine(EnemyDeath());
    }

    // Корутина, которая уничтожает врага через 0.7 секунды
    private IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }

    // Функция, применяющая навык к игровому объекту
    public void ApplySkill(GameObject gameObject, GameObject skillIsApplied)
    {
        StartCoroutine(SkillAplied(gameObject, skillIsApplied));
    }

    // Корутина, которая удаляет навык с игрового объекта через 4 секунды
    private IEnumerator SkillAplied(GameObject gameObject, GameObject skillIsApplied)
    {
        yield return new WaitForSeconds(4f);
        Destroy(skillIsApplied);
        gameObject.GetComponent<CapsuleCollider>().radius = gameObject.GetComponent<CapsuleCollider>().radius / 2f;
    }
}