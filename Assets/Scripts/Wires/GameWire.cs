using Draggables;
using UnityEngine;
using UnityEngine.UIElements;
using Util;

namespace Wires
{
    public class GameWire : MonoBehaviour, IDraggable
    {
        private LineRenderer _lineRenderer;
        
        public WireConnector[] Connectors = {};

        private bool dragging;
        
        void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }
        
        public void ConnectTo(WireConnector connector)
        {
            WireConnector[] newConnectors = new WireConnector[Connectors.Length + 1];
            Connectors.CopyTo(newConnectors, 0);
            newConnectors[newConnectors.Length - 1] = connector;
            
            Connectors = newConnectors;

            int position = _lineRenderer.positionCount;
            _lineRenderer.positionCount = position + 1;
            _lineRenderer.SetPosition(position, CellConverter.CellToWorldPosition(connector.Cell));

            if (dragging)
            {
                dragging = false;
                _lineRenderer.positionCount -= 1;
            }
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
            _lineRenderer.positionCount += 1;
            dragging = transform;
        }
        
        void Update()
        {
            if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse) && CellConverter.MouseWithinGameArea())
            {
                int positions = _lineRenderer.positionCount;
                _lineRenderer.SetPosition(positions - 1, 
                    CellConverter.CellToWorldPosition(CellConverter.GetMouseCell()));
                _lineRenderer.positionCount = positions + 1;
            }

            if (dragging)
            {
                _lineRenderer.SetPosition(_lineRenderer.positionCount-1, 
                    CellConverter.CellToWorldPosition(CellConverter.GetMouseCell()));
            }
        }
    }
}