
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
            }

            return null;
        }
    }
