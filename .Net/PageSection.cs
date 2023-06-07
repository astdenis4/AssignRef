using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.PageSections
{
    public class PageSection
    {
        public int Id { get; set; }

        public int PageTranslationId { get; set; }

        public string Name { get; set; }

        public string Component { get; set; }

        public bool IsActive { get; set; }

    }
}
