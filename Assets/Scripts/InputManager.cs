using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class InputManager: MonoBehaviour{
  private PlayerInput playerInput;
  private PlayerInput.PlayerActions playerActions; 
  private PlayerMotor playerMotor;
  private PlayerLook playerLook;

  void Awake() {
    playerInput = new PlayerInput();
    playerActions = playerInput.Player;
    playerMotor = GetComponent<PlayerMotor>();
    playerLook = GetComponent<PlayerLook>();
    playerActions.Jump.performed += ctx => playerMotor.Jump();
    if (playerActions.Sprint != null) {
      playerActions.Sprint.performed += ctx => playerMotor.Sprint(true);
      playerActions.Sprint.canceled += ctx => playerMotor.Sprint(false);
    }
  }

  void FixedUpdate() {
    playerMotor.Move(playerActions.Move.ReadValue<Vector2>());
  }

  void LateUpdate() {
    playerLook.ProcessLook(playerActions.Look.ReadValue<Vector2>());
  }

  void OnEnable() {
    playerActions.Enable();
  }

  void OnDisable() {
    playerActions.Disable();
  } 
}
