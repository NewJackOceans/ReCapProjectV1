using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Requests.Cards;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CardManager : ICardService
    {
        ICardDal _cardDal;
        ICustomerService _customerService;

        public CardManager(ICardDal cardDal, ICustomerService customerService)
        {
            _cardDal = cardDal;
            _customerService = customerService;
        }

        public IResult Add(CreateCardRequest request)
        {
            Card card = new Card();

            var customer = _customerService.GetById(request.CustomerId);
            if (customer.Success)
                card.CustomerId = request.CustomerId;
            else
                return new ErrorResult(customer.Message);
            card.CreditCardNumber = request.CreditCardNumber;
            card.OwnerName = request.OwnerName;
            card.ExpirationDate = request.ExpirationDate;
            card.SecurityCode = request.SecurityCode;

            _cardDal.Add(card);
            return new SuccessResult("Card Added");
        }

        public IResult Delete(int id)
        {
            var card = _cardDal.Get(card => card.CardId == id);
            if (card != null)
                _cardDal.Delete(card);
            return new SuccessResult("Card Deleted");
        }

        public IResult Update(int id, UpdateCardRequest request)
        {
            var card = _cardDal.Get(card => card.CardId == id);
            if (card != null)
            {
                card.CreditCardNumber = request.CreditCardNumber;
                card.ExpirationDate = request.ExpirationDate;
                card.OwnerName = request.OwnerName;
                card.CustomerId = request.CustomerId;
                card.SecurityCode = request.SecurityCode;
                _cardDal.Update(card);
                return new SuccessResult(Messages.CardUpdated);
            }
            else
                return new ErrorResult(Messages.CardNotUpdated);
            
        }

        public IDataResult<List<Card>> GetAll()
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll());
        }

        public IDataResult<List<Card>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetForPageable(null, pageIndex, pageCount), Messages.CardPaging);
        }

        public Pageable<Card> Search(string ownerName, int cardId, int customerId, int pageIndex, int pageCount)
        {
            Expression<Func<Card, bool>> searchQuery = card =>
            (!string.IsNullOrWhiteSpace(ownerName) ? card.OwnerName.Contains(ownerName) : true) &&
            (cardId > 0 ? card.CardId == cardId : true) &&
            (customerId > 0 ? card.CustomerId == customerId : true);

            var cards = _cardDal.GetForPageable(searchQuery, pageIndex, pageCount);
            var count = _cardDal.GetCount(searchQuery);
            var data = new Pageable<Card>(pageIndex, pageCount, count, cards);

            return data;
        }
    }
}
