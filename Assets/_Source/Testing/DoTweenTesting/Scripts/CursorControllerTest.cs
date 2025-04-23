using System;
using UnityEngine;

namespace _Source.Testing.DoTweenTesting.Scripts
{
    public class CursorControllerTest : MonoBehaviour
    {
        [SerializeField] private Texture2D _cursorTexture;
        [SerializeField] private Vector2 _clickPosition = Vector2.zero;

        private void Start()
        {
            Cursor.SetCursor(_cursorTexture, _clickPosition, CursorMode.Auto);
        }
    }
}