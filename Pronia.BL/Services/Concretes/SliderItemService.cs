﻿using Microsoft.EntityFrameworkCore;
using Pronia.BL.Services.Abstractions;
using Pronia.DAL.Contexts;
using Pronia.DAL.Models;

namespace Pronia.BL.Services.Concretes
{
    public class SliderItemService : ISliderItemService
    {
        private readonly ProniaDBContext _context;

        public SliderItemService(ProniaDBContext context)
        {
            _context = context;
        }

        #region Read
        public async Task<SliderItem> GetSliderItemByIdAsync(int id)
        {
            SliderItem? sliderItem = await _context.SliderItems.FindAsync(id);
            if (sliderItem is null)
            {
                throw new Exception($"Slider Item not found.");
            }

            return sliderItem;
        }

        public async Task<List<SliderItem>> GetAllSliderItemsAsync()
        {
            List<SliderItem> sliderItems = await _context.SliderItems.ToListAsync();
            return sliderItems;
        }
        #endregion

        #region Create

        public async Task CreateSliderItemAsync(SliderItem sliderItem)
        {
            await _context.SliderItems.AddAsync(sliderItem);
            int rows = await _context.SaveChangesAsync();

            if (rows != 1)
            {
                throw new Exception("Something went wrong.");
            }
        }
        #endregion

        #region Update

        public async Task UpdateSliderItemAsync(int id, SliderItem sliderItem)
        {
            if (id != sliderItem.Id) { throw new Exception("Ids are not same"); }
            SliderItem? baseSliderItem = await _context.SliderItems.FindAsync(id);
            if (baseSliderItem is null)
            {
                throw new Exception($"Slider Item not found with this id({id})");
            }

            if (baseSliderItem.IsDeleted)
            {
                throw new Exception($"Slider Item not found with this id({id})");
            }

            baseSliderItem.Title = sliderItem.Title;
            baseSliderItem.ShortDescription = sliderItem.ShortDescription;
            baseSliderItem.Offer = sliderItem.Offer;
            baseSliderItem.ImagePath = sliderItem.ImagePath;

            baseSliderItem.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
        #endregion
        #region Delete

        public async Task SoftDeleteSliderItemAsync(int id)
        {
            #region SoftDeleteV2
            SliderItem? baseSliderItem = await _context.SliderItems.SingleOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
            if (baseSliderItem is null)
            {
                throw new Exception($"Slider Item not found.");
            }

            baseSliderItem.IsDeleted = true;
            baseSliderItem.LastModifiedDate = DateTime.Now;
            baseSliderItem.DeletedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            #endregion

        }

        public async Task HardDeleteSliderItemAsync(int id)
        {


            SliderItem? baseSliderItem = await _context.SliderItems.FindAsync(id);
            if (baseSliderItem is null)
            {
                throw new Exception($"Slider Item not found with this id({id})");
            }

            _context.SliderItems.Remove(baseSliderItem);
        }

        #endregion

    }
}
