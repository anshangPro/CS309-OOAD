using System;
using System.Collections;
using System.Collections.Generic;
using DTO;
using GameData;
using Interfaces;
using StateMachine;
using UnityEngine;
using Util;
using static Util.PositionUtil;

namespace Units
{
    public abstract class Unit : MonoBehaviour, IClickable, IFloatPanel
    {
        public int type { get; internal set; }
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
        public int Exp = 0;
        private int RewardExp = 120;

        public LinkedList<Skill> Skills;

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

        public bool hasMoved { get; set; }
        private bool hasAttacked { get; set; }

        private const float Delta = 0.00001f;

        public Block onBlock;
        private static readonly int UnitClickedAnime = Animator.StringToHash("unitClicked");
        private static readonly int EnemyClickedAnime = Animator.StringToHash("enemyClicked");
        private static readonly int AttackMeleeAnime = Animator.StringToHash("attack_melee");
        private static readonly int AttackSkillAnime = Animator.StringToHash("attack_skill");
        private static readonly int TakeDamageAnime = Animator.StringToHash("take_damage");
        private static readonly int DeathAnime = Animator.StringToHash("death");


        protected virtual void Start()
        {
            // Health = MaxHealth;
            // Mv = 5;
            transform.position = DstBlock2DstPos3(onBlock);
            SetOnBlock(onBlock);

            GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        protected virtual void Update()
        {
            //gameObject.GetComponent<SpriteRenderer>().transform.LookAt(Camera.main.transform);
            Vector3 old_eu = gameObject.GetComponent<SpriteRenderer>().transform.eulerAngles;
            gameObject.GetComponent<SpriteRenderer>().transform.eulerAngles = new Vector3(-old_eu.x, Camera.main.transform.eulerAngles.y, old_eu.z);
        }

        protected virtual void MoveToBlock(Block block)
        {
            FaceTo(block.gameObject.transform);

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
        /// 攻击行为 在结尾时调用 进行伤害的计算
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
            {
                Health = 0;
                from.AddExp(RewardExp);
            }
            else
            {
                Health -= realDamage;
            }
        }

        public void AddExp(int amount)
        {
            int curExpUpperBound = level * 100;
            Exp += amount;
            while (Exp >= curExpUpperBound)
            {
                level++;
                UpdatePanel();
                Debug.Log($"{this} update to level {level}");
                Exp -= curExpUpperBound;
                curExpUpperBound = level * 100;
            }
        }

        /// <summary>
        /// 点击事件成败判断
        /// </summary>
        /// <returns> 点击是否成功: bool </returns>
        public bool IsClicked()
        {
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            GameDataManager gameData = GameDataManager.Instance;

            if (!hasMoved && canChoose(gameData.gameStatus))
            {
                gameData.SelectedUnit = this;
                // 进入状态UnitChosen
                animator.SetTrigger(UnitClickedAnime);
                return true;
            }

            if (gameData.gameStatus == GameStatus.FightMenu && CanFightWith())
            {
                // 进入状态fight
                gameData.SelectedEnemy = this;
                animator.SetTrigger(EnemyClickedAnime);
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
            List<GameStatus> gameStatusList = new List<GameStatus>() { GameStatus.Default,  GameStatus.UnitChosen, GameStatus.FightMenu};
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
            GameDataManager.Instance.GetCurrentPlayer().FinishedUnit++;
        }

        /// <summary>
        /// 当前角色已经攻击完
        /// </summary>
        public void Attacked()
        {
            hasAttacked = true;
        }

        public void DamageTaken()
        {
            if (Health <= 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger(DeathAnime);
            }
        }

        public void Died()
        {
            Destroy(gameObject);
            GameDataManager gameData = GameDataManager.Instance;
            gameData.Players[ofPlayer].UnitsList.Remove(this);
            onBlock.standUnit = null;
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

            Health = MaxHealth;
            Mp = MaxMp;
            
            foreach (Skill skill in Skills)
            {
                skill.RemainSkillPoint = skill.SkillPoint;
            }
        }

        public virtual bool CanFightWith()
        {
            return false;
        }

        public virtual String GetType()
        {
            return "Unit";
        }

        /// <summary>
        /// 使该单位面向对象。此乃正道。
        /// </summary>
        public void FaceTo(Transform target)
        {
            Transform camera_transform = gameObject.GetComponent<SpriteRenderer>().transform;
            double camera_euler_Y = camera_transform.eulerAngles.y;
            if (camera_euler_Y > 180.0)
                camera_euler_Y -= 360.0;

            // 180° 特判
            if (target.position.x == camera_transform.position.x &&
                target.position.z < camera_transform.position.z)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = (camera_euler_Y < 0);
                Debug.Log("target angle: 180");
                Debug.Log("flipped: " + (camera_euler_Y).ToString());
                return;
            }

            double angle = camera_euler_Y - Math.Atan2(
                target.position.x - camera_transform.position.x,
                target.position.z - camera_transform.position.z
            ) / Math.PI * 180;
            if (angle > 180.0)
                angle -= 360.0;
            if (angle < -180.0)
                angle += 360.0;

            gameObject.GetComponent<SpriteRenderer>().flipX = (angle > 0);
            Debug.Log("target angle: " + angle.ToString());
        }

        /// <summary>
        /// 播放自身的攻击动画
        /// </summary>
        public void PlayAttackAnime()
        {
            Animator selfAnimator = this.GetComponent<Animator>();
            FaceTo(GameDataManager.Instance.SelectedEnemy.gameObject.GetComponent<SpriteRenderer>().transform);
            if (GameDataManager.Instance.SelectedSkill is not null)
            {
                selfAnimator.SetTrigger(AttackSkillAnime);
            } else
            {
                selfAnimator.SetTrigger(AttackMeleeAnime);
            }
        }

        /// <summary>
        /// 播放敌方的受伤动画
        /// </summary>
        public void PlayTakeDamageAnime()
        {
            Debug.Log("take damage");
            Unit target = GameDataManager.Instance.SelectedEnemy;
            Animator oppositeAnimator = target.GetComponent<Animator>();
            target.FaceTo(this.gameObject.GetComponent<SpriteRenderer>().transform);
            oppositeAnimator.SetTrigger(TakeDamageAnime);
        }

        public void ShowPanel()
        {
            LeftDownInfoPanelController controller = LeftDownInfoPanelController.Instance;
            controller.magic = (int)Mp;
            controller.maxMagic = (int)MaxMp;
            controller.health = (int)Health;
            controller.maxHealth = (int)MaxHealth;
            controller.maxEnergy = level * 100;
            controller.energy = Exp;

            controller.atk = (int)Damage;
            controller.def = (int)Defense;
            controller.move = Mv;
            controller.level = level;
            controller.name = UnitName;
            
            controller.FixedUpdate();
            controller.gameObject.SetActive(true);
        }

        public void CopyFrom(UnitDTO unit)
        {
            type = unit.type;
            onBlock = GameDataManager.Instance.blockList[new Vector2(unit.coornidate[0], unit.coornidate[1])].Item1
                .GetComponent<Block>();
            onBlock.standUnit = this;
            UnitName = unit.UnitName;
            MaxHealth = unit.MaxHealth;
            Health = unit.Health;
            MaxMp = unit.MaxMp;
            Mp = unit.Mp;
            Damage = unit.Damage;
            Defense = unit.Defense;
            AtkRange = unit.AtkRange;
        }

        public Unit()
        {
            this.Skills = new LinkedList<Skill>();
        }

    }
}