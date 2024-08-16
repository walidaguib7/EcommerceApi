﻿using Ecommerce.Dtos.Media;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class FilesMapper
    {
        public static MediaModel ToFile(this CreateFile file)
        {
            return new MediaModel
            {
                file = file.file,
                CreatedAt = new DateOnly()
            };
        }

        public static FileDto ToFileDto(this MediaModel model)
        {
            return new FileDto
            {
                Id = model.Id,
                file = model.file
            };
        }
    }
}
