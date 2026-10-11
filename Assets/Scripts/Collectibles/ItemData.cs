using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;

    // Return true if the pickup should be consumed
    public abstract bool OnCollect(GameObject collector);
}
