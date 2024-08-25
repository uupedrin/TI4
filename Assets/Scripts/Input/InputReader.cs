using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader", order = 0)]
public class InputReader : ScriptableObject, InputGeneratedClass.IGameplayActions
{
	private InputGeneratedClass _inputMap;
	
	public static InputReader GetInputReader(){return Resources.Load("Input/InputReader") as InputReader;}
	
	private void OnEnable()
	{
		if(_inputMap is null)
		{
			_inputMap = new();
			_inputMap.Gameplay.SetCallbacks(this);
		}
	}
	
	private void OnDisable()
	{
		DisableAll();
	}
	
	public void DisableAll()
	{
		_inputMap.Gameplay.Disable();
	}
	
	#region GameplayActions
	//Actions
	public void SetGameplay()
	{
		DisableAll();
		_inputMap.Gameplay.Enable();
	}
	public event Action<Vector2> MovementEvent;
	public event Action JumpPressedEvent;
	public event Action JumpReleasedEvent;
	
	//Interface Implementation
	public void OnMovement(InputAction.CallbackContext context)
	{
		MovementEvent?.Invoke(context.ReadValue<Vector2>());
		Debug.Log("Moving");
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if(context.phase == InputActionPhase.Started)
		{
			JumpPressedEvent?.Invoke();
		}
		else if(context.phase == InputActionPhase.Canceled)
		{
			JumpReleasedEvent?.Invoke();
		}
	}
	#endregion
}