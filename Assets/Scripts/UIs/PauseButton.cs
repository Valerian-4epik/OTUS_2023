using GameManager;
using UnityEngine;

namespace UIs
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private CoreManager _coreManager;
        
        public void PauseGame()
        {
            if (_coreManager.GameState ==  GameState.Pause)
            {
                _coreManager.Resume();
                return;
            }

            _coreManager.Pause();
        }
    }
}