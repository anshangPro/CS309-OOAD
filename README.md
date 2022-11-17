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
      "cost": 1
    },
    {
      "type": 1,
      "coordinate": [1, 1, 4],
      "cost": 1
    }
  ],
  "environment": {
    "Tree": [
      [0, 0, 0],
      [1, 2, 1],
      [11, 45, 14]
    ],
    "House":[
      [1, 2, 0]
    ]
  }
}
```

`blocks`里面存一个list。list里面存许多dict。每个dict表示一个方块。每个dict里面必须有`type`和`coordinate`两个属性，分别表示block的类型和坐标（三维）。`cost`表示方块的移动消耗。dict可以添加其余optional属性来限制block的某些属性。

`environment`里存环境对象。key表示某个地形的prefab的类型，value是一个list，其中每个list对象存当前prefab在实例化时需要固定的坐标。
