using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ExitFryingMinigame : MonoBehaviour
{
    public Button yourButton;
	public GameObject canvas;
    public GameObject minigameCanvas;
	public GameObject player;
	public Appliance appliance;
	public PhotonView PV;

    
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
		PV = GetComponent<PhotonView>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);
		appliance = transform.parent.parent.GetComponent<Appliance>();
		minigameCanvas = transform.parent.gameObject;
		//GameEvents.current.assignPoints += appliance.GetComponent<fryingMinigame>().UpdateDishPointsFrying;
	}


    // Update is called once per frame
    void TaskOnClick()
    {
	    

	    if (appliance)
	    {
		    appliance.GetComponent<fryingMinigame>().UpdateDishPointsFrying();

	    }
		PhotonView aV = appliance.GetComponent<PhotonView>();
		PV.RPC("setCanvActive",RpcTarget.All,appliance.GetComponent<PhotonView>().ViewID);
		PV.RPC("setAddedF", RpcTarget.All, aV.ViewID);
		
        //minigameCanvas.gameObject.SetActive(false);
        if (appliance.itemsOnTheAppliance.Count > 0)
        {
            aV.RPC("clearItems", RpcTarget.All, aV.ViewID);


		}
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All,appliance.GetComponent<PhotonView>().ViewID);
		if (appliance.cookedDish)
		{
			appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);
		}

		
		PhotonView	view = appliance.GetComponent<PhotonView>();

        //view.RPC("EnablePushing",RpcTarget.All,view.ViewID);
        PV.RPC("EnablePforAll", RpcTarget.All, view.ViewID);

		PV.RPC("globalDestroy",RpcTarget.All,minigameCanvas.GetPhotonView().ViewID);


		//GameEvents.current.assignPoints -= appliance.GetComponent<fryingMinigame>().UpdateDishPointsFrying;
		//GameEvents.current = null;
		//PV.RPC("setSetF", RpcTarget.All, aV.ViewID);

    }
	[PunRPC]
	void globalDestroy(int viewID)
	{
		Destroy(PhotonView.Find(viewID).gameObject);
	}
    [PunRPC]
    void EnablePforAll(int applID)
    {
		List<int> x = PhotonView.Find(applID).GetComponent<Appliance>().appliancePlayers;


		
		for (int i=0; i < x.Count; i++)
		{
			PhotonView.Find(x[i]).GetComponentInChildren<playerMvmt>().enabled = true;
			PhotonView.Find(x[i]).GetComponentInChildren<PlayerController>().enabled = true;

			//PhotonView.Find(applID).GetComponent<Appliance>().appliancePlayers;
		}

		PhotonView.Find(applID).GetComponent<Appliance>().UIcamera.enabled = false;	
		PhotonView.Find(applID).GetComponent<Appliance>().appliancePlayers.Clear();



    }
	[PunRPC]
	void setAddedF(int viewiD)
    {
		PhotonView.Find(viewiD).GetComponent<Appliance>().added = false;
    }
    [PunRPC]
    private void setSetF(int viewiD)
    {
		Debug.Log("setF?");
		Debug.Log(PhotonView.Find(viewiD).GetComponent<fryingMinigame>().set);
		PhotonView.Find(viewiD).GetComponent<fryingMinigame>().set = false;
    }

    [PunRPC]
    void enableMoving(int viewID)
    {
	    PhotonView.Find(viewID).GetComponentInChildren<playerMvmt>().enabled = true;
    }

    [PunRPC]
    void setCanvActive(int viewID)
    {
	    PhotonView.Find(viewID).GetComponent<Appliance>().canvas.SetActive(true);
    }
  

}