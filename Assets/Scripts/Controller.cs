using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    [RequireComponent(typeof(GameField))]
    public class Controller : MonoBehaviour, PlayerActions.IGameplayActions
    {
        private bool _checkDirection(Vector2Int dir)
        {
            return (
                dir == Vector2Int.right || dir == Vector2Int.left || dir == Vector2Int.up || dir == Vector2Int.down
            );
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var dir = Vector2Int.RoundToInt(context.ReadValue<Vector2>());
            Debug.Log("OnMove " + dir);
            if (!_checkDirection(dir)) return;
            if (dir == Vector2Int.zero) return;
            var component = gameObject.GetComponent<GameField>();
            component.Move(dir);
            component.CreateCell();
        }
    }
}