using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAgenda.Context;
using WebAgenda.Models;

namespace WebAgenda.Controllers
{
    public class ContactsController : ApiController
    {
        private AgendaContext db = new AgendaContext();

        public IQueryable<Contact> GetContacts()
        {
            return db.Contacts;
        }
        
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> GetContact(int id)
        {
            Contact contato = await db.Contacts.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }

            return Ok(contato);

        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContact(int id, Contact contato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contato.Id)
            {
                return BadRequest();
            }

            db.Entry(contato).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ContactExists(id))
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> PostContact(Contact contato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contacts.Add(contato);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = contato.Id }, contato);
        }

        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> DeleteContact(int id)
        {
            Contact contato = await db.Contacts.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contato);
            await db.SaveChangesAsync();

            return Ok(contato);
        }



        private bool ContactExists(int id)
            {
                return db.Contacts.Count(c => c.Id == id) > 0;
            }

        

    }

    
}
