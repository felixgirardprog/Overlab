using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private RectTransform _rectTransform;
    protected RectTransform rectTransform
    {
        get => _rectTransform;
        set => _rectTransform = value;
    }
    private Image _image;
    protected Image image
    {
        get => _image;
        set => _image = value;
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition; // déplace l'objet en fonction du déplacement de la souris
    }


    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 170); // couleur plus transparente pour indiquer que l'objet est en train d'être déplacé
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 255); // couleur par défaut
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
