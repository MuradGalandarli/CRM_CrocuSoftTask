﻿using Entity.Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Abstract
{
    public interface ITeam:IGeneric<Team>
    {
        public Task<bool> CheckTeam(int teamId);
    }
}
