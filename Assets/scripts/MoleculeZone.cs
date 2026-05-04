using UnityEngine;

public class MoleculeZone : MonoBehaviour
{
    private Transform rectransform;
    public GameObject moleculePrefab;
    public Transform parentobject;

    public void SpawnMolecul(Molecule properties)
    {
        if (rectransform == null)
        {
            rectransform = transform;
        }

        if (properties != null)
        {
            GameObject moleculeInstance = Instantiate(moleculePrefab, rectransform.position, Quaternion.identity, parentobject);
            moleculeInstance.transform.localScale = new Vector3(2f, 2f, 2f);
            moleculeInstance.name = properties.moleculeName;

            Moleculeobject moleculeObject = moleculeInstance.GetComponent<Moleculeobject>();

            moleculeObject.moleculeinfo = properties;
        }
    }
}