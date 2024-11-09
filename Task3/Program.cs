internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter path for cleaning: ");
        string path = Console.ReadLine();

        Console.Write("Original data size: ");
        PrintFolderSize(path);
        Console.WriteLine();

        DeleteFilesAndFolders(path);

        Console.Write("Current data size: ");
        PrintFolderSize(path);
        Console.WriteLine();
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
            Console.WriteLine($"{CalcFolderSize(dir)} bytes");

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

    public static void DeleteFilesAndFolders(string path)
    {
        long deletedBytes = 0;
        try
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Path {path} doesnt exist.");
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] folders = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();

            foreach (DirectoryInfo folder in folders)
            {
                if ((DateTime.Now - folder.LastWriteTime) > TimeSpan.FromMinutes(30)) //LastAccessTime??? doesnt work :/
                {
                    try
                    {
                        deletedBytes += CalcFolderSize(folder);
                        folder.Delete(true);
                        Console.WriteLine($"folder ({folder.Name}) deleted sucsessfully.");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Folder {folder.Name} isn't deleted, error {ex.Message}");
                    }
                }
            }

            foreach (FileInfo file in files)
            {
                if ((DateTime.Now - file.LastWriteTime) > TimeSpan.FromMinutes(30))  //LastAccessTime??? doesnt work :/
                {
                    try
                    {
                        deletedBytes += file.Length;
                        file.Delete();
                        Console.WriteLine($"file ({file.Name}) deleted sucsessfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"File {file.Name} isn't deleted, error {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("error: " + ex.Message);
        }

        Console.WriteLine($"Cleaning completed. {deletedBytes} bytes deleted.");
    }
}