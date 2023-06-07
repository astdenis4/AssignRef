using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Emails
{
    public class AssignmentEmail : ContactUsRequest
    {
        public string GameDate { get; set; }
        public string HomeTeam { get; set; }  
        public string VisitingTeam { get; set; }
        public string GameLocation { get; set; }

    }
}


