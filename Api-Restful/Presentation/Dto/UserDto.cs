
namespace Api_Restful.Presentation.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int? ID_JobTitle { get; set; }
    public int? ID_Token { get; set; }

    public JobTitlesDto JobTitle { get; set; }
    public ICollection<TokenDto> Tokens { get; set; }
}
