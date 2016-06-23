using UnityEngine;

public class Health : MonoBehaviour
{

    public int hp;

    void Start()
    {
        hp = 100;
    }
    public void Damage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
