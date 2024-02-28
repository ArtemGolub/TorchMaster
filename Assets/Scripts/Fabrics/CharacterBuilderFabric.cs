
    public static class CharacterBuilderFabric
    {
        public static ICharacterBuilder CharacterBuilder(CharacterType type)
        {
            switch (type)
            {
                case CharacterType.Player:
                {
                    var playerBuilder = new PlayerBuilder();
                    return playerBuilder;
                }
                case CharacterType.Enemy:
                {
                    var enemyBuilder = new EnemyBuilder();
                    return enemyBuilder;
                }
            }

            return null;
        }
    }
