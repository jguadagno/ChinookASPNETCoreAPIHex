using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;

namespace Chinook.Domain.Converters
{
    public class MediaTypeCoverter
    {
        public static MediaTypeViewModel Convert(MediaType mediaType)
        {
            var mediaTypeViewModel = new MediaTypeViewModel()
            {
                MediaTypeId = mediaType.MediaTypeId,
                Name = mediaType.Name
            };
            return mediaTypeViewModel;
        }
        
        public static List<MediaTypeViewModel> ConvertList(List<MediaType> mediaTypes)
        {
            List<MediaTypeViewModel> mediaTypeViewModels = new List<MediaTypeViewModel>();
            foreach(var m in mediaTypes)
            {
                var mediaTypeViewModel = new MediaTypeViewModel
                {
                    MediaTypeId = m.MediaTypeId,
                    Name = m.Name
                };
                mediaTypeViewModels.Add(mediaTypeViewModel);
            }

            return mediaTypeViewModels;
        }
    }
}