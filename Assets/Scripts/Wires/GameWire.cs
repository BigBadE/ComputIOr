using Draggables;
using UnityEngine;
using UnityEngine.UIElements;
using Util;

namespace Wires
{
    public class GameWire : MonoBehaviour, IDraggable
    {
        private LineRenderer _lineRenderer;

        public WireConnector[] Connectors = { };

        private bool dragging;

        void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void ConnectTo(WireConnector connector)
        {
            WireConnector[] newConnectors = new WireConnector[Connectors.Length + 1];
            Connectors.CopyTo(newConnectors, 0);
            newConnectors[^1] = connector;

            Connectors = newConnectors;

            if (dragging)
            {
                dragging = false;
                _lineRenderer.positionCount -= 2;
            }

            int position = _lineRenderer.positionCount;
            _lineRenderer.positionCount = position + 1;
            _lineRenderer.SetPosition(position, CellConverter.CellToWorldPosition(connector.Cell));
        }

        public void RemoveConnection(WireConnector connector)
        {
            WireConnector[] newConnectors = new WireConnector[Connectors.Length - 1];
            int i = 0;
            foreach (WireConnector wireConnector in Connectors)
            {
                if (wireConnector != connector)
                {
                    newConnectors[i++] = wireConnector;
                }
            }

            Connectors = newConnectors;
        }

        public void StartDrag()
        {
            _lineRenderer.positionCount += 2;
            dragging = true;
        }

        void Update()
        {
            int positions = _lineRenderer.positionCount;
            if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse) && CellConverter.MouseWithinGameArea())
            {
                AddPathToCursor(positions);
                _lineRenderer.positionCount = positions + 2;
                positions += 2;
            }

            if (dragging)
            {
                AddPathToCursor(positions);
            }
        }

        private void AddPathToCursor(int positions)
        {
            Vector2 last = _lineRenderer.GetPosition(positions - 3);
            Vector2 mouse = CellConverter.CellToWorldPosition(CellConverter.GetMouseCell());

            _lineRenderer.SetPosition(positions - 2, new Vector3(last.x, mouse.y, 0));

            _lineRenderer.SetPosition(positions - 1,
                CellConverter.CellToWorldPosition(CellConverter.GetMouseCell()));
        }
    }
}