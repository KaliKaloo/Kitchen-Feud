using System.Collections;
using System.Collections.Generic;
using System.Text;
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
	}


    void TaskOnClick()
    {
		MusicManager.instance.minigameEnd();
		MusicManager.instance.inMG = false;
		
		// stop cooking animation
		playerAnimator.animator.SetBool("IsCooking", false);
	    GameObject gamePlayer = GameObject.Find("Local");
		PhotonView playerV = gamePlayer.GetPhotonView();
		playerV.RPC("setInMinigameF", RpcTarget.All, playerV.ViewID);

		

		if (appliance)
	    {
		    appliance.GetComponent<fryingMinigame>().UpdateDishPointsFrying();

	    }
	    
		PhotonView aV = appliance.GetComponent<PhotonView>();
		appliance.canvas.SetActive(true);
		PV.RPC("setAddedF", RpcTarget.AllBuffered, aV.ViewID);
		
        if (appliance.itemsOnTheAppliance.Count > 0)
        {
            aV.RPC("clearItems", RpcTarget.AllBuffered, aV.ViewID);


		}
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.AllBuffered,appliance.GetComponent<PhotonView>().ViewID);
		if (appliance.cookedDish)
		{
			appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.AllBuffered);
		}

		
		PhotonView	view = appliance.GetComponent<PhotonView>();

        PV.RPC("clearApplPlayers", RpcTarget.AllBuffered, view.ViewID);
        gamePlayer.GetComponentInChildren<playerMvmt>().enabled = true;
        gamePlayer.GetComponent<PlayerController>().enabled = true;
        appliance.UIcamera.enabled =  false;
        Destroy(minigameCanvas);
	


    }

	[PunRPC]
	void globalDestroy(int viewID)
	{
		Destroy(PhotonView.Find(viewID).gameObject);
	}
    [PunRPC]
    void clearApplPlayers(int applID)
    {
		/*List<int> x = PhotonView.Find(applID).GetComponent<Appliance>().appliancePlayers;

	
		for (int i=0; i < x.Count; i++)
		{
			PhotonView.Find(x[i]).GetComponentInChildren<playerMvmt>().enabled = true;
			PhotonView.Find(x[i]).GetComponentInChildren<PlayerController>().enabled = true;

		}

	

		PhotonView.Find(applID).GetComponent<Appliance>().UIcamera.enabled = false;	*/
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
    void setCanvActive(int viewID)
    {
	    PhotonView.Find(viewID).GetComponent<Appliance>().canvas.SetActive(true);
    }


}
