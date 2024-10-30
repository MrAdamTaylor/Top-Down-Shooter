using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class GameSystem : MonoBehaviour
    {

        private GameObject _resetMenu;
        
        public void Construct(GameObject resetMenu)
        {
            _resetMenu = resetMenu;
            UIDeathPopupProvider resetPopup = resetMenu.GetComponent<UIDeathPopupProvider>();
            resetPopup.Construct(this);
            _resetMenu.SetActive(false);
        }

        public void ShowResetMenu()
        {
            _resetMenu.SetActive(true);
        }

        public void ReloadGame()
        {
            Debug.Log($"<color=green>Reload Game</color>");
        }

        public void MainMenu()
        {
            Debug.Log($"<color=green>Main Menu</color>");
        }
    }
}