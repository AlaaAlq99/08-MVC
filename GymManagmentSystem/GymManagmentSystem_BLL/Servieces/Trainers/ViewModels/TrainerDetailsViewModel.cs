

namespace GymManagmentSystem_BLL.Servieces.Trainers.ViewModels
{
    public class TrainerDetailsViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Specialization { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? DateOfBirth { set; get; }

        public string Address { set; get; } = default!;


    }
}
