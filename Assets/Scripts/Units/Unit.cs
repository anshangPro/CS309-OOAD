using System;
using System.Collections.Generic;
using Interfaces;
using StateMachine;
using UnityEngine;
using static Util.PositionUtil;

namespace Units
{
    public class Unit : MonoBehaviour, IClickable
    {
        public string UnitName { get; set; }
        public float MaxHealth { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }
        public float Defense { get; set; }

        /// <summary>
        /// 敏捷值
        /// </summary>

        public int Mv  { get; set; }
        public bool canBeTarget { get; set; }


        private const float Delta = 0.00001f;
        private bool hasMoved;
        private bool hasAttacked;

        public GameObject onBlock;


        protected virtual void Start()
        {
            Health = MaxHealth;
            Mv = 5;
            transform.position = DstBlock2DstPos3(onBlock);
            SetOnBlock(onBlock);
        }

        protected virtual void Update()
        {
            gameObject.GetComponent<SpriteRenderer>().transform.LookAt(Camera.main.transform);
        }

        protected virtual void MoveToBlock(GameObject block)
        {
            Vector3 newPos = DstBlock2DstPos3(block);

            const float speed = 5.0f;
            transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
        }


        /// <summary>
        /// 找到当前这个unit站的方块
        /// </summary>
        /// <returns>
        /// 当前Unit所在的Block
        /// </returns>
        public virtual void MoveAlongPath(List<Block> path)
        {
            hasMoved = true;
            if (path.Count <= 0)
            {
                return;
            }

            MoveToBlock(path[0].gameObject);

            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                    DstBlock2DstPos2(path[0].gameObject)) < Delta)
            {
                SetOnBlock(path[0].gameObject);
                path.RemoveAt(0);
            }

            if (path.Count == 0)
            {
                return;
            }
        }
        

        protected virtual void SetOnBlock(GameObject block)
        {
            transform.position = DstBlock2DstPos3(block);
            onBlock.GetComponent<Block>().standUnit = null;
            onBlock = block;
            onBlock.GetComponent<Block>().standUnit = this;
        }

        /// <summary>
        /// 攻击行为
        /// </summary>
        /// <param name="target"> 攻击目标: Unit </param>
        public virtual void Attack(Unit target)
        {
            hasAttacked = true;
            float realDamage = Damage - target.Defense;

            if (realDamage <= 0)
                return;

            target.TakeDamage(this, realDamage);
        }

        /// <summary>
        /// 受伤行为
        /// </summary>
        /// <param name="from"> 伤害来源: Unit </param>
        /// <param name="realDamage"> 伤害值: float </param>
        protected virtual void TakeDamage(Unit from, float realDamage)
        {
            if (Health <= realDamage)
                Health = 0;
            else
                Health -= realDamage;
        }

        /// <summary>
        /// 点击事件成败判断
        /// </summary>
        /// <returns> 点击是否成功: bool </returns>
        public bool IsClicked()
        {
            // 自己回合：行动、攻击
            // TODO 敌方回合：作为攻击目标
            if (!hasMoved || !hasAttacked)
            {
                GameManager.gameManager.GetComponent<Animator>().SetTrigger("unitClicked");
                // TODO GameData
                return true;
            }

            if (canBeTarget)
            {
                GameManager.gameManager.GetComponent<Animator>().SetTrigger("enemyClicked");
                // TODO GameData
                return true;
            }

            return false;
        }

        /// <summary>
        /// 己方回合开始，重置 移动/攻击 能力
        /// </summary>
        public void OnTurnBegin()
        {
            hasMoved = false;
            hasAttacked = false;
        }
    }
}