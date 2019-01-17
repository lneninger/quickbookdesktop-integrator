using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commons.DTOs
{
    public class FileItemRefOutputDTO
    {
        public int Id { get; set; }

        public string FullUrl { get; set; }
        public int FileId { get; set; }
    }
}
