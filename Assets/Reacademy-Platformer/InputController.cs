using System;
using UnityEngine;
using Zenject;

public class InputController : ITickable
{
    public event Action OnLeftEvent;
    public event Action OnRightEvent;
    
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

    public void Tick()
    {
        CheckInput();
    }
}