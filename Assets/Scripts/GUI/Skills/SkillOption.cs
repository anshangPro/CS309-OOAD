using GameData;
using Interfaces;
using StateMachine;
using TMPro;
using UnityEngine;
using Units;

namespace GUI.Skills
{
    public class SkillOption: MonoBehaviour, IClickable
    {
        public TextMeshProUGUI skillName;
        public TextMeshProUGUI pp;
        public Skill Skill;
        
        private static readonly int AttackClicked = Animator.StringToHash("attackClicked");
  
        
        public void SetSkill(Skill s)
        {
            skillName.text = s.Name;
            pp.text = $"{s.RemainSkillPoint}/{s.SkillPoint}";
            this.Skill = s;
        }

        public bool IsClicked()
        {
            if (Skill.RemainSkillPoint > 0)
            {
                Animator animator = GameManager.gameManager.GetComponent<Animator>();
                GameDataManager.Instance.SelectedSkill = this;
                UIManager.Instance.SkillPanel.SetActive(false);
                this.Skill.TakeEffect();
                animator.SetTrigger(AttackClicked);
                return true;
            }
            return false;
        }
    }
}