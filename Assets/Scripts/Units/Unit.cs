using System.Collections.Generic;
using UnityEngine;

using static Util.PositionUtil;
using Interfaces;

public class Unit : MonoBehaviour, IClickable
{
    [SerializeField] private string unitName;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float defense;
    
    [SerializeField] public int agility;

    private const float Delta = 0.00001f;
    private bool hasMoved;
    private bool hasAttacked;

    public GameObject onBlock;

    protected virtual void MoveToBlock(GameObject block)
    {
        Vector3 newPos = DstBlock2DstPos3(block);

        const float speed = 5.0f;
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }

    protected virtual void MoveAlongPath(List<Block> path)
    {
        hasMoved = true;
        if (path.Count <= 0)
        {
            return;
        }

        MoveToBlock(path[0].gameObject);

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),DstBlock2DstPos2(path[0].gameObject) ) < Delta)
        {
            SetOnBlock(path[0].gameObject);
            path.RemoveAt(0);
        }
    }

    protected virtual void SetOnBlock(GameObject block)
    {
        transform.position = DstBlock2DstPos3(block);
        onBlock = block;
    }

    public virtual void Attack(Unit target)
    {
        hasAttacked = true;
        float realDamage = damage - target.defense;

        if (realDamage <= 0)
            return;
        
        target.TakeDamage(this, realDamage);
    }

    public virtual void TakeDamage(Unit from, float realDamage)
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
        gameObject.GetComponent<SpriteRenderer>().transform.LookAt(Camera.main.transform);
    }

    public bool IsClicked()
    {
        return !hasMoved || !hasAttacked;
    }

    public void OnTurnBegin()
    {
        hasMoved = false;
        hasAttacked = false;
    }
}
