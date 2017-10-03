using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chinook.API.ViewModels;
using Chinook.Data;
using Chinook.Domain.Entities;

namespace Chinook.API.Mapping
{
    public class PlaylistViewModelConverter : ITypeConverter<Playlist, PlaylistViewModel>
    {
        public PlaylistViewModel Convert(Playlist source, PlaylistViewModel destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
