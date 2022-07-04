﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CardManager : ICardService
    {
        ICardDal _cardDal;

        public CardManager(ICardDal cardDal)
        {
            _cardDal = cardDal;
        }

        public IResult Add(Card card)
        {
            _cardDal.Add(card);
            return new SuccessResult("Card Added");
        }

        public IResult Delete(Card card)
        {
            _cardDal.Delete(card);
            return new SuccessResult("Card Deleted");
        }

        public IResult Update(Card card)
        {
            return new SuccessResult("Card updated");
        }

        public IDataResult<List<Card>> GetAll()
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll());
        }

        public IDataResult<List<Card>> GetForPageable(int pageIndex, int pageCount)
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetForPageable(null, pageIndex, pageCount), Messages.CardPaging);
        }

        public IDataResult<List<Card>> Search(string ownerName, int cardId, int customerId, int pageIndex, int pageCount)
        {
            Expression<Func<Card, bool>> searchQuery = card =>
            (!string.IsNullOrWhiteSpace(ownerName) ? card.OwnerName.Contains(ownerName) : true) &&
            (cardId > 0 ? card.CardId == cardId : true) &&
            (customerId > 0 ? card.CustomerId == customerId : true);
            return new SuccessDataResult<List<Card>>(_cardDal.GetForPageable(searchQuery, pageIndex, pageCount), Messages.CardPaging);
        }
    }
}
