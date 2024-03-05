
    public static class ItemBuilderFabric
    {
        public static IItemBuilder ItemBuilder(ItemType type)
        {
            switch (type)
            {
                case ItemType.Torch:
                {
                    var itemBuilder = new ItemBuilder();
                    return itemBuilder;
                }
                case ItemType.Oil:
                {
                    var itemBuilder = new ItemBuilder();
                    return itemBuilder;
                }
            }

            return null;
        }
    }
