using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.DTO
{
    public class StatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ActionTime { get; set; }

        public int UserId { get; set; }
        public int VisitorId { get; set; }
    }
}
