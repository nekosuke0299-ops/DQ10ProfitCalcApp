using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProfitCalcApp.Data;
using ProfitCalcApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProfitCalcApp.Pages
{
    public class ProfitModel : PageModel
    {
        private readonly AppDbContext _context;

        public ProfitModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int Cost { get; set; }

        [BindProperty]
        public int Price { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty]
        public string? Title { get; set; }

        [BindProperty]
        public string? Note { get; set; }

        [BindProperty]
        public string? SelectedImage { get; set; }

        // 🔍 検索用プロパティ（GET対応）
        [BindProperty(SupportsGet = true)]
        public string? SearchTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchImage { get; set; }

        public string? ResultMessage { get; set; }
        public List<ProfitRecord> History { get; set; } = new();

        // ✅ 検索対応の OnGet()
        public void OnGet()
        {
            var query = _context.ProfitRecords.AsQueryable();

            // 🔍 タイトル検索
            if (!string.IsNullOrEmpty(SearchTitle))
            {
                query = query.Where(p => p.Title != null && p.Title.Contains(SearchTitle));
            }


            // 🔍 画像検索
            if (!string.IsNullOrEmpty(SearchImage))
            {
                query = query.Where(p => p.ImagePath != null && p.ImagePath.Contains(SearchImage));
            }


            // 結果を取得（新しい順）
            History = query.OrderByDescending(r => r.CreatedAt).ToList();
        }

        public IActionResult OnPost()
        {
            var profit = (Price - Cost) * Quantity;

            var record = new ProfitRecord
            {
                Cost = Cost,
                Price = Price,
                Quantity = Quantity,
                Profit = profit,
                Title = Title,
                Note = Note,
                ImagePath = !string.IsNullOrEmpty(SelectedImage) ? $"/images/{SelectedImage}" : null
            };

            _context.ProfitRecords.Add(record);
            _context.SaveChanges();

            ResultMessage = $"利益: {(int)profit} G";//円からGへ変更した

            // 保存後も履歴を最新状態で表示
            History = _context.ProfitRecords
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return Page();
        }
    }
}
