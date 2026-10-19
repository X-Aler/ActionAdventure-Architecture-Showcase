using UnityEngine;

namespace Controllers
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 moveInput;
        public bool isSpritPressed;
        public bool isJumpPressed;

        private void Update()
        {
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            isSpritPressed = Input.GetKey(KeyCode.LeftShift);
            
            isJumpPressed = Input.GetKeyDown(KeyCode.Space);
        }
    }
}
