using System.Data.Entity;
using WebAgenda.Models;

namespace WebAgenda.Context
{
    public class AgendaContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
    }
}