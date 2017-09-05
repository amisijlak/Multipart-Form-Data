﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultipartFormDataSample.Server.Models
{
    public class ImageSet
    {
        public string Name { get; set; }

        public List<Image> Images { get; set; }
    }

    public class Image
    {
        public string FileName { get; set; }

        public string MimeType { get; set; }

        public byte[] ImageData { get; set; }
    }
}