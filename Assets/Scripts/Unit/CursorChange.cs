using UnityEngine;

namespace Unit
{
    public class CursorChange : MonoBehaviour
    {
        public Texture2D inRangeCursor;
        public Texture2D hoverCursor;
        public Texture2D originalCursor;
        private Texture2D[] cursors;
        private Vector2 _hotSpot = new Vector2(20, 20);
        private CursorState _state;
        private CursorState _previousState;

        public void Start()
        {
            cursors = new[] {originalCursor, hoverCursor, inRangeCursor};
            PlayerController.playerController.ONHoverOverEnemy += ChangeCursor;
        }

        private void ChangeCursor(bool overEnemy, Transform targetTransform)
        {
            if (!overEnemy)
            {
                _state = CursorState.NotOverEnemy;
            }
            else
            {
                if (targetTransform != null)
                {
                    _state = PlayerController.playerController.InRange(targetTransform)
                        ? CursorState.InRangeAndOverEnemy
                        : CursorState.OverEnemy;
                }
                else
                    _state = CursorState.NotOverEnemy;
            }
            if (_state != _previousState)
                Cursor.SetCursor(cursors[(int)_state], _hotSpot, CursorMode.Auto);
            _previousState = _state;
        }
        private enum CursorState
        {
            NotOverEnemy,
            OverEnemy,
            InRangeAndOverEnemy
        }
    }
}