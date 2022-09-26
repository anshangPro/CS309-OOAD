using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private string unitName;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float defense;

    public GameObject onBlock;

    protected virtual void MoveToBlock(GameObject block)
    {
        Vector3 newPos = block.transform.position;
        newPos.x -= block.GetComponent<Collider>().bounds.size.x / 2;
        newPos.y += block.GetComponent<Collider>().bounds.size.y;
        newPos.z += block.GetComponent<Collider>().bounds.size.z / 2;
        GetComponent<Transform>().position = newPos;
    }

    protected virtual void Attack(Unit target)
    {
        float realDamage = damage - target.defense;

        if (realDamage <= 0)
            return;
        
        target.TakeDamage(this, realDamage);
    }

    protected virtual void TakeDamage(Unit from, float realDamage)
    {
        if (health <= realDamage)
            health = 0;
        else
            health -= realDamage;
    }

    protected virtual void Start()
    {
        health = maxHealth;
    }

    protected virtual void Update()
    {
        MoveToBlock(onBlock);
        gameObject.GetComponent<SpriteRenderer>().transform.LookAt(Camera.main.transform);
    }
}
