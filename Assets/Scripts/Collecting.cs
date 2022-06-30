using UnityEngine;

public class Collecting : MonoBehaviour
{
    public bool isCollected;
    public static int price;
    public CollectableTypes type;

    public enum CollectableTypes
    {
        //Score values for objects.
        Money = 1,
        Gold = 2,
        Diamond = 3
    }
}
