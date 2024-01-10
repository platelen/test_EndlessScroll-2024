using UnityEngine;
using UnityEngine.UI;

namespace Scroll
{
    public class InfinityScroll : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _viewPortTransform;
        [SerializeField] private RectTransform _contentPanelTransform;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
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

            int itemsToAdd = Mathf.CeilToInt(_viewPortTransform.rect.height /
                                             (_itemList[0].rect.height + _verticalLayoutGroup.spacing));
            for (int i = 0; i < itemsToAdd; i++)
            {
                RectTransform rt = Instantiate(_itemList[i % _itemList.Length], _contentPanelTransform);
                rt.SetAsLastSibling();
            }

            for (int i = 0; i < itemsToAdd; i++)
            {
                int num = _itemList.Length - i - 1;
                while (num < 0)
                {
                    num += _itemList.Length;
                }

                RectTransform rt = Instantiate(_itemList[num], _contentPanelTransform);
                rt.SetAsFirstSibling();
            }

            _contentPanelTransform.localPosition = new Vector3(
                _contentPanelTransform.localPosition.x,
                (0 - (_itemList[0].rect.height + _verticalLayoutGroup.spacing)) * itemsToAdd,
                _contentPanelTransform.localPosition.z);
        }

        private void InfiniteScrolling()
        {
            if (_isUpdate)
            {
                _isUpdate = false;
                _scrollRect.velocity = _oldVelocity;
            }

            if (_contentPanelTransform.localPosition.y > 0)
            {
                Canvas.ForceUpdateCanvases();
                _oldVelocity = _scrollRect.velocity;
                _contentPanelTransform.position -= new Vector3(
                    0, _itemList.Length * (_itemList[0].rect.height + _verticalLayoutGroup.spacing), 0);
                _isUpdate = true;
            }

            if (_contentPanelTransform.localPosition.y <
                0 - (_itemList.Length * (_itemList[0].rect.height + _verticalLayoutGroup.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                _oldVelocity = _scrollRect.velocity;
                _contentPanelTransform.position += new Vector3(
                    0, _itemList.Length * (_itemList[0].rect.height + _verticalLayoutGroup.spacing), 0);
                _isUpdate = true;
            }
        }
    }
}