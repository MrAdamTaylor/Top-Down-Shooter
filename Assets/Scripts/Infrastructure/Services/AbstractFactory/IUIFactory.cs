using System;
using UnityEngine;

public interface IUIFactory : IGameFactory
{
    public void Create()
    {
        throw new Exception("For UI Factory don't implemented method Create");
    }

    public GameObject CreateWithLoadConnect(object popupPath, object provider, object player);
}