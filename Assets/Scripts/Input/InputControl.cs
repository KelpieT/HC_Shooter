using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControl
{
	public event Action<Vector2> onTouch;
	public event Action<Vector2> onMove;
	private bool isTuched = false;
	private SimpleMouseControl simpleMouseControl;

	public void Init()
	{
		simpleMouseControl = new SimpleMouseControl();
		simpleMouseControl.Enable();
		simpleMouseControl.Game.Down.performed += StartDrag;
		simpleMouseControl.Game.Down.canceled += EndDrag;
	}

	private void StartDrag(InputAction.CallbackContext contex)
	{
		Vector2 currentPos = simpleMouseControl.Game.Position.ReadValue<Vector2>();
		isTuched = true;
		onTouch?.Invoke(currentPos);
	}

	private void EndDrag(InputAction.CallbackContext contex)
	{
		isTuched = false;
	}

	public void Disable()
	{
		simpleMouseControl.Game.Down.performed -= StartDrag;
		simpleMouseControl.Game.Down.canceled -= EndDrag;
	}

}
