using System;
using System.Collections.Generic;
using GameData;
using Interfaces;
using StateMachine;
using UnityEngine;
using static Util.PositionUtil;

namespace Units
{
    public class Unit : MonoBehaviour, IClickable
    {
        public int ofPlayer { get; set; }
        public string UnitName { get; internal set; }
        public float MaxHealth { get; internal set; }
        public float Health { get; internal set; }
        public float MaxMp { get; internal set; }
        public float Mp { get; internal set; }
        public float Damage { get; internal set; }
        public float Defense { get; internal set; }
        public int level { get; internal set; }
        public int AtkRange = 1;

        /// <summary>
        /// 敏捷值
        /// </summary>
        public int Mv { get; internal set; }

        internal float BaseHealth;
        internal float BaseMp;
        internal float BaseDamage;
        internal float BaseDefense;
        internal float DefenseUpdateRate;
        internal int BaseAtkRange = 1;

        /// <summary>
        /// 敏捷值
        /// </summary>
        internal int BaseMv;

        public bool canBeTarget { get; set; }
        private bool hasMoved { get; set; }
        private bool hasAttacked { get; set; }

        private const float Delta = 0.00001f;

        public Block onBlock;
        private static readonly int UnitClicked = Animator.StringToHash("unitClicked");
        private static readonly int EnemyClicked = Animator.StringToHash("enemyClicked");


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

        protected virtual void MoveToBlock(Block block)
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

            MoveToBlock(path[0]);

            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                    DstBlock2DstPos2(path[0])) < Delta)
            {
                SetOnBlock(path[0]);
                path.RemoveAt(0);
            }

            if (path.Count == 0)
            {
                return;
            }
        }


        protected virtual void SetOnBlock(Block block)
        {
            transform.position = DstBlock2DstPos3(block);
            onBlock.standUnit = null;
            onBlock = block;
            onBlock.standUnit = this;
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
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            GameDataManager gameData = GameDataManager.Instance;

            if ((!hasMoved || !hasAttacked) && canChoose(gameData.gameStatus))
            {
                gameData.SelectedUnit = this;
                // 进入状态UnitChosen
                animator.SetTrigger(UnitClicked);
                return true;
            }

            if (canBeTarget)
            {
                // 进入状态fight
                gameData.SelectedEnemy = this;
                animator.SetTrigger(EnemyClicked);
            }

            return false;
        }

        /// <summary>
        /// 当前角色是否可处于可被选择的状态下
        /// </summary>
        /// <param name="status"></param>
        /// <returns>boolean </returns>
        private bool canChoose(GameStatus status)
        {
            List<GameStatus> gameStatusList = new List<GameStatus>() { GameStatus.Default,  GameStatus.UnitChosen, GameStatus.Fight};
            return gameStatusList.Contains(status);
        }

        /// <summary>
        /// 己方回合开始，重置 移动/攻击 能力
        /// </summary>
        public void OnTurnBegin()
        {
            hasMoved = false;
            hasAttacked = false;
        }

        /// <summary>
        /// 己方回合结束，重置 移动/攻击 能力
        /// </summary>
        public void OnTurnEnd()
        {
            hasMoved = true;
            hasAttacked = true;
        }

        /// <summary>
        /// 当前角色已经移动完
        /// </summary>
        public void Moved()
        {
            hasMoved = true;
        }

        /// <summary>
        /// 当前角色已经攻击完
        /// </summary>
        public void Attacked()
        {
            hasAttacked = true;
        }

        /// <summary>
        /// 更新面板数据，仅能当升级 初始化时 由具体单位调用
        /// </summary>
        internal void UpdatePanel()
        {
            this.MaxHealth = BaseHealth + (int)Math.Round(level * 3.5);
            this.MaxMp = BaseMp + level;
            this.Mv = Math.Min(BaseMv + (int)Math.Floor((decimal)level / 10), 10);
            this.Defense = BaseDefense + level * DefenseUpdateRate;
            this.Damage = BaseDamage + level * 2 + (int)Math.Floor((decimal)level / 5) * 5;
        }
    }
}