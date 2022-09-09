# CS309-OOAD

[toc]

## Time line

| 时间   | 事件                             |
| ------ | -------------------------------- |
| Week 1 | unity + c#学习<br />战棋规则学习 |
| Week 2 |                                  |
| Week 3 |                                  |



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

