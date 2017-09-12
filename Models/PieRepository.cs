using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly ApplicationDbContext _context;

        public PieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pie> Pies
        {
            get { return _context.Pies.Include(c => c.Category); }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _context.Pies.Include(c => c.Category)
                    .Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return _context.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}