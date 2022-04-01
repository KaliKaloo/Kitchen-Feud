using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace CustomProperties
{
    public class PlayerCustomProperties
    {
        private static ExitGames.Client.Photon.Hashtable property = new ExitGames.Client.Photon.Hashtable();
        // Adds 
        public static void AddProperty(string customProperty, int addition)
        {
            property[customProperty] = (int)PhotonNetwork.LocalPlayer.CustomProperties[customProperty] + addition;
            PhotonNetwork.LocalPlayer.SetCustomProperties(property);
        }

        // Update is called once per frame
        public static void ResetProperty(string customProperty)
        {
            property[customProperty] = 0;
            PhotonNetwork.LocalPlayer.SetCustomProperties(property);
        }
    }

    public class PlayerCookedDishes : PlayerCustomProperties
    {
        private static readonly string property = "CookedDishes";
        public static void AddCookedDishes()
        {
            AddProperty(property, 1);
        }
        public static void ResetCookedDishes()
        {
            ResetProperty(property);
        }
    }

    public class PlayerPoints : PlayerCustomProperties
    {
        private static readonly string property = "PlayerPoints";
        public static void AddIndividualPlayerPoints(int points)
        {
            AddProperty(property, points);
        }
        public static void ResetIndividualPlayerPoints()
        {
            ResetProperty(property);
        }
    }


}
