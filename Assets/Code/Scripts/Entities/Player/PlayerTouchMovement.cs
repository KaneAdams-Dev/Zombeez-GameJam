using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace ZombeezGameJam.Entities.Player
{
    public class PlayerTouchMovement : PlayerInputHandler
    {
        [SerializeField] private Vector2 _joystickSize = new Vector2(300, 300);
        [SerializeField] private FloatingJoystick _joystick;

        private Finger _movementFinger;
        private Vector2 _movementAmount;

        #region Unity Methods

        // This function is called when the object becomes enabled and active
        public override void OnEnable()
        {
            base.OnEnable();

            EnhancedTouchSupport.Enable();

            ETouch.Touch.onFingerDown += Touch_onFingerDown;
            ETouch.Touch.onFingerUp += Touch_onFingerUp;
            ETouch.Touch.onFingerMove += Touch_onFingerMove;
        }

        // This function is called when the behaviour becomes disabled or inactive
        public override void OnDisable()
        {
            base.OnDisable();

            ETouch.Touch.onFingerDown -= Touch_onFingerDown;
            ETouch.Touch.onFingerUp -= Touch_onFingerUp;
            ETouch.Touch.onFingerMove -= Touch_onFingerMove;

            EnhancedTouchSupport.Disable();
        }

        #endregion Unity Methods

        #region Custom Methods

        private void Touch_onFingerDown(Finger a_touchedFinger)
        {
            if (_movementFinger != null)
            {
                return;
            }

            if (a_touchedFinger.screenPosition.x < Screen.width / 2f)
            {
                _movementFinger = a_touchedFinger;
                _movementAmount = Vector2.zero;
                _joystick.gameObject.SetActive(true);
                _joystick.RTransform.sizeDelta = _joystickSize;
                _joystick.RTransform.anchoredPosition = ClampStartPosition(a_touchedFinger.screenPosition);
            }
        }

        private void Touch_onFingerUp(Finger a_releasedFinger)
        {
            if (a_releasedFinger != _movementFinger)
            {
                return;
            }

            _movementFinger = null;
            _joystick.Knob.anchoredPosition = Vector2.zero;
            _joystick.gameObject.SetActive(false);
            _movementAmount = Vector2.zero;

            xMoveInput = 0;
            _playerScript.UpdatePlayerState(PlayerStates.Idle);
        }

        private void Touch_onFingerMove(Finger a_movedFinger)
        {
            if (a_movedFinger != _movementFinger)
            {
                return;
            }

            Vector2 knobPos;
            float maxMovement = _joystickSize.x / 2f;
            ETouch.Touch currentTouch = a_movedFinger.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition, _joystick.RTransform.anchoredPosition) > maxMovement)
            {
                knobPos = (currentTouch.screenPosition - _joystick.RTransform.anchoredPosition).normalized * maxMovement;
            } else
            {
                knobPos = currentTouch.screenPosition - _joystick.RTransform.anchoredPosition;
            }

            _joystick.Knob.anchoredPosition = knobPos;
            _movementAmount = knobPos / maxMovement;

            xMoveInput = _movementAmount.x;
            isFacingLeft = xMoveInput < 0;
            _playerScript.UpdatePlayerState(PlayerStates.Run);
        }

        private Vector2 ClampStartPosition(Vector2 a_startPos)
        {
            if (a_startPos.x < _joystickSize.x / 2)
            {
                a_startPos.x = _joystickSize.x / 2;
            }

            if (a_startPos.y < _joystickSize.y / 2)
            {
                a_startPos.y = _joystickSize.y / 2;
            } else if (a_startPos.y > Screen.height - _joystickSize.y / 2)
            {
                a_startPos.y = Screen.height - _joystickSize.y / 2;
            }

            return a_startPos;
        }

        #endregion Custom Methods
    }
}
