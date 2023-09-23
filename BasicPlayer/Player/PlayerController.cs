using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
	private PlayerStats _player = new PlayerStats();
	[SerializeField] private Inputs _inputs;

	//--- PLAYER ---//
	private float _threshold = 0.001f;
	private float originalHeight;
	private Vector3 originalCenter;
	public float FallTimeout = 0.15f;
	private float _terminalVelocity = 53.0f;
	private float _verticalVelocity;
	private bool _grounded;

	//--- CAMERA ---//
	public GameObject CmCamTarget;

	private float _cmTargetPitch;
	private float _rotationSpeed = 1.0f;
	private float _rotationVelocity;
	private float _minCamPitch = -90.0f;
	private float _maxCamPitch = 90.0f;

	//--- TIMING ---//
	private float _fallTimeoutDelta;

	//--- REFERENCES ---//
	private CharacterController _characterController;
	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();

	}

	void Start()
	{
		Settings.Instance.CanControlPlayer = true;
		_player.SetSpeed(_player.WalkSpeed);
		originalHeight = _characterController.height;
		originalCenter = _characterController.center;
	}

	void Update()
	{
		if (Settings.Instance.CanControlPlayer)
		{
			CheckGround();
			Movement();
		}
	}

	private void LateUpdate()
	{
		if (Settings.Instance.CanControlPlayer)
		{
			Look();
		}
	}

	//--- BASE ---//
	private void Movement()
	{
		Vector3 moveDir = new Vector3(_inputs.MoveVector.x, 0f, _inputs.MoveVector.y).normalized;

		if (_inputs.MoveVector != Vector2.zero)
		{
			moveDir = transform.right * _inputs.MoveVector.x + transform.forward * _inputs.MoveVector.y;
		}

		_characterController.Move(moveDir.normalized * (_player.Speed * Time.deltaTime) +
								  new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
	}

	private void Look()
	{
		if (_inputs.LookVector.sqrMagnitude >= _threshold)
		{
			_cmTargetPitch += _inputs.LookVector.y * _rotationSpeed * Settings.Instance.GameSettings.Sensitivity;
			_rotationVelocity = _inputs.LookVector.x * _rotationSpeed * Settings.Instance.GameSettings.Sensitivity;

			_cmTargetPitch = ClampAngle(_cmTargetPitch, _minCamPitch, _maxCamPitch);

			CmCamTarget.transform.localRotation = Quaternion.Euler(_cmTargetPitch, 0.0f, 0.0f);
			transform.Rotate(Vector3.up * _rotationVelocity);
		}
	}

	//--- CALCULATIONS ---//
	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

	private void CheckGround()
	{
		_grounded = _characterController.isGrounded;

		if (_grounded)
		{
			_fallTimeoutDelta = FallTimeout;

			if (_verticalVelocity < 0.0f)
			{
				_verticalVelocity = -2f;
			}
		}

		if (_verticalVelocity < _terminalVelocity)
		{
			_verticalVelocity += _player.Gravity * Time.deltaTime;
		}
	}
}