using System;
using UnityEngine;

public class InputController
{
    public event Action OnLeftEvent;
    public event Action OnRightEvent;

    public InputController()
    {
        TickableManager.TickableManager.UpdateNotify += CheckInput;
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnLeftEvent?.Invoke();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            OnRightEvent?.Invoke();
        }
    }
}