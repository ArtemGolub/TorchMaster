
    using System.Collections.Generic;
    using UnityEngine;


    public class PlayerInventory: IInventory
    {
        private Transform _inventoryPose;
        private float _offSet = 0.1f;
        private Dictionary<int, List<Item>> _items = new Dictionary<int, List<Item>>();
        
        public PlayerInventory(Transform inventoryPose)
        {
            _inventoryPose = inventoryPose;
        }
        
        public void GrabItem(Transform transform,Item item)
        {
            AddItemToInventory(item);
            PlaceItem(transform, item);
        }

        private void PlaceItem(Transform transform,Item item)
        {
            transform.SetParent(_inventoryPose);
            float poseY = _inventoryPose.position.y + (_offSet * _items[item.ID].Count);
            Vector3 itemPosition = _inventoryPose.position;
            itemPosition.y = poseY;
            
            transform.position = itemPosition;
        }

        private void AddItemToInventory(Item item)
        {
            if (_items.ContainsKey(item.ID))
            {
                _items[item.ID].Add(item);
            }
            else
            {
                List<Item> newItemList = new List<Item>();
                newItemList.Add(item);
                _items.Add(item.ID, newItemList);
            }
        }

        public void ThrowItem(Item item)
        {
            
        }
    }
