using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D body;// rigidbody du joueur pour le déplacer (gere la physique)
    public SacUI sacui;// objet du sac pour faire spawn les atomes dedans
    public float movespeed;// vitesse de déplacement du joueur
    public float obstaclerayDistance;// distance du raycast pour detecter les obstacles devant le joueur
    public GameObject obstacleRayObject;// objet de départ du raycast pour detecter les obstacles devant le joueur
    public LayerMask layerMask;// quel layer le raycast doit detecter (pour eviter de detecter les autres obstacles)
    public int H;// variables pour stocker le nombre d'Helium dans l'inventaire
    public int N;// variables pour stocker le nombre d'Azote dans l'inventaire
    public int C;// variables pour stocker le nombre de Carbone dans l'inventaire
    public int O;// variables pour stocker le nombre d'Oxygene dans l'inventaire
    public List<Moleculeobject> inv_molecule = new List<Moleculeobject>();
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
    public Synthetiseur synthetiseur;// objet du menu du synthetiseur pour l'ouvrir et le fermer
    private bool isSynthetiseurOpen = false;// variable pour savoir si le menu du synthetiseur est ouvert ou pas (pour eviter de l'ouvrir plusieurs fois)
    public choix_molecule script_choix_molecule;// objet du script de choix de molecule pour choisir la molecule à synthetiser et l'afficher dans les menus
    private Molecule molecule_a_synthetiser;//variable pour stocker la molecule à synthétiser choisie au hazard par le script de choix de molecule
    public GameObject commande_vaisseau_menu;// objet du menu de commande du vaisseau pour le désactiver à la mort du joueur
    public int maxAtomes = 20;
    public int maxMolecules = 10;
    public bool invOpened = false;// variable pour savoir si l'inventaire est ouvert ou pas (pour eviter de l'ouvrir plusieurs fois)
    public GameObject inventoryUI;// objet de l'UI de l'inventaire pour l'ouvrir et le fermer
    public GameObject invbox;// objet de la boite d'inventaire pour faire spawn les molecules dedans
    public GameObject togglePrefab;// objet du toggle pour afficher les molecules à synthétiser dans l'inventaire
    public TMP_Text nameDescrition;
    public TMP_Text moleculeDescrition;
    public bool firstStrart = false;
    public VaisseauAnim vaisseauAnim;
    public MoleculeZone moleculeZone;
    public AudioManager audioManager;
    public GameObject commandetext;
    public Pause pause;
    Vector2 playerDirection;

    // Start est appelé avant la première frame update
    void Start()
    {
        // initialisation des variables
        playerDirection = Vector2.zero;
        Energy = maxEnergy;
        H = 0;
        N = 0;
        C = 0;
        O = 0;
        deathScreenAnim = deathScreen.GetComponent<Animator>();
        deathTextAnim = deathText.GetComponent<Animator>();
        bouton1Anim = bouton1.GetComponent<Animator>();
        SimpleTimer timer = timerObject.GetComponent<SimpleTimer>();
        animateur = playerAnim.GetComponent<Animator>();
        firstStrart = true;
    }

    // Update est appelé une fois par frame
    void Update()
    {
        if (firstStrart)
        {
            molecule_a_synthetiser = script_choix_molecule.MoleculeChoisie(timer.GetComponent<SimpleTimer>().GetTotalSeconds());
            script_choix_molecule.AfficherMolecule(molecule_a_synthetiser);
            firstStrart = false;
        }
        //mort
        if (Energy <= 0 && !isDead)
        {
            isDead = true;
            synthetiseur.CloseSynthetiseur();
            commande_vaisseau_menu.SetActive(false);
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
            animateur.SetBool("idle", false);
            deathScreenAnim.SetTrigger("mort");
            deathTextAnim.SetTrigger("dead");
            bouton1Anim.SetTrigger("mort");
        }
        else if (Energy > 0)
        {



            // l'energie diminue avec le temp (mort en 90secondes)
            if (!paused && !isDead)
            {
                float energyDecreasePerSecond = maxEnergy / LifeExpectancy;
                Energy -= energyDecreasePerSecond * Time.deltaTime;
                Energy = Mathf.Clamp(Energy, 0, maxEnergy);

                height = Energy / maxEnergy;
                energyBar.transform.localScale = new Vector3(1, height, 1);
            }






            if (!isSynthetiseurOpen && !isDead)
            {
                //trait d'action devans le joueur et mouvement
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



                if (Mathf.Abs(xInput) > 0.2 || Mathf.Abs(yInput) > 0.2)
                {
                    animateur.SetBool("walk", true);
                    animateur.SetBool("idle", false);
                }
                else
                {
                    animateur.SetBool("walk", false);
                    animateur.SetBool("idle", true);
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

            // ouverture de l'inventaire
            if (Input.GetKeyDown(KeyCode.I) && !invOpened && !isSynthetiseurOpen)
            {
                invOpened = !invOpened;
                OpenInventory();
            }
            else if (Input.GetKeyDown(KeyCode.I) && invOpened)
            {
                invOpened = !invOpened;
                CloseInventory();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (invOpened)
                {
                    invOpened = !invOpened;
                    CloseInventory();
                }
                else if (isSynthetiseurOpen)
                {
                    isSynthetiseurOpen = !isSynthetiseurOpen;
                    CloseSynth();
                }
                else
                {
                    pause.TogglePause();
                }
            }
        }


    }
    public void OpenSynth()
    {
        if (invOpened)
        {
            invOpened = !invOpened;
            CloseInventory();
        }
        audioManager.Play(AudioManager.SoundType.OpenSynth);
        synthetiseur.OpenSynthetiseur();
    }
    public void CloseSynth()
    {
        audioManager.Play(AudioManager.SoundType.CloseSynth);
        synthetiseur.CloseSynthetiseur();
        Invoke("TextReload", 0.1f);
    }
    public void ChangeMolecule()
    {
        molecule_a_synthetiser = script_choix_molecule.MoleculeChoisie(timer.GetComponent<SimpleTimer>().GetTotalSeconds());
        script_choix_molecule.AfficherMolecule(molecule_a_synthetiser);
    }

    // fonction d'interraction avec l'objet en face du joueur
    private void onInteract(RaycastHit2D hitObstacle)
    {
        if (hitObstacle.collider != null)
        {

            // interraction avec les éléments collectables (H, N, C, O)
            if (hitObstacle.collider.gameObject.name[0] == 'H' && H < maxAtomes)
            {
                audioManager.Play(AudioManager.SoundType.Pickup);
                H++;
                HText.text = H.ToString();
                sacui.Refresh(H, N, C, O);
            }
            else if (hitObstacle.collider.gameObject.name[0] == 'H' && H <= maxAtomes)
            {
                audioManager.Play(AudioManager.SoundType.NotPickup);
            }
            else if (hitObstacle.collider.gameObject.name[0] == 'N' && N < maxAtomes)
            {
                audioManager.Play(AudioManager.SoundType.Pickup);
                N++;
                NText.text = N.ToString();
                sacui.Refresh(H, N, C, O);
            }
            else if (hitObstacle.collider.gameObject.name[0] == 'N' && H <= maxAtomes)
            {
                audioManager.Play(AudioManager.SoundType.NotPickup);
            }
            else if (hitObstacle.collider.gameObject.name[0] == 'C' && C < maxAtomes)
            {
                audioManager.Play(AudioManager.SoundType.Pickup);
                C++;
                CText.text = C.ToString();
                sacui.Refresh(H, N, C, O);
            }
            else if (hitObstacle.collider.gameObject.name[0] == 'C' && H <= maxAtomes)
            {
                audioManager.Play(AudioManager.SoundType.NotPickup);
            }
            else if (hitObstacle.collider.gameObject.name[0] == 'O' && O < maxAtomes)
            {
                audioManager.Play(AudioManager.SoundType.Pickup);
                O++;
                OText.text = O.ToString();
                sacui.Refresh(H, N, C, O);
            }
            else if (hitObstacle.collider.gameObject.name[0] == 'O' && H <= maxAtomes)
            {
                audioManager.Play(AudioManager.SoundType.NotPickup);
            }
            else if (hitObstacle.collider.gameObject.name[0] == 'S')
            {
                isSynthetiseurOpen = !isSynthetiseurOpen;
                if (isSynthetiseurOpen)
                {
                    OpenSynth();
                }
                else
                {
                    CloseSynth();
                }
            }
            else if (hitObstacle.collider.gameObject.name[0] == 'R')
            {
                foreach (Moleculeobject molecule in inv_molecule)
                {
                    if (molecule.GetName() == molecule_a_synthetiser.moleculeName)
                    {
                        audioManager.Play(AudioManager.SoundType.SpaceShip);
                        Invoke("ChangeMolecule", 0.3f);
                        Energy += maxEnergy * 0.3f;
                        vaisseauAnim.LanceAnim();
                        inv_molecule.Remove(molecule);
                        Destroy(molecule);
                        moleculeZone.RemoveMolecule(molecule.moleculeinfo);
                        return;
                    }
                }
                Debug.Log("Molecule non trouvée dans l'inventaire");
                audioManager.Play(AudioManager.SoundType.Wrong);
            }
        }
    }

    public void TextReload()
    {
        commandetext.SetActive(false);
        commandetext.SetActive(true);
    }

    public void PauseGame()
    {
        paused = !paused;
    }
    public int GetH() => H;
    public int GetN() => N;
    public int GetC() => C;
    public int GetO() => O;

    public void AddH()
    {
        H++;
        HText.text = H.ToString();
    }

    public void AddN()
    {
        N++;
        NText.text = N.ToString();
    }

    public void AddC()
    {
        C++;
        CText.text = C.ToString();
    }

    public void AddO()
    {
        O++;
        OText.text = O.ToString();
    }

    public void RemoveH()
    {
        H--;
        HText.text = H.ToString();
    }

    public void RemoveN()
    {
        N--;
        NText.text = N.ToString();
    }
    public void RemoveC()
    {
        C--;
        CText.text = C.ToString();
    }
    public void RemoveO()
    {
        O--;
        OText.text = O.ToString();
    }

    void UpdateSacUI()
    {
        sacui.Refresh(H, N, C, O);
    }

    public void AddMolecule(Moleculeobject molecule)
    {
        if (!(inv_molecule.Contains(molecule))) inv_molecule.Add(molecule);

        if (inv_molecule.Count > maxMolecules)
        {
            inv_molecule.RemoveAt(0);
        }
    }

    public void RemoveMolecule(Moleculeobject molecule)
    {
        if (inv_molecule.Contains(molecule))
        {
            Debug.Log("Removing molecule: " + molecule.GetName());
            inv_molecule.Remove(molecule);
            Destroy(molecule);

            moleculeZone.RemoveMolecule(molecule.moleculeinfo);
        }
        else
        {
            Debug.Log("Attempted to remove molecule that is not in inventory: " + molecule.GetName());
        }
    }

    public void MoleculeDisapear(Moleculeobject molecule)
    {
        if (inv_molecule.Contains(molecule))
        {
            Debug.Log("Removing molecule: " + molecule.GetName());
            inv_molecule.Remove(molecule);
        }
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);

        // Delete old toggles
        foreach (Transform child in invbox.transform)
        {
            Destroy(child.gameObject);
        }

        // Create one toggle per molecule
        foreach (Moleculeobject molecule in inv_molecule)
        {
            GameObject toggleObject = Instantiate(togglePrefab, invbox.transform);

            Toggle toggle = toggleObject.GetComponent<Toggle>();

            toggle.group = invbox.GetComponent<ToggleGroup>();

            toggle.isOn = false;

            // Set the visible text of the toggle
            TMP_Text toggleText = toggleObject.GetComponentInChildren<TMP_Text>();
            toggleText.text = molecule.GetChemicalFormula();

            // Add click event
            toggle.onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    nameDescrition.text = molecule.GetName() + "(" + molecule.GetChemicalFormula() + ")";
                    moleculeDescrition.text = molecule.GetDescription();
                }
            });
        }
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }
}
