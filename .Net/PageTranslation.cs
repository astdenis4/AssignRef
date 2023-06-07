using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.PageTranslations
{
    public class PageTranslation
    {
        public int Id { get; set; }

        public LookUp Language { get; set; }

        public string Link { get; set; }

        public string Name { get; set; }

        public int CreateBy { get; set; }

        public bool IsActive { get; set; }

    }
}
