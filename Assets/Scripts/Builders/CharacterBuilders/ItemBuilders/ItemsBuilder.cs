
    using System.Collections.Generic;
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
        
        public Item CreateItem(Transform transform, ItemSO itemData, Collider collider, List<Transform> lightPoints)
        {
            _itemDirector = new ItemDirector();
            Item item = _itemDirector.CreateItem(transform, itemData, collider, lightPoints);
            var collectSound = Instantiate(item.collectSound, item.Transform);
            item.collectSound = collectSound;

            var cantCollectSound = Instantiate(item.cantCollectSound, item.Transform);
            item.cantCollectSound = cantCollectSound;
            
            return item;
        }
    }
