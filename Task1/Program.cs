internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter folder path for clean: ");
        string path = Console.ReadLine();


        DeleteFilesAndFolders(path);
    }

    public static void DeleteFilesAndFolders(string path)
    {
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
                if ((DateTime.Now - folder.LastAccessTime) > TimeSpan.FromMinutes(30))
                {
                    try
                    {
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
                if ((DateTime.Now - file.LastAccessTime) > TimeSpan.FromMinutes(30))
                {
                    try
                    {
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

        Console.WriteLine($"Cleaning completed.");
    }
}