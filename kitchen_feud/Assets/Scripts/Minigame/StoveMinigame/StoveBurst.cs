using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurst : MonoBehaviour
{

    public void OnTriggerExit2D(Collider2D target)
    { 
        if (target.tag.ToString() == "Pot")
        {
            BurstEffect(gameObject);
        } 
    }


    public GameObject burstPrefab;

    public void BurstEffect(GameObject SpawnParent){
        GameObject newObject = Instantiate(burstPrefab, SpawnParent.transform.position,SpawnParent.transform.rotation);
        newObject.transform.position = new Vector3(newObject.transform.position.x,newObject.transform.position.y,0 );
        
        newObject.transform.SetParent(transform.parent.parent);

        Destroy(newObject,1f);
    }
}
