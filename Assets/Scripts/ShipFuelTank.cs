
[System.Serializable]
public class ShipFuelTank : Item {
    public string fuelType;
    public float fuelCapacity;

    public ShipFuelTank(int id, string fuelType, float fuelCapacity, string itemName, int capacityCost, float weight):base(id+10000,itemName,capacityCost,weight){
        this.fuelType = fuelType;
        this.fuelCapacity = fuelCapacity;
    }
}