using System.Collections;
using Scroll;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class ButtonsController : MonoBehaviour
    {
        [SerializeField] private ScrollContentController _scrollContentController;
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonStop;
        [SerializeField] private GameObject _panelBlockButtonStop;


        private void Start()
        {
            _panelBlockButtonStop.SetActive(false);
        }

        private void OnEnable()
        {
            _buttonStart.onClick.AddListener(_scrollContentController.StartGame);
            _buttonStop.onClick.AddListener(_scrollContentController.StopGame);
        }

        private void OnDisable()
        {
            _buttonStart.onClick.RemoveListener(_scrollContentController.StartGame);
            _buttonStop.onClick.RemoveListener(_scrollContentController.StopGame);
        }

        public IEnumerator BlockStopButton()
        {
            _panelBlockButtonStop.SetActive(true);
            _buttonStop.enabled = false;
            yield return new WaitForSeconds(_scrollContentController.ScrollData.AccelerationTime);
            _buttonStop.enabled = true;
            _panelBlockButtonStop.SetActive(false);
            StopCoroutine(BlockStopButton());
        }
    }
}