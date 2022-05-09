using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using ExitGames.Client.Photon.StructWrapping;

public class OutlineEffect : MonoBehaviour
{
    public Material mat;
    public float thickness = 1.03f;
    [ColorUsage(true, true)]
    public Color colorOutline;
    private Renderer rend;
    public GameObject outlineObjectPrefab;
    public GameObject outlineObject;
    public PhotonView PV;
    public GameObject loadingCanvas;
    private bool initialised = false;
    
    void Start()
    {
        PV = GetComponent<PhotonView>();
        loadingCanvas = GameSetup.GS.loadingCanvas;

    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && !loadingCanvas.activeSelf & initialised == false)
        {
            outlineObject = PhotonNetwork.Instantiate(Path.Combine("Appliances", outlineObjectPrefab.name),
                transform.position, gameObject.transform.rotation);
            PV.RPC("glowSettings",RpcTarget.All,outlineObject.GetPhotonView().ViewID,PV.ViewID);
            initialised = true;

        }
    }

    public void startGlowing(){
        if(outlineObject){
            outlineObject.GetComponent<Renderer>().enabled = true;
        }
    }
    
    public void stopGlowing(){
        if(outlineObject){
            outlineObject.GetComponent<Renderer>().enabled = false;
        }
    }

    [PunRPC]
    void glowSettings(int glowID,int thisID)
    {
        GameObject outline = PhotonView.Find(glowID).gameObject;
        outline.transform.SetParent(PhotonView.Find(thisID).transform);
        Renderer rendR = outline.GetComponent<Renderer>();
        if (!PhotonView.Find(thisID).GetComponent<OutlineEffect>().outlineObject)
        {
            PhotonView.Find(thisID).GetComponent<OutlineEffect>().outlineObject = outline;
        }
        rendR.material = mat;
        rendR.material.SetFloat("_Thickness", thickness);
        rendR.material.SetColor("_OutlineColor", colorOutline);
        rendR.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        rendR.enabled = false;
        outline.GetComponent<Collider>().enabled = false;


    }
}