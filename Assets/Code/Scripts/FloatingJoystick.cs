using UnityEngine;

namespace ZombeezGameJam
{
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public class FloatingJoystick : MonoBehaviour
    {
        [SerializeField] private RectTransform _knobOutline;
        private RectTransform _rectTransform;

        public RectTransform RTransform => _rectTransform;
        public RectTransform Knob => _knobOutline;

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
