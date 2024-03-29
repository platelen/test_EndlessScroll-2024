using Buttons;
using ContentScroll;
using SO;
using UnityEngine;

namespace Scroll
{
    public class ScrollContentController : MonoBehaviour
    {
        [SerializeField] private ContentAlignmentCalculation _contentAlignmentCalculation;
        [SerializeField] private ButtonsController _buttonsController;
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private SoScrollData _soScrollData;

        // [SerializeField] private float _startSpeed = 100f;
        // [SerializeField] private float _maxSpeed = 1000f;
        // [SerializeField] private float _accelerationTime = 4f;
        [SerializeField] private float _currentSpeed; //Вывел для проверки текущей скорости.

        private float _time;
        private bool _isGame;
        private bool _isStopTransform;

        //public float AccelerationTime => _accelerationTime;

        public SoScrollData ScrollData => _soScrollData;


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

            if (_currentSpeed <= 0)
                _contentAlignmentCalculation.CenterContentOnMiddleItem(_scrollContent);
        }

        private void UpdateSpeed(float deltaTime)
        {
            if (_currentSpeed < _soScrollData.MaxSpeed && _isGame)
            {
                _currentSpeed = Mathf.Lerp(_soScrollData.StartSpeed, _soScrollData.MaxSpeed,
                    _time / _soScrollData.AccelerationTime);
                _time += deltaTime;
            }
            else if (_currentSpeed > 0 && !_isGame)
            {
                _currentSpeed = Mathf.Lerp(_soScrollData.MaxSpeed, 0, _time / _soScrollData.AccelerationTime);
                _time += deltaTime;
                if (_currentSpeed <= 0)
                {
                    _currentSpeed = 0;
                    _time = 0;
                }
            }
        }


        private void TransformScrollContent()
        {
            float deltaY = _currentSpeed * Time.deltaTime;
            _scrollContent.anchoredPosition -= new Vector2(0, deltaY);
        }

        public void StartGame()
        {
            _isGame = true;
            StartCoroutine(_buttonsController.BlockStopButton());
            _isStopTransform = false;
            _time = 0;
        }

        public void StopGame()
        {
            _isGame = false;
            _isStopTransform = true;
            _time = 0;
        }
    }
}