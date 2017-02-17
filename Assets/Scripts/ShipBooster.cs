

public class ShipBooster : Item {

	public float accelerationBooster;
	public float boostMultiplier;

	public ShipBooster(int id, float accelerationBooster, float boostMultiplier, string itemName, int capacityCost, float weight):base(id+12000,itemName,capacityCost,weight){
        this.accelerationBooster = accelerationBooster;
        this.boostMultiplier = boostMultiplier;
    }
	
}
