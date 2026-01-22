using System.IO;

public static class SaveService
{
    public static void Save()
    {
        File.WriteAllText("D:/TestSaveUnityGamingCampus/save.txt", "Hello World!");
    }
}
