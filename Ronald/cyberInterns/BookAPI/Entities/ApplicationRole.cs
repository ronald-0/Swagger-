using System;
using Microsoft.AspNetCore.Identity;

namespace BookAPI.Entities
{
    public class ApplicationRole : IdentityRole
    {
       public int ID { get; set; }
       public string task { get; set; }
       public DateTime deadline { get; set; }
    }
}
