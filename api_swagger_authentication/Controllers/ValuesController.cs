﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_swagger_authentication.Models;

namespace api_swagger_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Context _context;

        public ValuesController(Context context)
        {
            _context = context;
        }

        // GET: api/Values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Values>>> Getvalues()
        {
            return await _context.values.ToListAsync();
        }

        // GET: api/Values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Values>> GetValues(int id)
        {
            var values = await _context.values.FindAsync(id);

            if (values == null)
            {
                return NotFound();
            }

            return values;
        }

        // PUT: api/Values/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValues(int id, Values values)
        {
            if (id != values.Id)
            {
                return BadRequest();
            }

            _context.Entry(values).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValuesExists(id))
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

        // POST: api/Values
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Values>> PostValues(Values values)
        {
            _context.values.Add(values);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetValues", new { id = values.Id }, values);
        }

        // DELETE: api/Values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Values>> DeleteValues(int id)
        {
            var values = await _context.values.FindAsync(id);
            if (values == null)
            {
                return NotFound();
            }

            _context.values.Remove(values);
            await _context.SaveChangesAsync();

            return values;
        }

        private bool ValuesExists(int id)
        {
            return _context.values.Any(e => e.Id == id);
        }
    }
}
