
    using UnityEngine;

    public class ItemBuilder: MonoBehaviour
    {
        public static ItemBuilder current;
        private ItemDirector _itemDirector;

        private void Awake()
        {
            if (current == null)
            {
                current = this;
            }
            if (current != this)
            {
                Destroy(transform);
            }
        }
        
        public Item CreateItem(Transform transform, ItemSO itemData)
        {
            _itemDirector = new ItemDirector();
            Item item = _itemDirector.CreateItem(transform, itemData);
            return item;
        }
    }
