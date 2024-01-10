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
        [SerializeField] private float _accelerationTime = 4f;

        [SerializeField] private float _currentSpeed; //Вывел для проверки текущей скорости.

        private float _time;
        private bool _isGame;
        private bool _isStopTransform;

        private void Start()
        {
            _isGame = false;
            _isStopTransform = false;
        }

        private void Update()
        {
            if (_isGame)
            {
                UpdateSpeed(Time.deltaTime);
                TransformScrollContent();
            }

            if (_isStopTransform)
            {
                UpdateSpeed(Time.deltaTime);
                TransformScrollContent();
            }
        }

        private void UpdateSpeed(float deltaTime)
        {
            if (_currentSpeed < _maxSpeed && _isGame)
            {
                _currentSpeed = Mathf.Lerp(_startSpeed, _maxSpeed, _time / _accelerationTime);
                _time += deltaTime;
            }
            else if (_currentSpeed > 0 && !_isGame)
            {
                _currentSpeed = Mathf.Lerp(_maxSpeed, 0, _time / _accelerationTime);
                _time += deltaTime;
                if (_currentSpeed <= 0)
                {
                    _currentSpeed = 0;
                    _time = 0;
                }
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

        private void TransformScrollContent()
        {
            float deltaY = _currentSpeed * Time.deltaTime;
            _scrollContent.anchoredPosition -= new Vector2(0, deltaY);
        }

        private void StartGame()
        {
            _isGame = true;
            _isStopTransform = false;
            _time = 0;
        }

        private void StopGame()
        {
            _isGame = false;
            _isStopTransform = true;
            _time = 0;
        }
    }
}