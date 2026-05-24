using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class DragMolecule : DragAndDrop, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform zoneRectTransform;
    public PlayerMovement player;
    public MoleculeZone zonemolecule;
    public GameObject itself; // Référence à l'objet lui-même pour pouvoir le détruire


    public override void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 255); // couleur par défaut

        if (zoneRectTransform != null)
        {
            // On utilise la position écran de l'atome (rectTransform)
            if (RectTransformUtility.RectangleContainsScreenPoint(zoneRectTransform, rectTransform.position, eventData.pressEventCamera))
            {
                Moleculeobject molecule = GetComponent<Moleculeobject>();
                player.AddMolecule(molecule);
            }
            else
            {
                player.MoleculeDisapear(GetComponent<Moleculeobject>());
                //zonemolecule.RemoveMolecule(GetComponent<Moleculeobject>().moleculeinfo);
                Destroy(itself);
            }
        }
        audioManager.Play(AudioManager.SoundType.UnClick);
    }
}
