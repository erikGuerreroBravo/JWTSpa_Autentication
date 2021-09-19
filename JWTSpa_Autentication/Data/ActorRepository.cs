using JWTSpa_Autentication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public class ActorRepository: IActorRepository
    {
        private readonly UserContext _context;

        public ActorRepository(UserContext context)
        {
            _context = context;
        }

        public IQueryable<Actor> GetQuerybleActor()
        {
            return  _context.Actores.AsQueryable<Actor>();
        }
        
        public async Task<List<Actor>> GetActoresAsync()
        {
            return await _context.Actores.ToListAsync();
        }
        public async Task<Actor> GetActorById(int id)
        {
            return await _context.Actores.FirstOrDefaultAsync(p => p.Id == id);
        }
        public void AddActor(Actor actor)
        {
            _context.Actores.Add(actor);

        }
        public async Task SaveChangesA()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateActor(Actor actor)
        {
            _context.Entry(actor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task DeleteActor(Actor actor)
        {
            _context.Actores.Remove(actor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidarActor(int id)
        {
            return await _context.Actores.AnyAsync(p => p.Id == id);

        }
    }
}
