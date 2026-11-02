

namespace GymManagmentSystem_BLL.Servieces.Trainers.ViewModels
{
    public class TrainerUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Specialties { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public string Street { get; set; } = default!;

        public string City { get; set; } = default!;
        public int BuildingNumber { get; set; } = default!;
    }
}
