
    using System.Collections.Generic;
    using UnityEngine;

    public class TorchBuilder: IItemBuilder
    {
        private Item _item = new Item();
       
        public Item GetItem()
        {
            return _item;
        }

        public void SetCollectSound(AudioSource sound)
        {
            _item.collectSound = sound;
        }

        public void SetCantCollectSound(AudioSource sound)
        {
            _item.cantCollectSound = sound;
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

        public void SetItemCommandManager(ItemSO itemSo)
        {
            _item.ItemCommandManager = new ItemCommandManager();
            SetBurnCommand(itemSo);
        }
        
        private void SetBurnCommand(ItemSO itemSo)
        {
            var burnCommand = new BurnStrategy(_item, itemSo.burnTime);
            TorchCanvas.current.InitSlider(itemSo.burnTime);
            _item.ItemCommandManager.AddCommand(ItemCommandType.Active, burnCommand);
        }

        public void SetFSM(FSMType type)
        {
            _item.FSM = FSMFactory.CreateItemStrategy(_item, type);
        }

        public void SetLightPoints(List<Transform> lightPoints)
        {
            _item.LightPoint = lightPoints;
        }
    }
