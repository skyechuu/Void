

public class ShipRadar : Item {

	public float radarDistance;
	public int informationQuality;

	public ShipRadar(int id, float radarDistance, int informationQuality, string itemName, int capacityCost, float weight):base(id+14000,itemName,capacityCost,weight){
        this.radarDistance = radarDistance;
        this.informationQuality = informationQuality;
    }

}
