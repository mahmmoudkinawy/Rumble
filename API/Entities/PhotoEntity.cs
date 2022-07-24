namespace API.Entities;

[Table("Photos")]
public class PhotoEntity
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    [ForeignKey(nameof(UserEntity))]
    public int UserId { get; set; }
    public UserEntity UserEntity { get; set; }
}
