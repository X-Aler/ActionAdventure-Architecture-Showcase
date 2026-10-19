using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput), typeof(PlayerAudio))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float jumpHeight = 2f; 
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float runSpeed = 7.5f;
        [SerializeField] private Transform playerCamera;

        private CharacterController _controller;
        private PlayerInput _playerInput;
        private PlayerAudio _playerAudio;
    
        private Vector3 _playerVelocity;
        private Vector3 _impact;
        private float _impactDamping = 5f;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();
            _playerAudio = GetComponent<PlayerAudio>();
        
            CursorController.Lock();
        }

        private void Update()
        {
            HandleGravity();
            HandleRotation();
            
            var moveDirection = GetMovementDirection();
            var finalVelocity = moveDirection + new Vector3(0, _playerVelocity.y, 0);
            
            if (_impact.magnitude > 0.2f)
            {
                finalVelocity += _impact;
                _impact = Vector3.Lerp(_impact, Vector3.zero, _impactDamping * Time.deltaTime);
            }
            
            _controller.Move(finalVelocity * Time.deltaTime);
            
            var isMoving = _playerInput.moveInput.magnitude > 0.1f;
            _playerAudio.UpdateSound(isMoving, _playerInput.isSpritPressed);
        }

        private Vector3 GetMovementDirection()
        {
            var currentSpeed = _playerInput.isSpritPressed ? runSpeed : walkSpeed;
            
            var forward = playerCamera.forward;
            var right = playerCamera.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
        
            var moveDirection = (forward * _playerInput.moveInput.y + right * _playerInput.moveInput.x).normalized;

            return moveDirection * currentSpeed;
        }

        private void HandleGravity()
        {
            if (_controller.isGrounded && _playerVelocity.y < 0f)
                _playerVelocity.y = -2f;
            
            if (_controller.isGrounded && _playerInput.isJumpPressed)
                _playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        
            _playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
    
        private void HandleRotation()
        {
            var lookDirection = playerCamera.forward;
            if (lookDirection.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(lookDirection);
        }
        
        public void AddImpact(Vector3 direction, float force)
        {
            direction.y = 0;
            _impact += direction.normalized * force;
        }
    }
}
