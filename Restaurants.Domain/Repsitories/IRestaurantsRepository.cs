﻿using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repsitories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
}
