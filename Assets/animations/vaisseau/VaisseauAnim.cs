using UnityEngine;

public class VaisseauAnim : MonoBehaviour
{

    private Animator animator;
    public GameObject menu_anim;
    private Animator menuanimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        menuanimator = menu_anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("part");
            menuanimator.SetTrigger("part");
        }
    }
}