
    public static class ItemBuilderFabric
    {
        public static IItemBuilder ItemBuilder(ItemType type)
        {
            switch (type)
            {
                case ItemType.Torch:
                {
                    var itemBuilder = new TorchBuilder();
                    return itemBuilder;
                }
            }

            return null;
        }
    }
