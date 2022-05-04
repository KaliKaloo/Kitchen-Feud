using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace CustomProperties
{
    public class PlayerCustomProperties
    {
        private static ExitGames.Client.Photon.Hashtable property = new ExitGames.Client.Photon.Hashtable();
        // Adds a generic property
        // THIS SHOULD NOT BE USED TO ADD STUFF USE FUNCTIONS BELOW

        public static void AddProperty(string customProperty, int addition)
        {
            property[customProperty] = (int)PhotonNetwork.LocalPlayer.CustomProperties[customProperty] + addition;
            PhotonNetwork.LocalPlayer.SetCustomProperties(property);
        }

        // Resets a generic property
        // THIS SHOULD NOT BE USED TO RESET STUFF USE FUNCTIONS BELOW
        public static void ResetProperty(string customProperty)
        {
            property[customProperty] = 0;
            PhotonNetwork.LocalPlayer.SetCustomProperties(property);
        }
    }

    // Tracks how many times a player completes a dish using an appliance#
    // Calculated every time the player clicks finish on an appliance minigame
    public class PlayerCookedDishes : PlayerCustomProperties
    {
        public static readonly string property = "CookedDishes";
   
        public static void AddCookedDishes()
        {
            AddProperty(property, 1);
        }

        // returns a string message of the highest amount of cooked dishes
        public static string GetHighestCookedDishes()
        {
            int highest = 0;
            string name = "";
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {

                if ((int)player.CustomProperties[property] >= highest)
                {
                    highest = (int)player.CustomProperties[property];
                    name = player.NickName;
                }
            }
            return name + " cooked the most dishes!";
        }
    }

    // Tracks how many points each player gets in a server
    // Calculated every time the individual player presses serve on a tray and gets points
    public class PlayerPoints : PlayerCustomProperties
    {
        public static readonly string property = "PlayerPoints";

        // instead of adding 1 will track their whole points
        public static void AddIndividualPlayerPoints(int points)
        {
            AddProperty(property, points);
        }

        // returns a string message of the player with the highest points
        public static string GetHighestPlayerPoints(int team)
        {
            int highest = 0;
            string name = "";
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {

                if ((int)player.CustomProperties[property] >= highest && (int)player.CustomProperties["Team"] == team)
                {
                    highest = (int)player.CustomProperties[property];
                    name = player.NickName;
                }
            }

            if (name == "")
                return "No one was the MVP";
            else
                return "MVP: " + name;
        }
    }

    // Tracks how many times a player enters the enemy kitchen
    // Calculated when the player enemy the enemy kitchen
    public class PlayerMischievous : PlayerCustomProperties
    {
        public static readonly string property = "Mischievous";
        public static void AddMischievousStat()
        {
            AddProperty(property, 1);
        }

        // returns a string message of the player with the highest points
        // Does not require a team!
        public static string GetHighestMischievousStat()
        {
            int highest = 0;
            string name = "";
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {

                if ((int)player.CustomProperties[property] >= highest)
                {
                    highest = (int)player.CustomProperties[property];
                    name = player.NickName;
                }
            }
            return name + " was the most mischievous player!";
        }
    }

    // Tracks which player kicked the most enemy players out of the kitchen
    // Calculated when the enemy player gets teleported 
    public class PlayerVigilant : PlayerCustomProperties
    {
        public static readonly string property = "Vigilant";
        public static void AddVigilantStat()
        {
            AddProperty(property, 1);
        }

        // returns a string message of the player with the highest points given a team
        public static string GetHighestVigilantStat()
        {
            int highest = 0;
            string name = "";
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {

                if ((int)player.CustomProperties[property] >= highest)
                {
                    highest = (int)player.CustomProperties[property];
                    name = player.NickName;
                }
            }
            return name + " was the most vigilant player!";
        }
    }

    public class PlayerResetStats
    {
        private static ExitGames.Client.Photon.Hashtable property = new ExitGames.Client.Photon.Hashtable();

        // add all reset methods here so can be gathered and used easily
        public static void ResetAll()
        {
            property[PlayerCookedDishes.property] = 0;
            property[PlayerPoints.property] = 0;
            property[PlayerMischievous.property] = 0;
            property[PlayerVigilant.property] = 0;

            PhotonNetwork.LocalPlayer.SetCustomProperties(property);

        }
    }
}
