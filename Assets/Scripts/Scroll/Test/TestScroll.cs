using UnityEngine;
using UnityEngine.UI;

namespace Scroll.Test
{
    public class TestScroll : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _viewPortTransform;
        [SerializeField] private RectTransform _contentPanelTransform;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        [SerializeField] private RectTransform[] _itemList;

        private Vector2 _oldVelocity;
        private bool _isUpdate;

        private void Start()
        {
            SetContent();
        }

        private void Update()
        {
            InfiniteScrolling();
        }

        private void SetContent()
        {
            _oldVelocity = Vector2.zero;
            _isUpdate = false;

            int ItemsToAdd = Mathf.CeilToInt(_viewPortTransform.rect.width /
                                             (_itemList[0].rect.width + _horizontalLayoutGroup.spacing));
            for (int i = 0; i < ItemsToAdd; i++)
            {
                RectTransform RT = Instantiate(_itemList[i % _itemList.Length], _contentPanelTransform);
                RT.SetAsLastSibling();
            }

            for (int i = 0; i < ItemsToAdd; i++)
            {
                int num = _itemList.Length - i - 1;
                while (num < 0)
                {
                    num += _itemList.Length;
                }

                RectTransform RT = Instantiate(_itemList[num], _contentPanelTransform);
                RT.SetAsFirstSibling();
            }

            _contentPanelTransform.localPosition = new Vector3(
                (0 - (_itemList[0].rect.width + _horizontalLayoutGroup.spacing))
                * ItemsToAdd,
                _contentPanelTransform.localPosition.y,
                _contentPanelTransform.localPosition.z);
        }

        private void InfiniteScrolling()
        {
            if (_isUpdate)
            {
                _isUpdate = false;
                _scrollRect.velocity = _oldVelocity;
            }

            if (_contentPanelTransform.localPosition.x > 0)
            {
                Canvas.ForceUpdateCanvases();
                _oldVelocity = _scrollRect.velocity;
                _contentPanelTransform.position -= new Vector3(
                    _itemList.Length * (_itemList[0].rect.width + _horizontalLayoutGroup.spacing), 0, 0);
                _isUpdate = true;
            }

            if (_contentPanelTransform.localPosition.x <
                0 - (_itemList.Length * (_itemList[0].rect.width + _horizontalLayoutGroup.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                _oldVelocity = _scrollRect.velocity;
                _contentPanelTransform.position += new Vector3(
                    _itemList.Length * (_itemList[0].rect.width + _horizontalLayoutGroup.spacing), 0, 0);
                _isUpdate = true;
            }
        }
    }
}