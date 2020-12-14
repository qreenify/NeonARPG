using UnityEngine;

namespace Unit
{
    public class CursorChange : MonoBehaviour
    {
        public PlayerController PlayerController;
        public Texture2D cursor;
        public Texture2D originalCursor;

        public void Update()
        {
            if (PlayerController.InRange)
            {
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
            }
            else
            {
                Cursor.SetCursor(originalCursor, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
    }
}