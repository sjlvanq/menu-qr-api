﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuApi.Data;
using MenuApi.Models;

namespace MenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliesController : ControllerBase
    {
        private readonly MenuDb _context;

        public SuppliesController(MenuDb context)
        {
            _context = context;
        }

        // GET: api/Supplies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supply>>> GetSupplies()
        {
            return await _context.Supplies.ToListAsync();
        }

        // GET: api/Supplies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supply>> GetSupply(int id)
        {
            var supply = await _context.Supplies.FindAsync(id);

            if (supply == null)
            {
                return NotFound();
            }

            return supply;
        }

        // PUT: api/Supplies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupply(int id, SupplyDTO supply)
        {
            var targetSupply = _context.Supplies.Include(s => s.Products)
                .Where(s=>s.Id==id).First();

            //var targetSupply = await _context.Supplies.FindAsync(id);
            if (targetSupply == null) { return NotFound(); }
            
            if (supply.Name != null)
            {
                targetSupply.Name = supply.Name;
            }
            if (supply.ProductIds != null)
            {
                var products = await _context.Products
                .Where(p => supply.ProductIds.Contains(p.Id))
                .ToListAsync();
                if(targetSupply.Products != null)
                { 
                    targetSupply.Products.Clear();
                }
                targetSupply.Products = products;
            }
            
            _context.Entry(targetSupply).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplyExists(id))
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

        // POST: api/Supplies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supply>> PostSupply(SupplyDTO supply)
        {
            var newSupply = new Supply();
            newSupply.Name = supply.Name;
            if (supply.ProductIds != null) { 
                var products = await _context.Products
                    .Where(p => supply.ProductIds.Contains(p.Id))
                    .ToListAsync();
                newSupply.Products = products;
            }

            _context.Supplies.Add(newSupply);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupply", new { id = newSupply.Id }, newSupply);
        }

        // DELETE: api/Supplies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupply(int id)
        {
            var supply = await _context.Supplies.FindAsync(id);
            if (supply == null)
            {
                return NotFound();
            }

            _context.Supplies.Remove(supply);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplyExists(int id)
        {
            return _context.Supplies.Any(e => e.Id == id);
        }
    }
}
