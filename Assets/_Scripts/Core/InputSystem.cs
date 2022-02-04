using UnityEngine;

namespace DartsGames.Core.Input
{
    public class InputSystem : Singleton<InputSystem>
    {
        public event System.Action onMouseUp;
        public event System.Action onMouseDown;


        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 lastPosition;

        public bool isHold;

        public Vector3 delta;

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                isHold = true;
                startPosition = lastPosition = UnityEngine.Input.mousePosition;
                onMouseDown?.Invoke();
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                isHold = false;
                startPosition = lastPosition = Vector3.zero;
                onMouseUp?.Invoke();
            }

            if (!isHold) return;

            delta = lastPosition - UnityEngine.Input.mousePosition;
            lastPosition = UnityEngine.Input.mousePosition;
        }
    }
}
