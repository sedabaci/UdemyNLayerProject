using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyNLayerProject.API.DTOS
{
    public class ErrorDto
    {
        public int Status { get; set; }
        public List<string> Errors { get; set; }
        public ErrorDto()
        {
            Errors = new List<string>();
        }
    }
}
