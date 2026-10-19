using Controllers;
using UnityEngine;

public class ExitButton : ButtonController
{
    public override void Click()
    {
        Application.Quit();
    }
}
