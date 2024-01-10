using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonStop;
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private float _startSpeed = 100f;
        [SerializeField] private float _maxSpeed = 1000f;

        private float _speed;
        private bool _isGame;

        private void Start()
        {
            _isGame = false;
            _speed = 0;
        }

        private void Update()
        {
            if (_isGame)
            {
                StartTransform();
            }
            else
            {
                StopTransform();
            }
        }

        private void OnEnable()
        {
            _buttonStart.onClick.AddListener(StartGame);
            _buttonStop.onClick.AddListener(StopGame);
        }

        private void OnDisable()
        {
            _buttonStart.onClick.RemoveListener(StartGame);
            _buttonStop.onClick.RemoveListener(StopGame);
        }

        private void StartTransform()
        {
            _speed = _startSpeed;
            float deltaY = _speed * Time.deltaTime;
            _scrollContent.anchoredPosition -= new Vector2(0, deltaY);
        }

        private void StopTransform()
        {
            _speed = 0;
        }

        private void StartGame()
        {
            Debug.Log("Стартуем");
            _isGame = true;
        }

        private void StopGame()
        {
            Debug.Log("Останавливаемся");
            _isGame = false;
        }
    }
}