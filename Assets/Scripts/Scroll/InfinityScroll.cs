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

        public RectTransform[] ItemList => _itemList;

        public VerticalLayoutGroup VerticalLayoutGroup => _verticalLayoutGroup;


        private void Start()
        {
            SetContent();

            if (_scrollRect != null)
            {
                _scrollRect.horizontal = false;
                _scrollRect.vertical = false;
            }
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
                                             (ItemList[0].rect.height + VerticalLayoutGroup.spacing));
            for (int i = 0; i < itemsToAdd; i++)
            {
                RectTransform rt = Instantiate(ItemList[i % ItemList.Length], _contentPanelTransform);
                rt.SetAsLastSibling();
            }

            for (int i = 0; i < itemsToAdd; i++)
            {
                int num = ItemList.Length - i - 1;
                while (num < 0)
                {
                    num += ItemList.Length;
                }

                RectTransform rt = Instantiate(ItemList[num], _contentPanelTransform);
                rt.SetAsFirstSibling();
            }

            _contentPanelTransform.localPosition = new Vector3(
                _contentPanelTransform.localPosition.x,
                (0 - (ItemList[0].rect.height + VerticalLayoutGroup.spacing)) * itemsToAdd,
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
                    0, ItemList.Length * (ItemList[0].rect.height + VerticalLayoutGroup.spacing), 0);
                _isUpdate = true;
            }

            if (_contentPanelTransform.localPosition.y <
                0 - (ItemList.Length * (ItemList[0].rect.height + VerticalLayoutGroup.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                _oldVelocity = _scrollRect.velocity;
                _contentPanelTransform.position += new Vector3(
                    0, ItemList.Length * (ItemList[0].rect.height + VerticalLayoutGroup.spacing), 0);
                _isUpdate = true;
            }
        }
    }
}