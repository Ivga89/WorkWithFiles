internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter path to canculate size: ");
        string path = Console.ReadLine();

        Console.WriteLine(CalcFolderSize(Directory.CreateDirectory(path)));
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