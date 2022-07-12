using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Requests.Colors;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorsListed);
        }

        //[ValidationAspect(typeof(ColorValidator))]
        public IResult Add(CreateColorRequest request)
        {
            //ValidationTool.Validate(new ColorValidator(), color);
            Color color = new Color();

            var colorName = _colorDal.Get(color => color.ColorName == request.ColorName);
            if (colorName != null)
                return new ErrorResult(Messages.ColorNameIsAvailable);            
            else
                color.ColorName = request.ColorName;

            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }

        public IResult Update(int id, UpdateColorRequest request)
        {
            var color = _colorDal.Get(color => color.ColorId == id);
            if (color != null)
            {
                color.ColorName = request.ColorName;
                _colorDal.Update(color);
                return new SuccessResult(Messages.ColorUpdated);
            }
            else
                return new ErrorResult(Messages.ColorNotUpdated);
            
        }

        public IResult Delete(int id)
        {
            var color = _colorDal.Get(color => color.ColorId == id);
            if(color != null)
                _colorDal.Delete(color);

            return new SuccessResult(Messages.ColorDeleted);
        }

        public IDataResult<List<Color>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetForPageable(null, pageIndex, pageCount), Messages.ColorPaging);
        }

        public Pageable<Color> Search(string colorName, int colorId, int pageIndex, int pageCount)
        {
            Expression<Func<Color, bool>> searchQuery = color =>
            (!string.IsNullOrWhiteSpace(colorName) ? color.ColorName.Contains(colorName) : true) &&
            (colorId > 0 ? color.ColorId == colorId : true);
            
            var colors = _colorDal.GetForPageable(searchQuery, pageIndex, pageCount);
            var count = _colorDal.GetCount(searchQuery);
            var data = new Pageable<Color>(pageIndex, pageCount, count, colors);

            return data;

        }

        public IDataResult<Color> GetById(int id)
        {
            var color = _colorDal.Get(color => color.ColorId == id);
            if (color == null)
            {
                return new ErrorDataResult<Color>(Messages.NotFoundColor);
            }
            return new SuccessDataResult<Color>(color);
        }
    }
}