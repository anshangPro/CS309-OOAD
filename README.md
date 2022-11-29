# CS309-OOAD

[toc]

## Time line

| 时间    | 事件                                       |
| ------- | ------------------------------------------ |
| Week 1  | unity + c#学习<br />战棋规则学习           |
| Week 2  | unity + c#学习<br />战棋规则学习           |
| Week 3  | 地图、场景搭建                             |
| Week 4  | 基础的人物对象模型设计，寻路、移动范围算法 |
| Week 5  | UI、总控的完善                             |
| Week 6  | 第一个战斗demo 游戏的完整流转              |
| Week 7  | 添加道具、装备系统                         |
| Week 8  | 场景、地图的丰富                           |
| Week 9  | 小剧情的加入                               |
| Week 10 | 存档功能                                   |
| Week 11 | dlc的添加                                  |
| Week 12 | 继续完善 准备答辩                          |



## Coding style

详细微软官方代码风格文档: [C# 编码约定 | Microsoft Docs](https://docs.microsoft.com/zh-cn/dotnet/csharp/fundamentals/coding-style/coding-conventions)

- 大括号需要换行。其中`if-else`中每个条件分支的大括号应另起一行，如下：

  ```c#
  class Demo
  {
      static void Main(string[] args)
      {
          if (/**/)
          {
              //...
          }
          else if (/**/)
          {
              //...
          }
          else
          {
              //...
          }
      }
  }
  ```

- 命名规范：

  | 命名对象              | 规则                          |
  | --------------------- | ----------------------------- |
  | 类名                  | 大驼峰规则 `DataService`      |
  | interface 命名        | I + 大驼峰 `IController`      |
  | public 成员           | 大驼峰 `public bool IsValid;` |
  | private internal 字段 | _ + 小驼峰 `_camelCasing`     |
  | 普通成员              | 小驼峰 `camelCasing`          |

- 禁止使用`var`和`dynamic`变量类型

## Git commit style

| type         | commit    |
| ------------ | --------- |
| 新功能       | feat:     |
| 修复bug      | fix:      |
| 代码重构     | refactor: |
| 文档修改     | docs:     |
| 代码格式修改 | style:    |
| 测试用例修改 | test:     |
| 其他优化     | chore:    |

## 地图存储格式

```json
{
  "blocks": [
    {
      "type": 0,
      "coordinate": [0, 1, 2], 
      "cost": 1,
      "optional": {
          "fuck": "you"
      }
    },
    {
      "type": 1,
      "coordinate": [1, 1, 4],
      "cost": 1
    }
  ],
  "environment": [
    {
      "type": "Tree",
      "coordinates": [
        [0, 0, 0],
        [1, 2, 1],
        [11, 45, 14]
      ]
    },
    {
      "type": "House",
      "coordinates": [
        [1, 1, 4]
      ]
    }
  ]
}
```

`blocks`里面存一个list。list里面存许多dict。每个dict表示一个方块。每个dict里面必须有`type`和`coordinate`两个属性，分别表示block的类型和坐标（三维）。`cost`表示方块的移动消耗。dict可以添加其余optional属性来限制block的某些属性。

`environment`里存环境对象。key表示某个地形的prefab的类型，value是一个list，其中每个list对象存当前prefab在实例化时需要固定的坐标。



## Feature

- [ ] 完整的游戏结构：游戏主界面，游戏内界面等。
- [ ] 至少实现两种游戏模式：
  - [ ] 单人模式（人机对战）: 人机对战的 AI 需要有一定的行为逻辑，不能设置为简单的随机。设计至少两个关卡。
  - [ ] 本地多人对战模式：玩家可以在同一台设备上交替对战。
- [ ] 设计至少三种棋子职业/兵种，每个兵种至少包含三种属性（如攻击，防御，血量等）。不 同兵种需要有除基础属性外的明显区分。举例：弓箭手，战士，魔法师，牧师，骑士等。
- [ ] 简易的地形要素，至少包括：
  - [ ] 有可移动和不可移动的地形区分。
  - [ ] 可以自定义地图，通过文件的形式读取。存储格式不限。
- [ ] 简易的 rpg 元素，至少包括：
  - [ ] 棋子可以提升等级，携带装备/学习技能/使用道具（至少实现其中一个）。
  - [ ] 至少设计 2 种不同功能的技能/道具/装备。
- [ ] 基本的 UI 交互，至少包括：
  - [ ] 选中棋子时显示能移动的范围。
  - [ ] 棋子选中与行动完毕后的不同显示效果。
  - [ ] 在周围有多个敌人的时候可以选择战斗的对象。
- [ ]  完备的游戏逻辑，至少包括：
  - [ ] 双方棋子对战时，合理且正确的攻击顺序及伤害计算，要求显示出简易的过程。
  - [ ] 在特定的情况或一方棋子全灭的时候，正确判断游戏胜负。
  - [ ] 正常状态下，棋子每一回合最多只能移动一次，攻击/技能一次或使用一次道具。
  - [ ] 敌我双方正常的回合交互。要求所有我方棋子行动后结束自己的回合或手动提前结束 回合。（玩家回合/棋子回合，两种模式都可以）
