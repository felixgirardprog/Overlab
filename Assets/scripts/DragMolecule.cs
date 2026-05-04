using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class DragMolecule : DragAndDrop, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform zoneRectTransform;
    public PlayerMovement player;

    public override void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 255); // couleur par défaut

        if (zoneRectTransform != null)
        {
            // On utilise la position écran de l'atome (rectTransform)
            if (RectTransformUtility.RectangleContainsScreenPoint(zoneRectTransform, rectTransform.position, eventData.pressEventCamera))
            {
                player.AddMolecule();
            }
        }
    }
}
