using UnityEngine;

namespace Util
{
    public static class CellConverter
    {
        //Size is actually the (x, y) of the top right corner, not the size of the rect.
        public static Rect GameArea = new Rect(
            Camera.main.WorldToScreenPoint(
                 GameObject.Find("GameField").transform.position) + 
             (Vector3) ((RectTransform)GameObject.Find("GameField").transform).rect.position, 
            Camera.main.WorldToScreenPoint(
                GameObject.Find("GameField").transform.position) - 
            (Vector3) ((RectTransform)GameObject.Find("GameField").transform).rect.position);

        public static Vector2 RectSize = (GameArea.size - GameArea.position) / 20;
        
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
    }
}