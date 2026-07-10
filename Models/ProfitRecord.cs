using System;

namespace ProfitCalcApp.Models
{
    public class ProfitRecord
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Profit { get; set; }
        public string? Title { get; set; }
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ImagePath { get; set; }

    }
}

