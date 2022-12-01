namespace StateMachine
{
    /// <summary>
    /// Start: 游戏的开始状态 在玩家点击存档后进入
    /// Default: 默认状态：可以选择一个单位，若为己方单位，会进入unitChosen状态，否则会显示单位的属性
    /// UnitChosen: 角色被选中状态：选中了一个己方单位，UI会展示单位可以走的路径，玩家可以选择一个范围内的方块进行移动
    /// BlockSelected: 方块被选中状态：选中一个状态，UI会展示角色到这个方块的最短路径，
    ///                 第二次选中不同的方块时，会重新显示路径
    ///                 第二次选中相同的方块时，会进入Move状态
    /// Move: 角色移动，播放角色的移动动画，移动完进入MenuAfterMove
    /// MenuAfterMove: 角色可以选择攻击，技能或者使用道具
    /// FightMenu: UI会展示当前角色的攻击范围，角色选择一个范围内的敌人攻击，选择后进入Fight状态
    /// Fight: UI播放战斗动画，播放完回到Default状态
    /// </summary>
    public enum GameStatus
    {
        Start,
        Default,  
        UnitChosen,
        BlockSelected,
        Move,
        MenuAfterMove,
        FightMenu,
        Fight,
        GameOver
    }
}