//Food items are the 3D objects on front face of cards

using UnityEngine;

public class FoodScript : MonoBehaviour
{
    void Start()
    {
        gameObject.AddComponent<ItemRotationScript>();
    }
}
