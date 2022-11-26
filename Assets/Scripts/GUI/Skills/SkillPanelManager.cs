using System;
using GameData;
using Units;
using UnityEngine;

namespace GUI.Skills
{
    public class SkillPanelManager : MonoBehaviour
    {
        public GameObject content;
        public GameObject skillPrefab;

        private void OnEnable()
        {
            Unit p = GameDataManager.Instance.MovedUnit;
            GameDataManager.Instance.PanelShowing = true;
            if (p is null) return;
            foreach (Skill pSkill in p.Skills)
            {
                GameObject skill = Instantiate(skillPrefab, content.transform);
                SkillOption so = skill.GetComponent<SkillOption>();
                so.SetSkill(pSkill);
            }
        }

        private void OnDisable()
        {
            GameDataManager.Instance.PanelShowing = false;
            int len = content.transform.childCount;
            for (int i = 0; i < len; i++)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
        }
    }
}