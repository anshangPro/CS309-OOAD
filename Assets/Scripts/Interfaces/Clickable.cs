interface IClickable {
    /// <summary>
    /// when on click, the mouse controller will call the object's isClicked function
    /// </summary>
    /// <returns>bool whether the obj finished its click event</returns>
    bool IsClicked();
}
