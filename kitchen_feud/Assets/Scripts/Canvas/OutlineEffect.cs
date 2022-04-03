using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class OutlineEffect : MonoBehaviour
{
    public Material mat;
    public float thickness = 1.03f;
    [ColorUsage(true, true)]
    public Color colorOutline;
    private Renderer rend;
    public GameObject outlineObjectPrefab;
    public GameObject outlineObject;
    // Start is called before the first frame update
    
 void Start()
    {
        outlineObject = PhotonNetwork.Instantiate(Path.Combine("Appliances",outlineObjectPrefab.name), transform.position,Quaternion.identity);
        outlineObject.transform.SetParent(gameObject.transform);
        outlineObject.transform.localScale = new Vector3(1, 1, 1);

        Renderer rend = outlineObject.GetComponent<Renderer>();
        rend.material = mat;
        rend.material.SetFloat("_Thickness", thickness);
        rend.material.SetColor("_OutlineColor", colorOutline);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        rend.enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        //outlineObject.GetComponentInParent<OutlineEffect>().enabled = false;
        this.rend = rend;
        rend.enabled = true;
    
    }

    // public void startGlowing()
    // {
    //     o
    // }

    public void destroyClone(){

       rend.enabled = false;
    }

}
