namespace Application.Tokens;

public class TokensVm
{
    public int TotalCount { get; set; }
    public ICollection<TokenDto> Tokens { get; set; }
}