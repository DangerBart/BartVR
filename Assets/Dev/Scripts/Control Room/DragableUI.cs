using UnityEngine;
using UnityEngine.EventSystems;

public class DragableUI : UIBehaviour, IDragHandler
{
    /// <summary>
    /// The area in which we are able to move the dragObject around.
    /// if null: canvas is used
    /// </summary>
    public RectTransform dragArea;
    public RectTransform draggable;

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        draggable.transform.position += (Vector3)eventData.delta;
    }

    #endregion
}
