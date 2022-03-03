using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMvmt
{

    PlayerController playerContr; 
    Rigidbody player;
    GameObject obj;


    [OneTimeSetUp]
    public void SetUp() {
        obj = new GameObject();
        playerContr = obj.AddComponent<PlayerController>();
        player = obj.AddComponent<Rigidbody>();
        playerContr.m_speed =  200;
    }

    [Test]
    public void tranformZVelocity()
    {
        Vector3 mvmt = playerContr.getTransformVelocity(new Vector3(2,3,4));
        Assert.AreNotEqual(0.0f, mvmt.z);
        Assert.AreEqual(playerContr.m_speed * 4 * Time.deltaTime, mvmt.z);
    }

    [Test]
    public void playerVelocityZNotZero()
    {
        player.velocity = playerContr.getTransformVelocity(obj.transform.forward);
        Assert.AreNotEqual(0.0f, player.velocity.z);
    }

    [Test]
    public void playerVelocityXEqualsZero()
    {
        player.velocity = playerContr.getTransformVelocity(obj.transform.forward);
        Assert.AreEqual(0.0f, player.velocity.x);

    }

    [Test]
    public void playerVelocityYEqualsZero()
    {
        player.velocity = playerContr.getTransformVelocity(obj.transform.forward);
        Assert.AreEqual(0.0f, player.velocity.y);

    }


}
