using System;

[Serializable]
public abstract class Item {

    public int id;
    public string itemName;
    public int capacityCost;
    public float weight;


    public Item(int id, string itemName, int capacityCost, float weight){
        this.id = id;
        this.itemName = itemName;
        this.capacityCost = capacityCost;
        this.weight = weight;
    }

}
