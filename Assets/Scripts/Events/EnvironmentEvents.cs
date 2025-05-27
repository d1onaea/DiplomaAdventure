using System;

public class EnviornmentEvents
{
    public event Action onDrawerOpened;
    public event Action onDrawerClosed;

    public void DrawerOpened()
    {
        onDrawerOpened.Invoke();
    }

    public void DrawerClosed()
    {
        onDrawerClosed.Invoke();
    }
}
