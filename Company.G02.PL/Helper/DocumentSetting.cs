namespace Company.G02.PL.Helper
{
    public static class DocumentSetting
    {




        public static string UploadFile(IFormFile file, string folderName)
        {
            // Combine the folder path properly
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName);

            // Ensure the folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Create unique file name
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            // Create the full file path
            var filePath = Path.Combine(folderPath, fileName);

            // Save the file
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        //2. Delete

        public static void DeleteFile(string fileName, string folderName)
        {
            //string folderPath = "C:\\Users\\shoro\\OneDrive\\Desktop\\.net assigments\\MVC\\S3Try\\Company.G02\\Company.G02.PL\\wwwroot\\files\\" + folderName;
            // string folderPath = Path.Combine(DocumentPath, folderName);
            // var folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName;

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files" + folderName, fileName);

            var filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
