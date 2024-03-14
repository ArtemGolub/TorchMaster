
    using UnityEngine;

    public class ItemsBuilder: MonoBehaviour
    {
        public static ItemsBuilder current;
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
        
        public Item CreateItem(Transform transform, ItemSO itemData, Collider collider)
        {
            _itemDirector = new ItemDirector();
            Item item = _itemDirector.CreateItem(transform, itemData, collider);
            return item;
        }
    }
