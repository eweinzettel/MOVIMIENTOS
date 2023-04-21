using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovimeintoWebAPI.Models;
using MovimeintoWebAPI.Models.Dto;

namespace MovimeintoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficinasController : ControllerBase
    {
        private readonly MOVIMIENTOSContext _context;

        public OficinasController(MOVIMIENTOSContext context)
        {
            _context = context;
        }

        // GET: api/Oficinas
        [HttpGet]
        public async Task<ActionResult<List<OficinaDTO>>> GetOficinas()
        {
            List<OficinaDTO> oficinaDTOs = new List<OficinaDTO>();
            try
            {
                // listaOficina es una lista de objetos de tipo oficina.

                var ListaOficina = _context.Oficinas.Include(c => c.Central).ToList();

                if (_context.Oficinas == null)
                {
                    return NotFound();
                }
                else
                {
                    foreach (var item in ListaOficina)
                    {
                        OficinaDTO DatosOficinaDTO = new OficinaDTO();

                        DatosOficinaDTO.IdOficina = item.IdOficina;
                        DatosOficinaDTO.CentralId = item.CentralId;
                        DatosOficinaDTO.CentralIndicativo = item.Central.CentralIndicativo;
                        DatosOficinaDTO.CentralNombre = item.Central.CentralNombre;
                        DatosOficinaDTO.OficinaTipo = item.OficinaTipo;
                        DatosOficinaDTO.OficinaIndicativo = item.OficinaIndicativo;
                        DatosOficinaDTO.OficinaNombre = item.OficinaNombre;

                        oficinaDTOs.Add(DatosOficinaDTO);
                    }

                    await _context.SaveChangesAsync();
                    return oficinaDTOs;
                }
            }
            catch (Exception ex)            {

                await _context.SaveChangesAsync();
                return BadRequest(ex.Message);
                
            }            
        }

        


        

        // GET: api/Oficinas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Oficina>> GetOficina(int id)
        {
          if (_context.Oficinas == null)
          {
              return NotFound();
          }
            var oficina = await _context.Oficinas.FindAsync(id);

            if (oficina == null)
            {
                return NotFound();
            }

            return oficina;
        }

        // PUT: api/Oficinas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOficina(int id, Oficina oficina)
        {
            if (id != oficina.IdOficina)
            {
                return BadRequest();
            }

            _context.Entry(oficina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OficinaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Oficinas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Oficina>> PostOficina(Oficina oficina)
        {
          if (_context.Oficinas == null)
          {
              return Problem("Entity set 'MOVIMIENTOSContext.Oficinas'  is null.");
          }
            _context.Oficinas.Add(oficina);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OficinaExists(oficina.IdOficina))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOficina", new { id = oficina.IdOficina }, oficina);
        }

        // DELETE: api/Oficinas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOficina(int id)
        {
            if (_context.Oficinas == null)
            {
                return NotFound();
            }
            var oficina = await _context.Oficinas.FindAsync(id);
            if (oficina == null)
            {
                return NotFound();
            }

            _context.Oficinas.Remove(oficina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OficinaExists(int id)
        {
            return (_context.Oficinas?.Any(e => e.IdOficina == id)).GetValueOrDefault();
        }
    }
}
