using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public interface IUIFactory : IGameFactory
    {
        public GameObject CreateWithLoadConnect(object popupPath, object provider, object player);
        GameObject CreateResetButton(object canvasTag);
    }
}