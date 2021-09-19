using JWTSpa_Autentication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetActoresAsync();
        Task<Actor> GetActorById(int id);
        void AddActor(Actor actor);
        Task SaveChangesA();
        Task<bool> UpdateActor(Actor actor);
        Task DeleteActor(Actor actor);
        Task<bool> ValidarActor(int id);
        IQueryable<Actor> GetQuerybleActor();
    }
}