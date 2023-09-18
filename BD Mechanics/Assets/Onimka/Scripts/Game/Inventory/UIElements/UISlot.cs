using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        var othersItemTransform = eventData.pointerDrag.transform;
        othersItemTransform.SetParent(transform);
        othersItemTransform.localPosition = Vector3.zero;
    }

   
}
