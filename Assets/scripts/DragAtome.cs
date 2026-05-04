using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAtome : DragAndDrop, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform zoneRectTransform;
    public SynthZone synthZone;

    public override void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 255); // couleur par défaut

        // Détection manuelle si l'atome est dans la zone
        if (zoneRectTransform != null)
        {
            // On utilise la position écran de l'atome (rectTransform)
            if (RectTransformUtility.RectangleContainsScreenPoint(zoneRectTransform, rectTransform.position, eventData.pressEventCamera))
            {
                synthZone.AjoutAtome(GetComponent<Atome>()); // Ajoute l'atome à la liste de la zone

            }
        }
    }
}
