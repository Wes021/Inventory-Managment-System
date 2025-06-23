namespace Inventory.DTO_S.Account
{
    public class RegesterationResponseDTO
    {
        public bool IsSuccessfulRegistration { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
