using UnityEngine;

[CreateAssetMenu(menuName = "Items/Coin")]
public class CoinItem : ItemData
{
    public int value = 1;

    public override bool OnCollect(GameObject collector)    //player is the collector
    {
        //collector.GetComponent<Inventory>().AddCoins(value);

        Debug.Log("coin collected | value: " + value);
        return true;
    }
}

[CreateAssetMenu(menuName = "Items/Health Potion")]
public class PotionItem : ItemData
{
    public int healAmount = 25;

    public override bool OnCollect(GameObject collector)
    {
        return collector.GetComponent<PlayerHealth>().Heal(healAmount);
    }
}

