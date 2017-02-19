using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ShipGenerator : MonoBehaviour {

    public GameObject pixelPrefab;
    private int shipSize;                    //total pixel
    [Range(8, 24)]
    public int shipLayerSize;               //total number of layers
    [Range(1, 10)]
    public int maxShipWideness;             //max wideness of ship layers
    [Range(1, 10)]
    public int minShipWideness;             //min wideness of ship layers
    public Color cockpitColor;              //color of cockpit pixel

    private int currentLayerPixelSize, prevLayerPixelSize;
    private int cockpitLayer;
    private GameObject lastShipPart;

    [SerializeField]
    public List<ShipBodyMaterial> shipMatrix;


    public void GenerateRandomShip(){

        int maxWidth=0;
        if (shipLayerSize == 0)
            shipLayerSize = Random.Range(8, 24);

        shipMatrix = new List<ShipBodyMaterial>();

        cockpitLayer = Random.Range(0, shipLayerSize);
        for (int i=0; i<shipLayerSize; i++)
        {
            currentLayerPixelSize = Random.Range(minShipWideness,maxShipWideness);
            if(currentLayerPixelSize>maxWidth) 
                maxWidth=currentLayerPixelSize;
            for (int j=currentLayerPixelSize/2*(-1); j<(currentLayerPixelSize/2)+1; j++)
            {
                if (currentLayerPixelSize%2==0)
                {
                    if (j != 0)
                    {
                        GenerateShipBody(j, i);
                        //float ppu = 10; //pixel sprite's ppu, 4=1; 40=10
                        //float calculatedppu = ppu / 4;
                        //GameObject go = Instantiate(pixelPrefab) as GameObject;
                        //go.transform.parent = transform;
                        //go.transform.localPosition = new Vector3(x / calculatedppu, y * (-1) / calculatedppu, 0f);
                        //shipSize++;
                        //shipMatrix[y].Add(go);
                        //lastShipPart = go;
                    }
                    else
                    {
                        if (j == 0 && i == cockpitLayer)
                        {
                            GenerateShipBody(j, i);
                            lastShipPart.transform.GetComponent<SpriteRenderer>().color = cockpitColor;
                        }
                    }
                }
                else
                {
                    GenerateShipBody(j, i);
                    if (j == 0 && i == cockpitLayer)
                    {
                        lastShipPart.transform.GetComponent<SpriteRenderer>().color = cockpitColor;
                    }
                }
            }

            prevLayerPixelSize = currentLayerPixelSize;
        }

        transform.localPosition = new Vector3(0, (shipLayerSize*0.2f)-0.2f, 0);
        transform.parent.GetComponent<CapsuleCollider2D>().size = new Vector2(maxWidth*0.4f,shipLayerSize*0.4f);
        Debug.Log("Random Ship Created. Size: "+shipMatrix.Count+". Attaching to : "+transform.parent.name);
        transform.parent.GetComponent<Ship>().setShipMatrix(shipMatrix);
    }


    void GenerateShipBody(int x, int y)
    {
        float ppu = 10; //pixel sprite's ppu, 4=1; 40=10
        float calculatedppu = ppu / 4;
        GameObject go = Instantiate(pixelPrefab) as GameObject;
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3(x / calculatedppu, y * (-1) / calculatedppu, 0f);
        shipSize++;
        go.GetComponent<ShipMaterialHolder>().cell = go.transform.position;
        go.GetComponent<SpriteRenderer>().sprite = Database.selfDb.GetMaterial(0);
        shipMatrix.Add(go.GetComponent<ShipMaterialHolder>().getMaterial());
        lastShipPart = go;
    }

    public void BuildShip(List<ShipBodyMaterial> matrix)
    {
        if(matrix.Count>0){
            Vector2 colliderSize=Vector2.one;
            float xMin=0, xMax=0, yMin=0, yMax=0;
            foreach(Transform t in transform){
                Destroy(t.gameObject);
            }
            foreach(ShipBodyMaterial sbm in matrix){
                GameObject go = Instantiate(pixelPrefab) as GameObject;
                go.transform.parent = transform;
                go.transform.localPosition = sbm.cell + new Vector3(-4,4,0);
                if(go.transform.localPosition.x > xMax)
                    xMax = go.transform.localPosition.x;
                if(go.transform.localPosition.x < xMin)
                    xMin = go.transform.localPosition.x;
                if(go.transform.localPosition.y > yMax)
                    yMax = go.transform.localPosition.y;
                if(go.transform.localPosition.y < yMin)
                    yMin = go.transform.localPosition.y;
                colliderSize = new Vector2 ( xMax - xMin, yMax - yMin);
                transform.parent.GetComponent<CapsuleCollider2D>().size = colliderSize+(Vector2.one*0.4f);
                if(transform.parent.GetComponent<CapsuleCollider2D>().size.x > transform.parent.GetComponent<CapsuleCollider2D>().size.y)
                    transform.parent.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
                transform.localPosition = new Vector3(0, ((yMax-yMin)/0.4f*-0.2f) - (yMin), 0);
                shipSize++;
                go.GetComponent<ShipMaterialHolder>().cell = sbm.cell;
                shipMatrix = matrix;
                lastShipPart = go;
            }
        }
        else{
            GenerateRandomShip();
        }

    }

    public void PreviewShip(List<ShipBodyMaterial> matrix)
    {
        if(matrix.Count>0){
            float xMin=0, xMax=0, yMin=0, yMax=0;
            foreach(Transform t in transform){
                Destroy(t.gameObject);
            }
            foreach(ShipBodyMaterial sbm in matrix){
                GameObject go = Instantiate(pixelPrefab) as GameObject;
                go.transform.parent = transform;
                go.transform.localPosition = sbm.cell + new Vector3(-4,4,0);
                if(go.transform.localPosition.x > xMax)
                    xMax = go.transform.localPosition.x;
                if(go.transform.localPosition.x < xMin)
                    xMin = go.transform.localPosition.x;
                if(go.transform.localPosition.y > yMax)
                    yMax = go.transform.localPosition.y;
                if(go.transform.localPosition.y < yMin)
                    yMin = go.transform.localPosition.y;
                transform.localPosition = new Vector3(0, ((yMax-yMin)/0.4f*-0.2f) - (yMin), 0);
                shipSize++;
                go.GetComponent<ShipMaterialHolder>().cell = sbm.cell;
                shipMatrix = matrix;
                lastShipPart = go;
            }
        }
    }



    


}

