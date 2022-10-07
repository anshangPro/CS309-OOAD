using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameStatus
{
    Default,            // 可以选择人物，选中后进入 move 状态 选择空地，选中后进入 MainMenu
    FightMenu,          // 选择敌人 选择装备 .... 该状态可以选择敌人
    Fight,              // 播放战斗动画
    Move,               // 此时选择一个block -> 播放动画 -> 人物移动到目标位置 -> MenuAfterMove
    MainMenu,           // 可以选择存档 关闭游戏 ..
    MenuAfterMove       // 攻击按钮 , 技能按钮等等 该状态无法选择敌人
} 