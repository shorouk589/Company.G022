namespace Company.G02.PL.Helper
{
    public static class DocumentSettings
    {
        // 1. Upload
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Folder Location 
            // string folderPath = "C:\\Users\\shoro\\OneDrive\\Desktop\\.net assigments\\MVC\\S3Try\\Company.G02\\Company.G02.PL\\wwwroot\\files\\" + folderName;
            // var folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);








            // 2. Get File Name and make it unique "Guid"
            // 2. FileName
            //  var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var filename = $"{Guid.NewGuid()}{file.FileName}";





            // file path
            var filepath = Path.Combine(folderPath, filename);
            using var fileStream = new FileStream(filepath, FileMode.Create);

            file.CopyTo(fileStream);


            return filename ;
        }

        // 2. Delete

        public static void DeleteFile(string fileName, string folderName)
        {
            // 1. Get Folder Location 
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            // 2. File Path
            var filePath = Path.Combine(folderPath, fileName);

            // 3. Delete
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
