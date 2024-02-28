
    using System.Collections.Generic;
    using UnityEngine;


    public sealed class PlayerInventory: IInventory
    {
        private Transform _inventoryPose;
        private float _offSet = 0.1f;

        private int capacity = 0;
        private int maxCapacity = 5;
        
        readonly Dictionary<int, List<Item>> _items = new Dictionary<int, List<Item>>();
        
        public PlayerInventory(Transform inventoryPose)
        {
            _inventoryPose = inventoryPose;
        }
        
        public void GrabItem(Transform transform,Item item)
        {
            if(capacity >= maxCapacity) return;
            AddItemToInventory(item);
            PlaceItem(transform, item);
            capacity++;
            item.SM.Grab(this);
            ActivateNext(item);
        }
        
        public void RemoveItem(Transform transform, Item item)
        {
            transform.SetParent(null);
            UnPlaceItem(transform, item);
            RemoveItemFromInventory(item);
            capacity--;
            item.SM.Removed();
            ActivateNext(item);
        }

        private void ActivateNext(Item item)
        {
            List<Item> itemList = _items[item.ID];
            if (itemList.Count > 0)
            {
                Item firstItem = itemList[0];
                firstItem.SM.Active();
            }
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

        private void RemoveItemFromInventory(Item item)
        {
            if (_items.ContainsKey(item.ID))
            {
                _items[item.ID].Remove(item);
            }
        }
        private void PlaceItem(Transform transform,Item item)
        {
            transform.SetParent(_inventoryPose);
            
            float poseY = _inventoryPose.position.y + (_offSet * _items[item.ID].Count);
            Vector3 itemPosition = _inventoryPose.position;
            itemPosition.y = poseY;
            
            transform.position = itemPosition;
        }

        private void UnPlaceItem(Transform transform,Item item)
        {
            transform.SetParent(null);
            
            float poseY = _inventoryPose.position.y + (_offSet * _items[item.ID].Count);
            Vector3 itemPosition = _inventoryPose.position;
            itemPosition.y = poseY;
            transform.position = itemPosition;
            
            item.SM.Removed();
        }


    }
