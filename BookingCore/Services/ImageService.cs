using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public class ImageService : Service<Image>, IImageService
    {
        public ImageService(ITrackableRepository<Image> repository) : base(repository)
        {

        }


    }
}
