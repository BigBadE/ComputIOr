using UnityEngine;

namespace Util
{
    public static class CellConverter
    {
        public static readonly Camera Camera = Camera.main;
            
        //Size is actually the (x, y) of the top right corner, not the size of the rect.
        public static readonly Rect GameArea = new(
            Camera.WorldToScreenPoint(
                 GameObject.Find("GameField").transform.position) + 
             (Vector3) ((RectTransform)GameObject.Find("GameField").transform).rect.position, 
            Camera.WorldToScreenPoint(
                GameObject.Find("GameField").transform.position) - 
            (Vector3) ((RectTransform)GameObject.Find("GameField").transform).rect.position);

        public static readonly Vector2 RectSize = (GameArea.size - GameArea.position) / 20;
        
        public static bool MouseWithinGameArea()
        {
            return Input.mousePosition.x > GameArea.x && Input.mousePosition.x < GameArea.width &&
                   Input.mousePosition.y > GameArea.y && Input.mousePosition.y < GameArea.height;
        }
        
        public static Vector2Int GetMouseCell()
        {
            return Floor(((Vector2) Input.mousePosition - GameArea.position) / RectSize);
        }

        private static Vector2Int Floor(Vector2 input)
        {
            return new Vector2Int((int) input.x, (int) input.y);
        }
        
        public static Vector2 CellToScreenPosition(Vector2 cell)
        {
            return cell * RectSize + GameArea.position;
        }
        
        public static Vector2 CellToWorldPosition(Vector2 cell)
        {
            return Camera.ScreenToWorldPoint(cell * RectSize + GameArea.position);
        }
    }
}