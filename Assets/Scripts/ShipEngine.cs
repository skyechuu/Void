

public class ShipEngine : Item {

	public float maxSpeed;
	public float acceleration;

	public ShipEngine(int id, float maxSpeed, float acceleration, string itemName, int capacityCost, float weight):base(id+11000,itemName,capacityCost,weight){
        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
    }
	
}
