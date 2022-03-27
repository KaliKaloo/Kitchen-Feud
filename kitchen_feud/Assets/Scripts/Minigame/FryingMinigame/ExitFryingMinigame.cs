using System.Collections;
using System.Collections.Generic;
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
		canvas = GameObject.Find("Canvas");
		appliance = transform.parent.parent.GetComponent<Appliance>();
		minigameCanvas = transform.parent.gameObject;
		//GameEvents.current.assignPoints += appliance.GetComponent<fryingMinigame>().UpdateDishPointsFrying;
	}

    // Update is called once per frame
    void TaskOnClick()
    {
		appliance.GetComponent<fryingMinigame>().UpdateDishPointsFrying();

		PhotonView aV = appliance.GetComponent<PhotonView>();
        canvas.gameObject.SetActive(true);
		PV.RPC("setAddedF", RpcTarget.All, aV.ViewID);
		
        //minigameCanvas.gameObject.SetActive(false);
        if (appliance.itemsOnTheAppliance.Count > 0)
        {
            aV.RPC("clearItems", RpcTarget.All, aV.ViewID);


		}
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All,appliance.GetComponent<PhotonView>().ViewID);
		
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);

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
			PhotonView.Find(x[i]).GetComponent<PlayerController>().enabled = true;
			//PhotonView.Find(applID).GetComponent<Appliance>().appliancePlayers;
		}
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

}
