using UnityEngine;

public class MoleculeZone : MonoBehaviour
{
    private Transform rectransform;
    public GameObject moleculePrefab;
    public GameObject parentobject;

    public void SpawnMolecul(Molecule properties)
    {
        if (rectransform == null)
        {
            rectransform = transform;
        }

        GameObject moleculeInstance = Instantiate(moleculePrefab, rectransform.position, Quaternion.identity);

        Moleculeobject moleculeObject = moleculeInstance.GetComponent<Moleculeobject>();

        moleculeObject.moleculeinfo = properties;
    }
}