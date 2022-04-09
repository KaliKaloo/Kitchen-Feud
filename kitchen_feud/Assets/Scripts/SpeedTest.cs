using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Diagnostics;
using System.Globalization;
using System;
using Debug = UnityEngine.Debug;


public class SpeedTest : MonoBehaviour
{
    private DateTime dt1;

    private DateTime dt2;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError(CheckInternetSpeed());
    }

    // Update is called once per frame




        public double CheckInternetSpeed()
        {
            // Create Object Of WebClient
            WebClient wc = new WebClient();

            //DateTime Variable To Store Download Start Time.
             dt1 = DateTime.Now;

            //Number Of Bytes Downloaded Are Stored In ‘data’
            byte[] data = wc.DownloadData("https://www.google.co.uk");

            //DateTime Variable To Store Download End Time.
             dt2 = DateTime.Now;
            Debug.Log((dt2-dt1).TotalSeconds);
            Debug.Log(data.Length);
            //To Calculate Speed in Kb Divide Value Of data by 1024 And Then by End Time Subtract Start Time To Know Download Per Second.
            return Math.Round((data.Length * 0.008f) / (dt2 - dt1).TotalSeconds, 2);            
        }


    

}
