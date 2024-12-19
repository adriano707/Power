using Power.Domain.RingsAggregate;

namespace Power.ViewModel
{
    public class RingViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Power { get; set; }
        public string Carrier { get; set; }
        public ForgedBy ForgedBy { get; set; }
        public string Image { get; set; }
    }
}
