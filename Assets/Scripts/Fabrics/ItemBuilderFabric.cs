
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
                case ItemType.Oil:
                {
                    var itemBuilder = new OilBuilder();;
                    return itemBuilder;
                }
                case ItemType.TrueSight:
                {
                    var itemBuilder = new TrueSightBuilder();
                    return itemBuilder;
                }
                case ItemType.Key:
                {
                    var itemBuilder = new KeyBuilder();
                    return itemBuilder;
                }
            }

            return null;
        }
    }
