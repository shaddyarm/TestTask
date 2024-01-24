// Ётот скрипт создает объект-ресурс, содержащий характеристики движени€
using UnityEngine;

// —оздаем меню в Unity, чтобы можно было создать этот ресурс через CreateAssetMenu
[CreateAssetMenu(fileName = "Characteristics", menuName = "Movement/MovementCharacteristics", order = 1)]
public class MovementCharacteristics : ScriptableObject
{
    // —ериализованна€ переменна€ дл€ скорости перемещени€
    [SerializeField] private float _movementSpeed = 1f;

    // —ериализованна€ переменна€ дл€ скорости поворота
    [SerializeField] private float _angularSpeed = 150f;

    // —ериализованна€ переменна€ дл€ гравитации
    [SerializeField] private float _gravity = 15f;

    // —войство дл€ получени€ значени€ скорости перемещени€
    public float MovementSpeed => _movementSpeed;

    // —войство дл€ получени€ значени€ скорости поворота
    public float AngularSpeed => _angularSpeed;

    // —войство дл€ получени€ значени€ гравитации
    public float Gravity => _gravity;
}