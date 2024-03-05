
    using UnityEngine;

    public class ItemBuilder: IItemBuilder
    {
        private Item _item = new Item();
        
        public Item GetItem()
        {
            return _item;
        }

        public void SetName(string name)
        {
            _item.Name = name;
        }
        
        public void SetItemType(ItemType type)
        {
            _item.ItemType = type;
        }

        public void SetTransform(Transform transform)
        {
            _item.Transform = transform;
        }

        public void SetCollider(Collider collider)
        {
            _item.Collider = collider;
        }

        public void SetFSM(FSMType type)
        {
            _item.FSM = FSMFactory.CreateItemStrategy(_item, type);
        }
    }
