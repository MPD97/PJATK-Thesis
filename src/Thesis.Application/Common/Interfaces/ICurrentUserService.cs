﻿using System.Collections.Generic;
using System.Text;

namespace Thesis.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
    }
}
