internal class Program
{
    private static void Main(string[] args)
    {
        List<Student> studentsToWrite = new List<Student>
            {
                new Student { Name = "Жульен", Group = "G1", DateOfBirth = new DateTime(2001, 10, 22), AverageScore = 3.3M },
                new Student { Name = "Боб", Group = "G1", DateOfBirth = new DateTime(1999, 5, 25), AverageScore = 4.5M},
                new Student { Name = "Лилия", Group = "F2", DateOfBirth = new DateTime(1999, 1, 11), AverageScore = 5M},
                new Student { Name = "Роза", Group = "F2", DateOfBirth = new DateTime(1989, 9, 19), AverageScore = 3.7M}
            };

        WriteStudentsToBinFile(studentsToWrite, "students.dat");


        List<Student> studentsToRead = ReadStudentsFromBinFile("students.dat");
        //foreach (Student studentProp in studentsToRead)
        //{
        //    Console.WriteLine(studentProp.Name + " " + studentProp.Group + " " + studentProp.DateOfBirth + " " + studentProp.AverageScore);
        //}

        SaveStudentsToTextFiles(studentsToRead);
        Console.WriteLine($"The data saved to text files in folder Students on desktop.");
    }

    static void WriteStudentsToBinFile(List<Student> students, string fileName)
    {
        using FileStream fs = new FileStream(fileName, FileMode.Create);
        using BinaryWriter bw = new BinaryWriter(fs);

        foreach (Student student in students)
        {
            bw.Write(student.Name);
            bw.Write(student.Group);
            bw.Write(student.DateOfBirth.ToBinary());
            bw.Write(student.AverageScore);
        }
        bw.Flush();
        bw.Close();
        fs.Close();

    }

    static List<Student> ReadStudentsFromBinFile(string fileName)
    {
        List<Student> result = new List<Student>();
        using FileStream fs = new FileStream(fileName, FileMode.Open);
        // чтение с бинарного файла
        //using StreamReader sr = new StreamReader(fs);
        using BinaryReader br = new BinaryReader(fs);

        //Console.WriteLine(sr.ReadToEnd());// не нужна

        //fs.Position = 0;///тож не надо

        //BinaryReader br = new BinaryReader(fs);/// сразу сделали

        while (fs.Position < fs.Length)
        {
            Student student = new Student // так немного симпотичнее ;)
            {
                Name = br.ReadString(),
                Group = br.ReadString(),
                DateOfBirth = DateTime.FromBinary(br.ReadInt64()),
                AverageScore = br.ReadDecimal()
            };
            result.Add(student);
        }

        fs.Close();
        return result;
    }

    static void SaveStudentsToTextFiles(List<Student> students)
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folderDir = Path.Combine(folderPath, "Students");

        if (!Directory.Exists(folderDir))
        {
            Directory.CreateDirectory(folderDir);
        }

        Dictionary<string, List<Student>> studentsGroups = new Dictionary<string, List<Student>>();
        foreach (var student in students)
        {
            if (!studentsGroups.ContainsKey(student.Group))
            {
                studentsGroups[student.Group] = new List<Student>();
            }
            studentsGroups[student.Group].Add(student);
        }

        foreach (var group in studentsGroups)
        {
            string textFileGroup = Path.Combine(folderDir, $"{group.Key}.txt");
            using (StreamWriter sw = new StreamWriter(textFileGroup))
            {
                foreach (var student in group.Value)
                {
                    sw.WriteLine($"{student.Name}, {student.DateOfBirth}, {student.AverageScore}");
                }
            }
        }
    }
}