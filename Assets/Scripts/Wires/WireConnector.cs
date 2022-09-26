using Draggables;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Wires
{
    public class WireConnector : MonoBehaviour
    {
        public GameObject WirePrefab;

        public Vector2 Cell;
        [CanBeNull] private GameWire Wire;

        public void OnClick()
        {
            if (DraggableManager.CurrentDrag != null)
            {
                if (!(DraggableManager.CurrentDrag is GameWire wire))
                {
                    return;
                }

                if (Wire != null)
                {
                    DraggableManager.CurrentDrag = Wire;
                }
                else
                {
                    DraggableManager.CurrentDrag = null;
                }
                
                Wire = wire;
                return;
            }
            if (Wire == null)
            {
                Wire = ((GameObject) PrefabUtility.InstantiatePrefab(WirePrefab)).GetComponent<GameWire>();
                
                Wire.ConnectTo(this);
                Wire.StartDrag();
                DraggableManager.CurrentDrag = Wire;
            } else if (Wire != null && Wire.Connectors.Length == 1)
            {
                Wire = null;
            }
            else
            {
                Wire.RemoveConnection(this);
                Wire.StartDrag();
                DraggableManager.CurrentDrag = Wire;
            }
        }
    }
}
