using UnityEngine;

namespace Util
{
    public static class CellConverter
    {
        private static Camera _camera = Camera.main;
            
        //Size is actually the (x, y) of the top right corner, not the size of the rect.
        private static Rect GameArea = new(
            _camera.WorldToScreenPoint(
                 GameObject.Find("GameField").transform.position) + 
             (Vector3) ((RectTransform)GameObject.Find("GameField").transform).rect.position, 
            _camera.WorldToScreenPoint(
                GameObject.Find("GameField").transform.position) - 
            (Vector3) ((RectTransform)GameObject.Find("GameField").transform).rect.position);

        public static readonly Vector2 RectSize = (GameArea.size - GameArea.position) / 20;
        
        public static bool MouseWithinGameArea()
        {
            return Input.mousePosition.x > GameArea.x && Input.mousePosition.x < GameArea.width &&
                   Input.mousePosition.y > GameArea.y && Input.mousePosition.y < GameArea.height;
        }
        
        public static Vector2 GetMouseCell()
        {
            return ((Vector2) Input.mousePosition - GameArea.position) / RectSize;
        }

        public static Vector2 CellToScreenPosition(Vector2 cell)
        {
            return cell * RectSize + GameArea.position;
        }
        
        public static Vector2 CellToWorldPosition(Vector2 cell)
        {
            return _camera.ScreenToWorldPoint(cell * RectSize + GameArea.position);
        }
    }
}