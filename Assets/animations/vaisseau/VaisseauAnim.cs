using UnityEngine;
using System.Collections;


public class VaisseauAnim : MonoBehaviour
{
    private Animator animator;
    public GameObject menu_anim;
    private Animator menuanimator;

    public float chanceShipA = 50f;
    public bool dodge = false;

    public Sprite shipA;
    public Sprite shipB;
    public float partDuration = 0.5f;   // durée de l'animation part
    public float arriveDuration = 0.5f; // durée de l'animation arrive



    private SpriteRenderer sr;

    
    void Start()
{
    animator = GetComponent<Animator>();
    menuanimator = menu_anim.GetComponent<Animator>();
    sr = GetComponent<SpriteRenderer>();

}



    void Update()
{
    if (Input.GetKeyDown(KeyCode.V))
    {
        float rand = Random.Range(0f, 100f);
        dodge = rand < chanceShipA;

        StartCoroutine(SwitchShip());
    }
}

    IEnumerator SwitchShip()
{
    // 1. Jouer l'animation de départ avec l'ancien sprite
    animator.SetTrigger("part");
    menuanimator.SetTrigger("part");

    // 2. Attendre la fin de l'animation de départ
    yield return new WaitForSeconds(partDuration);

    // 3. Changer le sprite AVANT l'arrivée
    sr.sprite = dodge ? shipA : shipB;

    // 4. Jouer l'animation d'arrivée
    animator.SetTrigger("arrive");
    menuanimator.SetTrigger("arrive");

    // 5. (optionnel) attendre la fin de l'arrivée
    yield return new WaitForSeconds(arriveDuration);
}

}
