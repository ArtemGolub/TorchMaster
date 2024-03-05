
    public static class CharacterBuilderFabric
    {
        public static ICharacterBuilder CharacterBuilder(CharacterType type)
        {
            switch (type)
            {
                case CharacterType.Player:
                {
                    var playerBuilder = new CharacterBuilder();
                    return playerBuilder;
                }
                case CharacterType.Enemy:
                {
                    var enemyBuilder = new CharacterBuilder();
                    return enemyBuilder;
                }
            }

            return null;
        }
    }
