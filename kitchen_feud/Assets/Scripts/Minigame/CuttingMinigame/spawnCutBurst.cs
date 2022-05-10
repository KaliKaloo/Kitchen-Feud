using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class spawnCutBurst : MonoBehaviour
{
    public GameObject burstPrefab;

    public void BurstEffect(GameObject SpawnParent){
        Image img = SpawnParent.GetComponent<Image>();
        Color colorA = AverageColorFromTexture(img.sprite.texture);
        
        GameObject newObject = Instantiate(burstPrefab, SpawnParent.transform.position,SpawnParent.transform.rotation);
        newObject.transform.position = new Vector3(newObject.transform.position.x,newObject.transform.position.y,0 );
        
        ParticleSystem ps = newObject.GetComponent<ParticleSystem>();

        foreach (Transform child in newObject.transform){
            ParticleSystem psChild = child.GetComponent<ParticleSystem>();
            Debug.Log(psChild.name);
            var main = psChild.main;
            colorA.a = 1f;
            main.startColor = new ParticleSystem.MinMaxGradient(colorA);
        }
        var main2 = ps.main;
        colorA.a = 1f;
        main2.startColor = new ParticleSystem.MinMaxGradient(colorA);
        
        Destroy(newObject,1f);
    }

    Color AverageColorFromTexture(Texture2D tex)
    {
        Color32[] texColors = tex.GetPixels32();
        int total = texColors.Length;
        float r = 0;
        float g = 0;
        float b = 0;
 
        for(int i = 0; i < total; i++)
        {
            r += texColors[i].r;
            g += texColors[i].g;
            b += texColors[i].b;
        }
    
        Color color = new Color32((byte)(r / total) , (byte)(g / total) , (byte)(b / total) , 0);
        return color;
 
    }     
}
