using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static Util.PositionUtil;

public class Unit : MonoBehaviour
{
    [SerializeField] private string unitName;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float defense;

    private float _delta = 0.00001f;

    public float speed = 1.0f;

    // private Vector3 _lookAt;

    public GameObject onBlock;

    protected virtual void MoveToBlock(GameObject block)
    {
        Vector3 newPos = DstBlock2DstPos3(block);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, newPos, step);
    }

    protected virtual void MoveAlongPath(List<Block> path)
    {
        if (path.Count <= 0)
        {
            return;
        }

        MoveToBlock(path[0].gameObject);

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),DstBlock2DstPos2(path[0].gameObject) ) < _delta)
        {
            PositionOnBlock(path[0].gameObject);
            path.RemoveAt(0);
        }
    }

    protected virtual void PositionOnBlock(GameObject block)
    {
        transform.position = DstBlock2DstPos3(block);
        onBlock = block;
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
        // MoveToBlock(onBlock);
        // _lookAt.x = Camera.main.transform.position.x;
        // _lookAt.y = transform.position.y;
        // _lookAt.z = Camera.main.transform.position.z;
        // gameObject.GetComponent<SpriteRenderer>().transform.LookAt(_lookAt);
        gameObject.GetComponent<SpriteRenderer>().transform.LookAt(Camera.main.transform);
    }
}
