using System.Collections;
using GameManager;
using TMPro;
using UnityEngine;

namespace UIs
{
    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private CoreManager _coreManager;
        [SerializeField] private TMP_Text _text;

        private Coroutine _retention;
        
        public void StartRetention()
        {
            if(_retention != null)
                return;
            
            _retention = StartCoroutine(Retention());
        }

        private void DisplayDelay(string value)
        {
            _text.text = value;
        }

        private IEnumerator Retention()
        {
            int delay = 3;

            for (int i = delay; i > 0; i--)
            {
                DisplayDelay(i.ToString());
                
                yield return new WaitForSeconds(1);
            }

            _text.enabled = false;
            _coreManager.OnStart();
        }
    }
}