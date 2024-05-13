using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels.Sliders
{
    public class CreateSlidersVM
    {
        [MaxLength(32 ,ErrorMessage ="bashliq 32 simvoldan cox ola bilmez" ), Required]
        public string Title { get; set; }
        [Range(0, 100 ,ErrorMessage ="Faizi duzgun daxil edin")]
        public int Discount { get; set; }
        [MaxLength(64 ,ErrorMessage = "64 simvoldan cox ola bilmez"), Required]
        public string Subtitle { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
