using System;

public class DropdownNavStateContainer{
    public bool IsOpened { get; private set; }

    public event Action OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke(); //null => .Invoke()

    public void SetVisible(bool state){
        IsOpened = state;
        NotifyStateChanged();
    }
}