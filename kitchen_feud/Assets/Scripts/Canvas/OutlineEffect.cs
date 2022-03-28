using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
// namespace outlineEffect {
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
        
            //GameObject outlineObject = Instantiate(this.gameObject,transform.position,transform.rotation,transform);
            // GameObject outlineObjectCopy = this.gameObject;
           
            // PhotonView pv = outlineObjectCopy.GetComponent<PhotonView>();
            // pv.enabled = false;
           // outlineObject = Instantiate(this.gameObject,transform.position,transform.rotation,transform);
            //  Debug.Log(outlineObject.name);
            outlineObject = PhotonNetwork.Instantiate(Path.Combine("Appliances","Stove"), transform.position,Quaternion.identity);

            outlineObject.transform.SetParent(gameObject.transform);
            
            outlineObject.transform.localScale = new Vector3(1, 1, 1);
            // //Destroy(outlineObject.GetComponent<PhotonView>());
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
        
        void Update(){
           // if(!outlineObject){

            //Debug.Log(outlineObject.name);

            }
        // }
        // private void OnMouseExit()
        // {
        //     rend.enabled = false;
        // }
        // private void OnMouseEnter()
        // {
        //    rend.enabled = true;
        // }
        // // Update is called once per frame
        // void Update()
        // {
            
        // }
    }
// }