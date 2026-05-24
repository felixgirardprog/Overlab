using UnityEngine;
using System.Collections;

public class VaisseauAnim : MonoBehaviour
{
    private Animator animator;
    public GameObject menu_anim;
    private Animator menuanimator;
    public Animator menutextone;
    public Animator menutexttwo;

    public float chanceShipA = 20f;
    public bool dodge = false;

    public Sprite shipA;
    public Sprite shipB;
    public Sprite shipC;
    public Sprite shipD;
    public Sprite shipE;
    public Sprite shipF;
    public Sprite shipG;
    public Sprite shipH;
    public Sprite shipI;
    public Sprite shipJ;
    public Sprite shipK;


    private Sprite shipSpace;

    public float partDuration = 0.5f;
    public float arriveDuration = 0.5f;

    private SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        menuanimator = menu_anim.GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        // IMPORTANT : choisir un sprite dès le début
        choix();
        
    }
    IEnumerator SwitchShip()
    {
        animator.SetTrigger("part");
        menuanimator.SetTrigger("part");
        menutextone.SetTrigger("part");
        menutexttwo.SetTrigger("part");

        // Attendre la durée réelle de l'animation
        yield return new WaitForSeconds(partDuration);

        // Choisir le nouveau sprite
        choix();
    }

    void choix()
    {
        if (dodge)
        {
            shipSpace = shipA;
        }
        else
        {
            Sprite[] ships = { shipB, shipC, shipD, shipE, shipF, shipG,  shipH, shipI, shipJ, shipK};
            int index = Random.Range(0, ships.Length);
            shipSpace = ships[index];
        }
    }

    public void ChangeShipSprite()
    {
        if (shipSpace == null)
        {
            Debug.LogError("ERREUR : shipSpace est NULL !");
            return;
        }

        sr.sprite = shipSpace;
    }

    public void LanceAnim()
    {
        float rand = Random.Range(0f, 100f);
        dodge = rand < chanceShipA;

        StartCoroutine(SwitchShip());
    }
}
