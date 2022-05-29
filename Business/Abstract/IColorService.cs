﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    internal interface IColorService
    {
        IDataResult<List<Color>> GetAll();
        
        IDataResult<Color> GetById(int colorId);
    }
}