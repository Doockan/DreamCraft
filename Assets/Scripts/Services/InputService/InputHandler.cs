using System;
using UnityEngine;
using Assets.Scripts.Services.Player;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.Services.InputService
{
    public class InputHandler : IInputHandler, IDisposable
    {
        private readonly IPlayerHandler _playerHandler;
        private readonly PlayerInputActions _inputActions;

        public InputHandler(IPlayerHandler playerHandler)
        {
            _playerHandler = playerHandler;

            _inputActions = new PlayerInputActions();
            _inputActions.Enable();

            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMoveCanceled;
            _inputActions.Player.Mouse.performed += MousPosition;
            _inputActions.Player.Mouse.canceled += MousPositionCanceled;
            _inputActions.Player.Attack.started += OnAttackStarted;
            _inputActions.Player.Attack.canceled += OnAttackCanceled;
        }

        private void OnMove(CallbackContext context)
        {
            _playerHandler.Player?.Movement.OnMove(context.ReadValue<Vector2>());
        }

        private void OnMoveCanceled(CallbackContext context)
        {
            _playerHandler.Player?.Movement.OnMove(Vector2.zero);
        }

        private void MousPosition(CallbackContext context)
        {
            _playerHandler.Player?.Rotate.OnMousePosition(context.ReadValue<Vector2>());
        }

        private void MousPositionCanceled(CallbackContext context)
        {
            _playerHandler.Player?.Rotate.OnMousePosition(Vector2.zero);
        }

        private void OnAttackStarted(CallbackContext context)
        {
            Debug.Log("Started");
        }

        private void OnAttackCanceled(CallbackContext context)
        {
            Debug.Log("Canceled");
        }

        public void Dispose()
        {
            _inputActions.Disable();
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Attack.started -= OnAttackStarted;
            _inputActions.Player.Attack.canceled -= OnAttackCanceled;
            _inputActions.Player.Mouse.performed -= MousPosition;
        }
    }
}