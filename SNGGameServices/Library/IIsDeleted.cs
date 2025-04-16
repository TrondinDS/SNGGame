namespace Library
{
    public interface IIsDeleted
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
