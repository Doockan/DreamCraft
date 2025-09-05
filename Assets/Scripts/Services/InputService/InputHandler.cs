using System;
using UnityEngine;

namespace Assets.Scripts.Services.InputService
{
    public class InputHandler : IInputHandler, IDisposable
    {
        private readonly PlayerInputActions _inputActions;

        public InputHandler()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Enable();

            _inputActions.Player.Move.performed += context => OnMove(context.ReadValue<Vector2>());
            _inputActions.Player.Move.canceled += _ => OnMove(Vector2.zero);

            _inputActions.Player.Attack.started += _ => OnAttackStarted();
            _inputActions.Player.Attack.canceled += _ => OnAttackCanceled();
        }

        private void OnMove(Vector2 value)
        {
            Debug.Log($"Value {value}");
        }

        private void OnAttackStarted()
        {
            Debug.Log("Started");
        }

        private void OnAttackCanceled()
        {
            Debug.Log("Canceled");
        }

        public void Dispose()
        {
            _inputActions.Disable();
        }
    }
}