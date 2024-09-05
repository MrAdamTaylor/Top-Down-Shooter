using System;
using UnityEngine;

public class Test1 : AIPart
{
    public override void Subscribe(Action name)
    {
        name += TestMethod;
        Debug.Log("Подписка на Test1");
    }

    public override void Unsubscribe(Action name)
    {
        name -= TestMethod;
    }

    private void TestMethod()
    {
        Debug.Log("Test1 - Execute");
    }
}