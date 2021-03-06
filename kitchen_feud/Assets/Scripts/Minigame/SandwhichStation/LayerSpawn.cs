using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LayerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public SpriteAtlas imgAtlas;
    public Camera UICamera;
    
    private int chosenY;
    private int chosenX;
    private float gridX = 1;
    private float gridY = 4;
    
    public void StartSpawn(List<string> idList)
    {
        chosenY = (int)(2f * UICamera.orthographicSize);
        chosenX = (int)(chosenY  * UICamera.aspect);
        float spacing = chosenY/6;
   

        for (int y = 1; y <= gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                GameObject myParentObject = gameObject;
                Vector3 newPosition = myParentObject.transform.position;
                newPosition.z = 0;
                myParentObject.transform.position = newPosition;

                //setting the spacing for the sandwich layers to spawn. the most bottom one has a different spacing so that it is on the counter and above intstrucions.
                if(y == 1){
                    spacing = chosenY/5;}
                else{
                    spacing = chosenY/7;}

                Vector3 pos = new Vector3(chosenX/2, (y * spacing), 0) ;
                GameObject newObject = Instantiate(prefab, pos, Quaternion.identity);
                newObject.transform.SetParent(myParentObject.transform);

                newObject.GetComponent<SandwichMove>().LayerID = idList[y-1];
                newObject.GetComponent<SandwichMove>().speed = Random.Range(550, 730);
                newObject.GetComponent<SandwichMove>().imgAtlas = imgAtlas;
                transform.localPosition = new Vector3(0,0,0);
            }
        }
        
    }
    
    public void DestroyLayers(){
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
