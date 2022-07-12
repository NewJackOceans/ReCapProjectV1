using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;
        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageLimit(carImage.CarId));
            if (result != null)
            {
                return result;
            }


            carImage.ImagePath = _fileHelper.Upload(file, PathConstants.ImagesPath, carImage.ImagePath);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete(int id)
        {
            var carImage = _carImageDal.Get(carImage => carImage.Id == id);
            if (carImage != null)
                _carImageDal.Delete(carImage);
                        
            return new SuccessResult(Messages.CarImageDeleted);
        }
        public IResult Update(int id, IFormFile file)
        {
            var carImage = _carImageDal.Get(carImage => carImage.Id == id);
            if (carImage != null)
            {

                carImage.ImagePath = _fileHelper.Update(file, PathConstants.ImagesPath + carImage.ImagePath, PathConstants.ImagesPath);
                _carImageDal.Update(carImage);
                return new SuccessResult(Messages.CarImageUpdated);

            }
            else
                return new ErrorResult(Messages.CarImageNotUpdated);
            
        }       

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        private IResult CheckIfCarImageLimit(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IDataResult<List<CarImage>> GetDefaultImage(int carId)
        {
            List<CarImage> carImage = new List<CarImage>();
            carImage.Add(new CarImage { CarId = carId, Date = DateTime.Now, ImagePath = "DefaultImage.jpg" });
            return new SuccessDataResult<List<CarImage>>(carImage);
        }
        private IResult CheckCarImage(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<CarImage>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetForPageable(null, pageIndex, pageCount), Messages.CarImagePaging);
        }

        public Pageable<CarImage> Search(int id, int carId, int pageIndex, int pageCount)
        {
            Expression<Func<CarImage, bool>> searchQuery = carImage =>
            (id > 0 ? carImage.Id == id : true) &&
            (carId > 0 ? carImage.CarId == carId : true);

            var carImages = _carImageDal.GetForPageable(searchQuery, pageIndex, pageCount);
            var count = _carImageDal.GetCount(searchQuery);
            var data = new Pageable<CarImage>(pageIndex, pageCount, count, carImages);

            return data;

        }

        public IDataResult<List<CarImage>> GetByImageId(int id)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.Id == id));
        }
    }
}