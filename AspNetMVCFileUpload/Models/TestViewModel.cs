using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVCFileUpload.Models
{
    public class TestViewModel
    {
 

        public int Id { get; set; }
        public string NumberEmployee { get; set; }
        public string File_Name { get; set; }
        public string File_Content { get; set; }
        public string Date { get; set; }
    }
}