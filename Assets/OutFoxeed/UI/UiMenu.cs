using System;
using UnityEngine;

namespace OutFoxeed.UI
{
    public class UiMenu : MonoBehaviour
    {
        private UiManager uiManager;
        public void Init(UiManager uiManager) => this.uiManager = uiManager;
        protected void OpenUiOfType(UiManager.UiType type) => uiManager.OpenUiOfType(type);
        
        #region Methods for Ui buttons
        public void OpenUiOfType(string typeName)
        {
            if (Enum.TryParse(typeName, out UiManager.UiType type))
            {
                uiManager.OpenUiOfType(type);
            }
        }
        public void LeaveMenu() => uiManager.LeaveCurrentUiMenu();
        public void ExitAllUi() => uiManager.ExitAllUi();
        public void QuitApplication() => Application.Quit();
        #endregion
    }
}