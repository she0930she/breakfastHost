// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;

namespace BuberBreakfast.Services.Breakfasts.impl;


public class BreakfastServiceImpl : IBreakfastService
{
    // instantiate once, use static
    private static readonly Dictionary<Guid, Breakfast> dictBreakfast = new ();
    public void CreateBreakfast(Breakfast breakfast)
    {
        dictBreakfast.Add(breakfast.Id, breakfast);
    }

    public void DeleteBreakfast(Guid id)
    {
        dictBreakfast.Remove(id);
    }

    public Breakfast GetBreakfast(Guid id)
    {
        Breakfast breakfast = dictBreakfast[id];
        return breakfast;
    }

    public void UpsertBreakfast(Breakfast breakfast)
    {
        dictBreakfast[breakfast.Id] = breakfast;
    }
}
