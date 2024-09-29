using UnityEngine;

public interface IUIFactory : IGameFactory
{
    public GameObject CreateWithLoadConnect(object popupPath, object provider, object player);
}