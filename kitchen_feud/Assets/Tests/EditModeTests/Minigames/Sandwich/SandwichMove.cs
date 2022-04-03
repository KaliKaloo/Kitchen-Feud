using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class SandwichMoveTest
{
    SandwichMove sandwichMove;

    [SetUp]
    public void setUp(){
        GameObject obj = new GameObject();
        sandwichMove = obj.AddComponent<SandwichMove>();
    }

    [Test]
    public void InitSandwichMove()
    {
        sandwichMove.stopped = false;
    }
}
