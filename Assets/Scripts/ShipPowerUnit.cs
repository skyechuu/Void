
public class ShipPowerUnit : Item {

	public int power;
	public Element element;

	public ShipPowerUnit(int id, int power, Element element, string itemName, int capacityCost, float weight):base(id+13000,itemName,capacityCost,weight){
        this.power = power;
        this.element = element;
    }
}
