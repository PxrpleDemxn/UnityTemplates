using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
	InputActions _inputs;

	public Vector2 MoveVector { get; private set; }
	public Vector2 LookVector { get; private set; }
	public bool IsRunning { get; private set; }

	private void Awake()
	{
		_inputs = new InputActions();

		_inputs.Gameplay.Run.performed += Run;
		_inputs.Gameplay.Run.canceled += Run;
	}
	private void Update()
	{
		MoveVector = _inputs.Gameplay.Move.ReadValue<Vector2>();
		LookVector = _inputs.Gameplay.Look.ReadValue<Vector2>();
	}
	#region InputActions setup
	private void OnEnable()
	{
		_inputs.Gameplay.Enable();
	}

	private void OnDisable()
	{
		_inputs.Gameplay.Disable();
	}
	#endregion InputActions setup

	private void Run(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			IsRunning = true;
		}
		else if (ctx.canceled)
		{
			IsRunning = false;
		}
	}
}
