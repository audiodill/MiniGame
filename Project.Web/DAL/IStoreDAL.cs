﻿using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.DAL
{
    public interface IStoreDAL
    {
        List<ItemModel> GetStoreInventory(int sort, int InventoryId);
    }
}