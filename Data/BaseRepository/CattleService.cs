using Jallikattu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jallikattu.Data.BaseRepository
{
    public class CattleService: EntityBaseRepository<CattleBreedsTable>, ICattleBreedService
    {
        public CattleService(Entities context) : base(context)
        {

        }

    }
}