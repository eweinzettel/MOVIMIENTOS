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
    public class CentralsController : ControllerBase
    {
        private readonly MOVIMIENTOSContext _context;

        public CentralsController(MOVIMIENTOSContext context)
        {
            _context = context;
        }

        // GET: api/Centrals
        [HttpGet]
        public async Task<ActionResult<List<CentralDTO>>> GetCentrals()
        {
          List<CentralDTO> centralDTOs = new List<CentralDTO>();
          
            if (_context.Centrals == null)
          {
              return NotFound();
          }
            else 
            { 
              foreach (var item in _context.Centrals) 
              {
                    CentralDTO DatosCentralDTO = new CentralDTO();

                    DatosCentralDTO.IdCentral = item.IdCentral;
                    DatosCentralDTO.CentralNombre = item.CentralNombre;
                    DatosCentralDTO.CentralIndicativo = item.CentralIndicativo;                

                    centralDTOs.Add(DatosCentralDTO);
              }
                  return centralDTOs;
            }
        }

        // GET: api/Centrals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Central>> GetCentral(int id)
        {
           // CentralDTO centralDTO = new CentralDTO();

          if (_context.Centrals == null)
          {
              return NotFound();
          }
            var central = await _context.Centrals.FindAsync(id);

            if (central == null)
            {
                return NotFound();
            }

            return central;
        }

   // ----------------------- TRAER TODAS LAS OFICINAS DE UNA (1) SOLA CENTRAL ------------


        [HttpGet]
        [Route("GetOficinasdeCentral")]
        public async Task<ActionResult<List<OficinaDTO>>> GetOficinasdeCentral(int IdCentral)
        {

            if (_context.Centrals == null)
            {
                return NotFound();
            }

            var central = await _context.Centrals.
                Include(c => c.Oficinas).
                FirstOrDefaultAsync(c => c.IdCentral == IdCentral);

           
            if (central == null)
            {
                return NotFound();
            }
            
            try
            {
                
                List<Oficina> listaOficinas = central.Oficinas.ToList();

                List<OficinaDTO> listaOficinasPorCentralDTO = new List<OficinaDTO>();

                foreach(var item in listaOficinas) 
                { 
                    
                    OficinaDTO oficinaDTO = new OficinaDTO();
                    
                    oficinaDTO.IdOficina = item.IdOficina;
                    oficinaDTO.CentralId = item.CentralId;
                    oficinaDTO.CentralIndicativo = item.Central.CentralIndicativo;
                    oficinaDTO.CentralNombre = item.Central.CentralNombre;
                    oficinaDTO.OficinaTipo = item.OficinaTipo;
                    oficinaDTO.OficinaIndicativo = item.OficinaIndicativo;
                    oficinaDTO.OficinaNombre = item.OficinaNombre;

                    listaOficinasPorCentralDTO.Add(oficinaDTO);
                }

                // listaOficina es una lista de objetos de tipo oficina y filtramos con el where para que sea unicamente de una determinada central.
                //var ListaOficina = _context.Oficinas.Include(c => c.Central).ToList(); //.Where(CentralId == IdCentral).ToList();

                return listaOficinasPorCentralDTO;
            }
            catch (Exception ex)
            {
                await _context.SaveChangesAsync();
                return BadRequest(ex.Message);
            }

        }





        // PUT: api/Centrals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCentral(int id, CentralDTO DatosCentralDTO)
        {
            if (id != DatosCentralDTO.IdCentral)
            {
                return BadRequest();
            }

            _context.Entry(DatosCentralDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentralExists(id))
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

        // POST: api/Centrals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Central>> PostCentral(Central central)
        {
          if (_context.Centrals == null)
          {
              return Problem("Entity set 'MOVIMIENTOSContext.Centrals'  is null.");
          }
            _context.Centrals.Add(central);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCentral", new { id = central.IdCentral }, central);
        }

        // DELETE: api/Centrals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCentral(int id)
        {
            if (_context.Centrals == null)
            {
                return NotFound();
            }
            var central = await _context.Centrals.FindAsync(id);
            if (central == null)
            {
                return NotFound();
            }

            _context.Centrals.Remove(central);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CentralExists(int id)
        {
            return (_context.Centrals?.Any(e => e.IdCentral == id)).GetValueOrDefault();
        }
    }
}
