using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D body;// rigidbody du joueur pour le déplacer (gere la physique)
    public float movespeed;// vitesse de déplacement du joueur
    public float obstaclerayDistance;// distance du raycast pour detecter les obstacles devant le joueur
    public GameObject obstacleRayObject;// objet de départ du raycast pour detecter les obstacles devant le joueur
    public LayerMask layerMask;// quel layer le raycast doit detecter (pour eviter de detecter les autres obstacles)
    private int H;// variables pour stocker le nombre d'Helium dans l'inventaire
    private int N;// variables pour stocker le nombre d'Azote dans l'inventaire
    private int C;// variables pour stocker le nombre de Carbone dans l'inventaire
    private int O;// variables pour stocker le nombre d'Oxygene dans l'inventaire
    public TMP_Text HText;// objet de texte pour afficher le nombre d'Helium collecté
    public TMP_Text NText;// objet de texte pour afficher le nombre d'Azote collecté
    public TMP_Text CText;// objet de texte pour afficher le nombre de Carbone collecté
    public TMP_Text OText;// objet de texte pour afficher le nombre d'Oxygene collecté
    private float Energy;// energie du joueur entre 0 et maxEnergy
    private float maxEnergy = 286f;// energie max (c'est juste la hauteur de la barre d'energie pour faciliter le calcul de la hauteur de la barre)
    public GameObject energyBar;// objet de la barre d'energie
    private float height;// hauteur de la barre d'energie entre 0 et 1
    private float LifeExpectancy = 90f;// en seconodes, temps avant de mourir
    public GameObject deathScreen;// animation de mort du joueur
    private Animator deathScreenAnim;// animator de l'animation de mort du joueur
    public GameObject deathText;// texte de mort du joueur
    private Animator deathTextAnim;// animator du texte de mort du joueur
    public GameObject bouton1;
    private Animator bouton1Anim;
    public GameObject timer;
    public TMP_Text timetextmort;
    private bool isDead = false;// variable pour savoir si le joueur est mort ou pas (pour eviter de lancer l'animation de mort plusieurs fois)
    public GameObject timerObject;
    private bool paused = false;// variable pour savoir si le jeu est en pause ou pas
    public GameObject playerAnim;// objet du joueur pour les animations
    private Animator animateur;// animator du joueur pour les animations de déplacement et autres
    Vector2 playerDirection;





    // Start est appelé avant la première frame update
    void Start()
    {
        // initialisation des variables
        playerDirection = Vector2.zero;
        Energy = maxEnergy;
        H=0;
        N=0;
        C=0;
        O=0;
        deathScreenAnim = deathScreen.GetComponent<Animator>();
        deathTextAnim = deathText.GetComponent<Animator>();
        bouton1Anim = bouton1.GetComponent<Animator>();
        SimpleTimer timer = timerObject.GetComponent<SimpleTimer>();
        animateur = playerAnim.GetComponent<Animator>();
    }






    // Update est appelé une fois par frame
    void Update()
    {
        // ajout de 30% d'energie avec P et retrait de 30% d'energie avec O pour tester la barre d'energie
        if (Input.GetKeyDown(KeyCode.P))
        {
            Energy += maxEnergy * 0.3f;
            Energy = Mathf.Clamp(Energy, 0, maxEnergy);
            Debug.Log("Energy increased by 30%: " + Energy);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Energy -= maxEnergy * 0.3f;
            Energy = Mathf.Clamp(Energy, 0, maxEnergy);
            Debug.Log("Energy decreased by 30%: " + Energy);
        }
    


        // l'energie diminue avec le temp (mort en 90secondes)
        if (!paused && !isDead)
        {
            float energyDecreasePerSecond = maxEnergy / LifeExpectancy;
            Energy -= energyDecreasePerSecond * Time.deltaTime;
            Energy = Mathf.Clamp(Energy, 0, maxEnergy);
            Debug.Log("Energy: " + Energy);

            height = Energy / maxEnergy;
            energyBar.transform.localScale = new Vector3(1, height, 1);
        }



        //mort
        if (Energy <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log("Game Over! Energy depleted.");
            timer.GetComponent<SimpleTimer>().playing = false;
            float gameTime = timer.GetComponent<SimpleTimer>().GetTime();
            int minutes = (int)(gameTime / 60f);
            int seconds = (int)(gameTime % 60f);
            int tenths = (int)(gameTime * 10f) % 10;
            if (minutes == 0)
                    timetextmort.text = "Vous avez survécu pendant : " + string.Format("{0:#0}:{1}", seconds, tenths);
                else
                    timetextmort.text = "Vous avez survécu pendant : " + string.Format("{0:#:}{1:00}:{2}", minutes, seconds, tenths);
            animateur.SetBool("dead", true);
            animateur.SetBool("left", false);
            animateur.SetBool("right", false);
            deathScreenAnim.SetTrigger("mort");
            deathTextAnim.SetTrigger("dead");
            bouton1Anim.SetTrigger("mort");
        }



        //trait d'action devans le joueur
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");


        if (Mathf.Abs(xInput) > 0.2 && !paused)
        {
            body.linearVelocity = new Vector2(xInput * movespeed, body.linearVelocity.y);
        }

        if (Mathf.Abs(yInput) > 0.2 && !paused)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, yInput * movespeed);
        }

        if (xInput > 0.2 && !paused)
        {
            playerDirection = new Vector2(1, 0);
            animateur.SetBool("right", true);
            animateur.SetBool("left", false);
        }
        else if (xInput < -0.2 && !paused)
        {
            playerDirection = new Vector2(-1, 0);
            animateur.SetBool("right", false);
            animateur.SetBool("left", true);
        }
        else if (yInput > 0.2 && !paused)
        {
            playerDirection = new Vector2(0, 1);
        }
        else if (yInput < -0.2 && !paused)
        {
            playerDirection = new Vector2(0, -1);
        }

        RaycastHit2D hitObstacle = Physics2D.Raycast(obstacleRayObject.transform.position, playerDirection, obstaclerayDistance, layerMask);
        if (hitObstacle.collider != null)
        {
            Debug.DrawRay(obstacleRayObject.transform.position, hitObstacle.distance * playerDirection, Color.red);
        }
        else if (hitObstacle.collider == null)
        {
            Debug.DrawRay(obstacleRayObject.transform.position, obstaclerayDistance * playerDirection, Color.green);
        }




        // interraction avec l'objet en face du joueur
        if (Input.GetKeyDown(KeyCode.E))
            {
                onInteract(hitObstacle);
            }



    }




    // fonction d'interraction avec l'objet en face du joueur
    private void onInteract(RaycastHit2D hitObstacle)
    {
        if (hitObstacle.collider != null)
        {
            Debug.Log("Hit an obstacle! Distance: " + hitObstacle.distance + " Object: " + hitObstacle.collider.gameObject.name);

                // interraction avec les éléments collectables (H, N, C, O)
                if (hitObstacle.collider.gameObject.name[0] == 'H')
                {
                    H++;
                    Debug.Log("H collected! Total H: " + H);
                    HText.text = H.ToString();
                }
                else if (hitObstacle.collider.gameObject.name[0] == 'N')
                {
                    N++;
                    Debug.Log("N collected! Total N: " + N);
                    NText.text = N.ToString();
                }
                else if (hitObstacle.collider.gameObject.name[0] == 'C')
                {
                    C++;
                    Debug.Log("C collected! Total C: " + C);
                    CText.text = C.ToString();
                }
                else if (hitObstacle.collider.gameObject.name[0] == 'O')
                {
                    O++;
                    Debug.Log("O collected! Total O: " + O);
                    OText.text = O.ToString();
                }
                else if (hitObstacle.collider.gameObject.name[0] == 'S')
                {
                    Debug.Log("Synthetiseur ouvert! (pas encore implémenté)");
                    // ouvrir le menu du synthetiseur (a faire)
                }
            
            
        }
        // si il n'y a pas d'obstacle en face du joueur
        else if (hitObstacle.collider == null)
        {
            Debug.Log("No obstacle in front of the player.");
        }
    }

    public void PauseGame()
    {
        paused = !paused;
    }
}
