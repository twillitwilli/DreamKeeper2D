public static class SceneSelector
{
    public enum SceneName
    {
        TitleScreen,
        NightmareNamikVillage,
        NamikVillage
    }

    public static int GetScene(SceneName nameOfScene)
    {
        return (int)nameOfScene;
    }
}
