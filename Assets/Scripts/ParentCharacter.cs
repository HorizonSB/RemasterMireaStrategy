using UnityEngine;
using UnityEngine.AI;

public class ParentCharacter : MonoBehaviour
{
    public int _health;
    public int _damage;


    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Destroy(gameObject);
    }
}
