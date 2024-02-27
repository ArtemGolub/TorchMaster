
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

        public void SetID(int id)
        {
            _item.ID = id;
        }

        public void SetLifeTime(float time)
        {
            _item.LifeTime = time;
        }

        public void SetFSM(Transform transform, FSMType type)
        {
            _item.SM = FSMFactory.CreateStrategy(transform, type);
        }
    }
