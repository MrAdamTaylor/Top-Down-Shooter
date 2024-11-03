using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public interface IUIFactory : IGameFactory
    {
        public GameObject CreateWithLoadConnect(object popupPath, object parent);
        GameObject CreateResetButton(object canvasTag);
    }
}