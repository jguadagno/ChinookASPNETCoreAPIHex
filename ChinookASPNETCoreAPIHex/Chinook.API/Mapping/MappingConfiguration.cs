using AutoMapper;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using Chinook.API.ViewModels;

namespace Chinook.API.Mapping
{
    public static class MappingConfiguration
    {
        private static MapperConfiguration _configuration;

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException("services");


            services.AddSingleton(GetConfiguration().CreateMapper());

            return services;
        }

        public static MapperConfiguration GetConfiguration()
        {
            return _configuration ?? (_configuration = new MapperConfiguration(_ =>
            {
                _.CreateMap<Album, AlbumViewModel>();
                _.CreateMap<Artist, ArtistViewModel>();
                _.CreateMap<Customer, CustomerViewModel>();
                _.CreateMap<Employee, EmployeeViewModel>();
                _.CreateMap<Genre, GenreViewModel>();
                _.CreateMap<Invoice, InvoiceViewModel>();
                _.CreateMap<InvoiceLine, InvoiceLineViewModel>();
                _.CreateMap<MediaType, MediaTypeViewModel>();
                _.CreateMap<Playlist, PlaylistViewModel>();
                _.CreateMap<PlaylistTrack, PlaylistTrackViewModel>();
                _.CreateMap<Track, TrackViewModel>();
            }));
        }
    }
}
