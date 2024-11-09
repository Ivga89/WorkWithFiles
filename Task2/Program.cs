internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter path to canculate size: ");
        string path = Console.ReadLine();

        PrintFolderSize(path);
    }

    public static void PrintFolderSize(string path)
    {
        if (!Directory.Exists(path))
        {
            Console.WriteLine($"Path {path} doent exit.");
            return;
        }
        DirectoryInfo dir = new DirectoryInfo(path);
        try
        {
            Console.WriteLine($"\nFolder size: {CalcFolderSize(dir)} bytes");
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
    }

    public static long CalcFolderSize(DirectoryInfo dir)
    {
        long size = 0;

        FileInfo[] files = dir.GetFiles();

        foreach (FileInfo file in files)
        {
            size += file.Length;
        }

        DirectoryInfo[] folders = dir.GetDirectories();
        foreach (DirectoryInfo folder in folders)
        {
            size += CalcFolderSize(folder);
        }

        return size;
    }
}