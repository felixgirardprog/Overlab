using UnityEngine;

public class Atome : MonoBehaviour
{
    public string atomeType; // "H", "O", "C", "N"

    public void Delete()
    {
        Destroy(gameObject);
    }
}