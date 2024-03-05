
    using UnityEngine;

    public class TorchBuilder: IItemBuilder
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

        public void SetItemCommandManager()
        {
            _item.ItemCommandManager = new ItemCommandManager();
            SetBurnCommand();
        }
        
        public void SetBurnCommand()
        {
            var burnCommand = new BurnStrategy(_item, 200f);
            _item.ItemCommandManager.AddCommand(ItemCommandType.Active, burnCommand);
        }

        public void SetFSM(FSMType type)
        {
            _item.FSM = FSMFactory.CreateItemStrategy(_item, type);
        }
    }
