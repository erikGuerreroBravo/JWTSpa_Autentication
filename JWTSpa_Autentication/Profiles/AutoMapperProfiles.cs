using AutoMapper;
using JWTSpa_Autentication.Dtos;
using JWTSpa_Autentication.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        //GeometryFactory geometryFactory
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDto>().ReverseMap();
            CreateMap<GeneroCreacionDto, Genero>().ReverseMap();
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<ActorCreacionDto, Actor>().ForMember(x=> x.Foto,options=> options.Ignore());
            CreateMap<ActorPatchDto, Actor>().ReverseMap();
            CreateMap<RolCreacionDto, Rol>().ReverseMap();
            CreateMap<RolDto, Rol>().ReverseMap().ForMember(dest=> dest.Status, origen=>origen.MapFrom(c=> c.Status));
            CreateMap<StatusDto, Status>().ReverseMap();
            CreateMap<PeliculasDto, Pelicula>().ForMember(x=> x.Poster, options=> options.Ignore()).ReverseMap();
            CreateMap<PeliculasCreacionDto, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x=> x.PeliculasActores, options=> options.MapFrom(MapPeliculasActores));

            CreateMap<Pelicula, PeliculaDetalleDto>().ForMember(x => x.Generos, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x=> x.Actores, options => options.MapFrom(MapPeliculasActores));
            
            CreateMap<SalaDeCine, SalaDeCineDto>()
                .ForMember(x=> x.Latitud, x=> x.MapFrom(y => y.Ubicacion.Y))
                .ForMember(x=>x.Longitud, x=> x.MapFrom(y => y.Ubicacion.Y));
            //var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CreateMap<SalaDeCineDto, SalaDeCine>();

            CreateMap<SalaDeCineCreacionDto, SalaDeCine>();

        }

        private List<ActorPeliculaDetalleDto> MapPeliculasActores(Pelicula pelicula, PeliculaDetalleDto peliculaDetalleDto)
        {
            var resultado = new List<ActorPeliculaDetalleDto>();
            if (pelicula.PeliculasActores == null) { return resultado; }
            foreach (var actorPelicula in pelicula.PeliculasActores)
            {
                resultado.Add(new ActorPeliculaDetalleDto
                {
                    ActorId = actorPelicula.ActorId,
                    Personaje = actorPelicula.Personaje,
                    NombrePersona = actorPelicula.Actor.Nombre
                });
            }
            return resultado;
        }


        private List<GeneroDto> MapPeliculasGeneros(Pelicula pelicula, PeliculaDetalleDto peliculaDetalleDto)
        {
            var resultado = new List<GeneroDto>();
            if (pelicula.PeliculasGeneros == null) 
            {
                return resultado;
            }
            foreach (var generoPelicula in pelicula.PeliculasGeneros)
            {
                resultado.Add(new GeneroDto() { Id = generoPelicula.GeneroId, Nombre = generoPelicula.Genero.Nombre });
            }
            return resultado;
        }




        private List<PeliculasGeneros> MapPeliculasGeneros(PeliculasCreacionDto peliculasCreacionDto, Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();
            if (peliculasCreacionDto.GenerosIDs == null)
            {
                return resultado;
            }
            foreach (var id in peliculasCreacionDto.GenerosIDs)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id});
            }
            return resultado;
        }
        private List<PeliculasActores> MapPeliculasActores(PeliculasCreacionDto peliculasCreacionDto, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();
            if (peliculasCreacionDto.Actores == null)
            {
                return resultado;
            }
            foreach (var actor in peliculasCreacionDto.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.ActorId, Personaje = actor.Personaje});
            }
            return resultado;
        }







    }
}
