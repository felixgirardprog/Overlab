using UnityEngine;

public class CrerMolecule : MonoBehaviour
{
    public SynthZone synthZone;
    public string molecule_folder_path = "Molecules/";
    private bool MoleculeExistante = false;
    public GameObject carre_synthese;
    public GameObject prefab_molecule;
    public PlayerMovement Player;
    public MoleculeZone zoneMolecule;
    public AudioManager AudioManager;
    public void CreateMolecule()
    {
        InventaireSynthetiseur currentAtoms = gameObject.AddComponent<InventaireSynthetiseur>();

        foreach (Atome atome in synthZone.atomsInside)
        {
            switch(atome.atomeType)
            {
                case "H": currentAtoms.Hydrogen++; break;
                case "O": currentAtoms.Oxygene++; break;
                case "C": currentAtoms.Carbone++; break;
                case "N": currentAtoms.Azote++; break;
            }
        }
        synthZone.ClearAtoms(); // Vide la liste des atomes dans la zone pour préparer la prochaine synthèse

        Molecule currentmolecule = FindMatchingMolecule(currentAtoms);
        zoneMolecule.SpawnMolecul(currentmolecule);
        if (MoleculeExistante)
        {
            AudioManager.Play(AudioManager.SoundType.Create);
            Player.molecouille++;
        }
        else
        {
            AudioManager.Play(AudioManager.SoundType.NotCreate);
        }
        currentAtoms.clear(); // Réinitialise les compteurs d'atomes pour la prochaine synthèse
    }

    public Molecule FindMatchingMolecule(InventaireSynthetiseur currentAtoms)
    {
        MoleculeExistante = false;
        foreach (Molecule molecule in Resources.LoadAll<Molecule>(molecule_folder_path + (currentAtoms.Hydrogen + currentAtoms.Oxygene + currentAtoms.Carbone + currentAtoms.Azote).ToString()))
        {
            if (currentAtoms.Hydrogen == molecule.hydrogen &&
                currentAtoms.Oxygene == molecule.oxygene &&
                currentAtoms.Carbone == molecule.carbone &&
                currentAtoms.Azote == molecule.azote)
            {
                MoleculeExistante = true;
                return molecule;
            }
        }


        return null;
    }
}
