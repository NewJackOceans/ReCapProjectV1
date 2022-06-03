using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helper
{
    public class FileHelperManager : IFileHelper
    {
        public IResult Add(IFormFile file, string root)
        {
            CheckFileExists(file);

            if (!Directory.Exists(root))//Dosya dizini kontrol edilir.
                Directory.CreateDirectory(root);//Yoksa oluşturulur.

            string extension = Path.GetExtension(file.FileName); //Seçilen dosyanın uzantısını extension a atıyoruz.
            string imageName = Guid.NewGuid().ToString() + extension; //Benzersiz bir Guid oluşturuyoruz ve dosya uzantısını imageName e atıyoruz.
            CheckFileExtensionExists(extension);//Dosya Uzantısı konrol edilir.

            using (FileStream fileStream = File.Create(root + imageName))//FileStream nesnesi oluşturduk, File.Create(root + imageName) Bir dosya yolu oluşturduk parametre olarak dosya yolunu ve adını bildirdik.
            {
                file.CopyTo(fileStream);//Dosyanın yüklenecegi akışı belirledik.
                fileStream.Flush(); //arabellekleri temizler ve arabelleğe alınan verilerin dosyaya yazılmasına neden olur.
                return new SuccessResult(imageName); //Sql e imageName adı ile elemek için imageName verdik.
            }
        }

        public IResult Delete(string filePath)
        {
            if (File.Exists(filePath))//Aynı isimli dosya varmı diye kontrol ediyoruz.
            {
                File.Delete(filePath);
                return new SuccessResult("Resim Silindi");
            }
            else
                return new ErrorResult("Resim Bulunamadı");
        }

        public IResult Update(IFormFile file, string filePath, string root)
        {
            if (File.Exists(filePath))//Aynı isimli dosya varmı diye kontrol ediyoruz.
                File.Delete(filePath);

            return Add(file, root);
        }


        private static IResult CheckFileExists(IFormFile file)
        {
            if (file.Length > 0 || file != null)
                return new SuccessResult();
            else
                return new ErrorResult();
        }

        private static IResult CheckFileExtensionExists(string extension)
        {
            if (extension != ".jpeg" && extension != ".png" && extension != ".jpg")
                return new ErrorResult("Resim Uzantısı Yanlış");
            else
                return new SuccessResult();

        }
    }
}

