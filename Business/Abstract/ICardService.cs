using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Requests.Cards;

namespace Business.Abstract
{
    public interface ICardService
    {
        IResult Add(Card card);
        IResult Delete(int id);
        IResult Update(int id, UpdateCardRequest request);

        IDataResult<List<Card>> GetAll();
        IDataResult<List<Card>> GetForPageable(int pageIndex, int pageCount);
        IDataResult<List<Card>> Search(string ownerName, int cardId, int customerId, int pageIndex, int pageCount);
    }
}