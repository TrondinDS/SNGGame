namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame
{
    public class StatisticGameDTO
    {
        public Guid Id { get; set; }
        public int RatingSum { get; set; }
        public int PeopleCount { get; set; }
        public Guid GameId { get; set; }
    }
}
