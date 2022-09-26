using JetBrains.Annotations;

namespace Draggables
{
    public static class DraggableManager
    {
        [CanBeNull] public static IDraggable CurrentDrag;
    }
}